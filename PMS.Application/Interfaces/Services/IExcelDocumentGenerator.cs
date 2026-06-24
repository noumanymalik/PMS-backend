using PMS.Application.Common;

namespace PMS.Application.Interfaces.Services
{
    public interface IExcelDocumentGenerator
    {
        Task<byte[]> ExportToExcel<T>(IList<T> source, List<string> headers = null, Func<T, object[]> formatterFunc = null, string sheetName = ApplicationConstants.Export.DEFAULT_SHEET_NAME);

        //EPPlus
        //Task<byte[]> WriteToExcel<T>(List<T> rows, string[] headers = null, Func<T, object[]> formatterFunc = null, string sheetName = ApplicationConstants.Export.DEFAULT_SHEET_NAME);

    }
}
