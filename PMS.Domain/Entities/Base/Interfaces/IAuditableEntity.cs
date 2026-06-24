namespace PMS.Domain.Entities.Base.Interfaces
{
    public interface IAuditableEntity
    {
        object Id { get; }
        int? CreatedBy { get; set; }
        DateTime? DateCreated { get; set; }
        int? UpdatedBy { get; set; }
        DateTime? DateUpdated { get; set; }
        bool IsArchived { get; set; }
        DateTime? DateArchived { get; set; }
    }
}
