using System.Globalization;

namespace PMS.Application.Features.Lookups.Queries.GetEnumValues
{
    public class GetEnumValuesResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static ICollection<GetEnumValuesResponse> ConvertEnumToList<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("Type given T must be an Enum");
            }

            var result = Enum.GetValues(typeof(T))
                             .Cast<T>()
                             .Select(x => new GetEnumValuesResponse
                             {
                                 Id = Convert.ToInt32(x),
                                 Name = x.ToString(new CultureInfo("en"))
                             })
                             .ToList()
                             .AsReadOnly();

            return result;
        }
    }
}
