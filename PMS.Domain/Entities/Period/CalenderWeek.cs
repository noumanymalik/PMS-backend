using PMS.Domain.Entities.Base;

namespace PMS.Domain.Entities.Period
{
    public class CalenderWeek : BaseAuditableEntity<int>
    {
        public int CalenderMonthId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CalenderMonth CalenderMonth { get; set; }
        public IList<CalenderDate> CalenderDates { get; set; }
    }
}
