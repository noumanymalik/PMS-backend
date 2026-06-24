using System.Globalization;
using System.Text.RegularExpressions;

namespace PMS.Application.Extensions
{
    public static class StringExtensions
    {
        private static readonly Regex _splitOnCapitals= new Regex(@"(?<=[A-Z])(?=[A-Z][a-z]) | (?<=[^A-Z])(?=[A-Z]) | (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);
        public static string ToYesNoString(this bool value) => value ? "Yes" : "No";
        public static string FormatTime(this double? seconds) => !seconds.HasValue ? "" : $"{(int)seconds / 3600}h {(int)seconds / 60 % 60}m";

        //public static string ToTitleCase(this string title) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title);
        public static string ToTitleCase(this string title) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title.ToLower());

        public static string SplitOnCapitals(this string input) => _splitOnCapitals.Replace(input, " ");

    }
}