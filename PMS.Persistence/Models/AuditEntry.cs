using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using PMS.Domain.Entities.Auditing;

namespace PMS.Persistence.Models
{
    internal class AuditEntry
    {
        public AuditEntry(EntityEntry entity)
        {
            Entity = entity;
            PrimaryKey = int.Parse(entity.Entity.GetType().GetProperty("Id").GetValue(entity.Entity).ToString());
        }

        public EntityEntry Entity { get; }

        public int UserId { get; set; }

        public string TableName { get; set; }

        public int PrimaryKey { get; }

        public Dictionary<string, object> OldValues { get; } = new();

        public Dictionary<string, object> NewValues { get; } = new();

        public AuditType AuditType { get; set; }

        public List<string> ChangedColumns { get; } = new();

        public AuditLog ToAudit()
        {
            var audit = new AuditLog();
            audit.UserId = UserId;
            audit.Type = AuditType.ToString();
            audit.TableName = TableName;
            audit.DateTime = DateTime.UtcNow;
            audit.PrimaryKey = PrimaryKey;
            audit.OldValues = OldValues.Count == 0 ? string.Empty : JsonConvert.SerializeObject(OldValues);
            audit.NewValues = NewValues.Count == 0 ? string.Empty : JsonConvert.SerializeObject(NewValues);
            audit.AffectedColumns = ChangedColumns.Count == 0 ? string.Empty : JsonConvert.SerializeObject(ChangedColumns);
            return audit;
        }
    }
}

