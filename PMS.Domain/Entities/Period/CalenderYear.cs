using PMS.Domain.Entities.Base;

namespace PMS.Domain.Entities.Period
{
    public class CalenderYear : BaseAuditableEntity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public IList<CalenderMonth> CalenderMonths { get; set; }

    }
}
