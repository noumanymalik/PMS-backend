using MigraDocCore.DocumentObjectModel;
using MigraDocCore.DocumentObjectModel.Tables;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PMS.Application.Common;
using PMS.Application.Interfaces.Services;

namespace PMS.Infrastructure.Services.PdfDocumentGenerator
{
    public class PdfSharpDocumentGenerator : IPdfDocumentGenerator
    {
        //private static List<string> _headers;
        private static List<Tuple<string, int>> _headers;
        private static Document _migraDocument;
        private static PdfDocument? _document;
        private static byte[] _imageArray;
        private static string _imagefile;

        public async Task<byte[]> Generate<T>(IList<T> source, List<Tuple<string, int>> headers = null, Func<T, object[]> formatterFunc = null)
        {
            _headers = headers;
            return await CreatePDF(source, formatterFunc);
        }

        private async static Task<byte[]> CreatePDF<T>(IList<T> source, Func<T, object[]> formatterFunc = null)
        {
            _document = new();
            _document.Info.Title = ApplicationConstants.ExportPdfDocument.DEFAULT_Document_NAME;
            _document.Info.Author = "Nouman Malik";
            _document.Info.Subject = "List of data";

            // Set font encoding to unicode
            XPdfFontOptions options = new(PdfFontEncoding.Unicode);

            // Create new page
            PdfPage page = _document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            //XFont font = new("Verdana", 13, XFontStyle.Bold);
            XFont font = new("OpenSans-Regular", 20, XFontStyle.Regular, options);

            // You always need a MigraDocCore document for rendering.
            _migraDocument = new();
            // Each MigraDocCore document needs at least one section.
            Section sec = _migraDocument.AddSection();

            DefineStyles();


            //Page Header
            gfx.DrawString("List Of Accounts", new XFont("Verdana", 30, XFontStyle.Bold), XBrushes.Black, new XPoint(120, 70));

            Table table = CreateTable(_migraDocument, source, formatterFunc);

            // Create a renderer and prepare (=layout) the document
            MigraDocCore.Rendering.DocumentRenderer docRenderer = new(_migraDocument);
            docRenderer.PrepareDocument();

            // Render the paragraph. You can render tables or shapes the same way.
            docRenderer.RenderObject(gfx, XUnit.FromCentimeter(1), XUnit.FromCentimeter(5), "12cm", table);


            //MemoryStream PdfStream = new();
            //_document.Save(PdfStream);
            //return PdfStream.ToArray();

            byte[]? response = null;
            using (MemoryStream pdfStream = new MemoryStream())
            {
                _document.Save(pdfStream);
                response = pdfStream.ToArray();
            }

            return response;
        }


        private static void DefineStyles()
        {
            // Get the predefined style Normal.
            Style style = _migraDocument.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Arial";
            style = _migraDocument.Styles[StyleNames.Header];
            style.Font.Name = "OpenSans-Regular";
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = _migraDocument.Styles[StyleNames.Footer];
            style.Font.Name = "OpenSans-Regular";
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            // Create a new style called Table based on style Normal
            style = _migraDocument.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Arial";
            style.Font.Size = 8;

            // Create a new style called Reference based on style Normal
            style = _migraDocument.Styles.AddStyle("Reference", "Normal");
            style.ParagraphFormat.SpaceBefore = "5mm";
            style.ParagraphFormat.SpaceAfter = "5mm";
        }

        private static Table CreateTable<T>(Document _migraDocument, IList<T> collection, Func<T, object[]> formatterFunc = null)
        {
            _migraDocument.LastSection.AddParagraph("Simple Table", "Heading2");

            Table table = new();
            table.Style = "Table";
            table.Borders.Width = 0.75;

            /*Column column = table.AddColumn(Unit.FromCentimeter(3));
            column.Format.Alignment = ParagraphAlignment.Center;

            table.AddColumn(Unit.FromCentimeter(2));
            table.AddColumn(Unit.FromCentimeter(2));
            table.AddColumn(Unit.FromCentimeter(3));
            table.AddColumn(Unit.FromCentimeter(3));*/

            var properties = typeof(T).GetProperties();
            foreach (var h in _headers ?? properties.Select(pi => new Tuple<string, int>(pi.Name, 2)))
            {
                //table.AddColumn(Unit.FromCentimeter(h.Item2));
                table.AddColumn(Unit.FromCentimeter(h.Item2));
            }

            Row row = table.AddRow();
            //row.Shading.Color = Colors.MediumPurple;
            row.Shading.Color = new Color(27, 40, 80);
            row.Format.Font.Color = Colors.White;
            Cell cell = null;

            var rowIndex = 0;
            var colIndex = 0;

            //var properties = typeof(T).GetProperties();
            //foreach (var h in _headers ?? properties.Select(pi => pi.Name))
            //foreach (var h in _headers ?? properties.Select(pi => new { pi.Name, 2 }))
            foreach (var h in _headers ?? properties.Select(pi => new Tuple<string, int>(pi.Name, 2)))
            {
                //table.AddColumn(Unit.FromCentimeter(h.Item2));
                cell = row.Cells[colIndex++];
                cell.AddParagraph(h.Item1);
            }

            foreach (var item in collection)
            {
                colIndex = 0;
                row = table.AddRow();

                var values = formatterFunc == null ? properties.Select(pi => pi.GetValue(item)) : formatterFunc(item);
                foreach (var v in values)
                {
                    cell = row.Cells[colIndex++];
                    var currentCellValue = Convert.ToString(v);
                    cell.AddParagraph(currentCellValue);
                }
            }

            table.SetEdge(0, 0, 5, 1, Edge.Box, BorderStyle.Single, 1.5, Colors.Black);

            _migraDocument.LastSection.Add(table);

            return table;
        }
    }
}
