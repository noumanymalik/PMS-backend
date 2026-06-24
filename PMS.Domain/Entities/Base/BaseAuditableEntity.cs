using PMS.Domain.Entities.Base.Interfaces;

namespace PMS.Domain.Entities.Base
{
    public abstract class BaseAuditableEntity<TId> : BaseEntity<TId>, IAuditableEntity
    {
        public int? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public bool IsArchived { get; set; } = false;
        public DateTime? DateArchived { get; set; }
        object IAuditableEntity.Id
        {
            get { return Id; }
        }

    }
}
