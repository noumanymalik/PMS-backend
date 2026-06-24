using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace PMS.Persistence.Extensions
{
    public static class ConfigurationExtensions
    {
        const string ConfigMissingErrorMessage = "Options type of '{0}' was requested as required, but the corresponding section was not found in configuration. Make sure one of your configuration sources contains this section.";

        public static T GetConfigOptions<T>(this IConfiguration configuration, bool requiredToExistInConfiguration = false) where T : class, new()
        {
            var bound = configuration.GetSection(typeof(T).Name).Get<T>();

            if (bound is null && requiredToExistInConfiguration)
                throw new InvalidOperationException(string.Format(ConfigMissingErrorMessage, typeof(T).Name));

            bound ??= new T();
            Validator.ValidateObject(bound, new ValidationContext(bound), validateAllProperties: true);

            return bound;
        }
    }
}
