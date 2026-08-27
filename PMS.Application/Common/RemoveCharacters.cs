namespace PMS.Application.Common
{
    public static class RemoveCharacters
    {
        public static string RemoveSpecialCharacters(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            return new string(
                name.Where(c =>
                    (c >= 'A' && c <= 'Z') ||
                    (c >= 'a' && c <= 'z') ||
                    char.IsWhiteSpace(c))
                .ToArray());
        }
    }
}
