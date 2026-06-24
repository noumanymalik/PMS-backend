using PMS.Application.Features.Lookups.Queries.GetEnumValues;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;

namespace PMS.Application.Extensions
{
    public static class EnumExtensions
    {
        public static TEnum ConvertTo<TEnum>(this Enum source)
        {
            try
            {
                return (TEnum)Enum.Parse(typeof(TEnum), source.ToString(), ignoreCase: true);
            }
            catch (ArgumentException aex)
            {
                throw new InvalidOperationException
                (
                    $"Could not convert {source.GetType().ToString()} [{source.ToString()}] to {typeof(TEnum).ToString()}", aex
                );
            }
        }

        public static List<string> GetEnumMemberValues<T>() where T : struct, IConvertible
        {
            List<string> list = new List<string>();
            var members = typeof(T)
                 .GetTypeInfo()
                 .DeclaredMembers;
            foreach (var member in members)
            {
                var val = member?.GetCustomAttribute<EnumMemberAttribute>(false)?.Value;
                if (!string.IsNullOrEmpty(val))
                    list.Add(val);
            }

            return list;
        }

        public static ICollection<GetEnumValuesResponse> GetEnumValuesFromName(string enumName)
        {
            Type type = Type.GetType(enumName);

            if (type == null)
                throw new ArgumentException($"No enum value with string value '{enumName}' found");

            if (!type.IsEnum)
                throw new ArgumentException("T must be an enum type");

            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
            var results = fields
                             .Select(x => new GetEnumValuesResponse
                             {
                                 Id = Convert.ToInt32(x.GetRawConstantValue()),
                                 Name = x.Name.ToString(new CultureInfo("en"))
                             })
                             .ToList()
                             .AsReadOnly();
            
            return results;
        }
    }
}
