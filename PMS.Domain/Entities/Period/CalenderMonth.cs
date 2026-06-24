using PMS.Domain.Entities.Base;

namespace PMS.Domain.Entities.Period
{
    public class CalenderMonth : BaseAuditableEntity<int>
    {
        public int CalenderYearId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public CalenderYear CalenderYear { get; set; }
        public IList<CalenderWeek> CalenderWeeks { get; set; }
    }
}
