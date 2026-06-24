using NPOI.SS.UserModel;

namespace PMS.Infrastructure.Services.DataExportor
{
    public class ExcelGenerator : AbstractDataExport
    {
        public ExcelGenerator()
        {
            _headers = new List<string>();
            _type = new List<string>();
        }
        public sealed override void WriteData<T>(List<T> data, Func<T, object[]> formatterFunc = null)
        {
            var properties = typeof(T).GetProperties();
            /*foreach (var h in _headers ?? properties.Select(pi => pi.Name))
            {
                //ws.Cells[row, col++].Value = h;
            }*/
            var row = 0;
            var col = 0;

            IRow sheetRow = null;
            //ICellStyle dateStyle = _workbook.CreateCellStyle();
            //IDataFormat format = _workbook.CreateDataFormat();
            //dateStyle.DataFormat = format.GetFormat("dd-MMM-yyyy HH:mm:ss");

            foreach (var item in data)
            {
                row++;
                col = 0;

                sheetRow = _sheet.CreateRow(row);

                var values = formatterFunc == null ? properties.Select(pi => pi.GetValue(item)) : formatterFunc(item);
                foreach (var v in values)
                {
                    ICell newCell = sheetRow.CreateCell(col++);
                    var currentCellValue = Convert.ToString(v);
                    newCell.SetCellValue(currentCellValue);
                }
            }
        }
    }
}
