
namespace PMS.Persistence.Settings
{
    internal class ApplicationDbSettings
    {
        /// <summary>
        /// Specifies if migration should be attempted automatically during configuration.
        /// </summary>
        public bool? EnableAutoMigrate { get; init; }

        /// <summary>
        /// Specifies if seeding should be attempted automatically during configuration.
        /// </summary>
        public bool? EnableAutoSeed { get; init; }

        /// <summary>
        /// Specifies if deleted records should be skipped automatically.
        /// </summary>
        public bool? EnableSoftDeleteFilter { get; init; }
    }
}
