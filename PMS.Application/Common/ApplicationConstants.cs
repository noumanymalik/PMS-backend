namespace PMS.Application.Common
{
    public static class ApplicationConstants
    {
        public static class Message
        {
            public static readonly string NOTFOUND = "Not Found";
            public static readonly string UPDATED = "Updated Successfully";
            public static readonly string EXISTS = "Exists";
            public static readonly string SAVED = "Save Successfully";
        }

        public struct Export
        {
            public const string DEFAULT_SHEET_NAME = "Sheet1";
            public const string DEFAULT_FILE_DATETIME = "MM-dd-yyyy HHmm";
            public const string DATETIME_FORMAT = "dd-MMM-yyyy HH:mm:ss";
            public const string EXCEL_MEDIA_TYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            public const string DISPOSITION_TYPE_ATTACHMENT = "attachment";


            #region DataType available for Excel Export
            public const string STRING = "string";
            public const string FLOAT = "single";
            public const string INT32 = "int32";
            public const string INT64 = "long";
            public const string BOOLEAN = "boolean";
            public const string DOUBLE = "double";
            public const string DECIMAL = "decimal";
            public const string DATETIME = "datetime";
            #endregion
        }

        public struct ExportPdfDocument
        {
            public const string DEFAULT_Document_NAME = "PMS Document";
            public const string DEFAULT_Document_Author = "Nouman Malik";
            public const string DEFAULT_Document_Subject = "PMS List of data";

        }

    }
}