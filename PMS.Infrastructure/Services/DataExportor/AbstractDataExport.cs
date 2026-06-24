using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PMS.Application.Common;
using PMS.Application.Interfaces.Services;

namespace PMS.Infrastructure.Services.DataExportor
{
    public abstract class AbstractDataExport : IExcelDocumentGenerator
    {
        protected string _sheetName;
        protected string _fileName;
        protected List<string> _headers;
        protected List<string> _type;
        protected IWorkbook _workbook;
        protected ISheet _sheet;

        //string FormatTime(double? seconds) => !seconds.HasValue ? "" : $"{(int)seconds / 3600}h {(int)seconds / 60 % 60}m";
        //string ToYesNoString(bool value) => value ? "Yes" : "No";
        public async Task<byte[]> ExportToExcel<T>(IList<T> source, List<string> headers = null, Func<T, object[]> formatterFunc = null, string sheetName = ApplicationConstants.Export.DEFAULT_SHEET_NAME)
        {
            _sheetName = sheetName;

            #region Generation of Workbook, Sheet and General Configuration

            _workbook = new XSSFWorkbook();
            _sheet = _workbook.CreateSheet(_sheetName);

            var headerStyle = _workbook.CreateCellStyle();
            var headerFont = _workbook.CreateFont();
            headerFont.IsBold = true;
            headerFont.FontHeightInPoints = 12;
            headerStyle.SetFont(headerFont);
            #endregion

            _headers = headers;

            WriteData(source.ToList(), formatterFunc);

            #region Generating Header Cells
            var header = _sheet.CreateRow(0);
            for (var i = 0; i < _headers.Count; i++)
            {
                var cell = header.CreateCell(i);
                cell.SetCellValue(_headers[i]);
                cell.CellStyle = headerStyle;
                // It's heavy, it slows down your Excel if you have large data                
                //_sheet.AutoSizeColumn(i);
            }
            #endregion

            for (int i = 0; i <= 20; i++) _sheet.AutoSizeColumn(i);

            #region Generating and Returning Stream for Excel
            using (var memoryStream = new MemoryStream())
            {
                _workbook.Write(memoryStream);

                //var excelBytes = memoryStream.ToArray();
                byte[] excelBytes = await Task.Run(() => memoryStream.ToArray());

                return excelBytes;
            }
            #endregion
        }


        /*
        //EPPlus

            var fileBytes = await _excelExport.WriteToExcel(
                accounts.ToList(),
                new[]
                {
                    "Account Code",
                    "Account Name",
                    "Is System Based",
                    "Current Balance",
                    "Account Type",
                    "Account Sub-Type"
                },
                r => new object[]
                {
                    r.Code,
                    r.Name,
                    r.IsSystemBased,
                    $"{r.CurrentBalance:F1}",
                    r.AccountType,
                    r.AccountSubType
                }, "");

            excelResult.Add("ExcelData", fileBytes);
            return excelResult;

        public async Task<byte[]> WriteToExcel<T>(List<T> rows, string[] headers = null, Func<T, object[]> formatterFunc = null, string sheetName = ApplicationConstants.Export.DEFAULT_SHEET_NAME)
        {
            using (var ms = new MemoryStream())
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var p = new ExcelPackage(ms))
                {
                    var row = 1;
                    var col = 1;
                    var ws = p.Workbook.Worksheets.Add("Export");
                    var pis = typeof(T).GetProperties();
                    foreach (var h in headers ?? pis.Select(pi => pi.Name).ToArray())
                    {
                        ws.Cells[row, col++].Value = h;
                    }
                    foreach (var o in rows)
                    {
                        row++;
                        col = 1;
                        var values = formatterFunc == null ? pis.Select(pi => pi.GetValue(o)) : formatterFunc(o);
                        foreach (var v in values)
                        {
                            ws.Cells[row, col++].Value = v;
                        }
                    }
                    ws.Cells.AutoFitColumns();
                    return await p.GetAsByteArrayAsync();
                }
            }
        }
        */

        /// <summary>
        /// Generic Definition to handle all types of List
        /// Overrride this function to provide your own implementation
        /// </summary>
        /// <param name="exportData"></param>
        /// <param name="appendDateTimeInFileName"></param>
        public abstract void WriteData<T>(List<T> exportData, Func<T, object[]> formatterFunc = null);
    }
}
