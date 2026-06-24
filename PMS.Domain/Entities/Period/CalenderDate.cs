using PMS.Domain.Entities.Base;

namespace PMS.Domain.Entities.Period
{
    public class CalenderDate : BaseAuditableEntity<int>
    {
        public int CalenderWeekId { get; set; }
        public DateTime Date { get; set; }
        public CalenderWeek CalenderWeek { get; set; }

    }
}
