namespace PMS.Domain.Entities.Auditing
{
    public class AuditLog
    {
        public int Id { get; set; }
        public int UserId { get; set; } = 0;
        public string Type { get; set; } = AuditType.None.ToString();
        public string TableName { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string AffectedColumns { get; set; }
        public int PrimaryKey { get; set; }
    }

    public enum AuditType
    {
        None = 0,
        Create = 1,
        Update = 2,
        Delete = 3
    }
}
