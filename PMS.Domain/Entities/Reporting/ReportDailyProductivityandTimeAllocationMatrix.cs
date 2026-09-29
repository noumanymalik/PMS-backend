
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Domain.Entities.Reporting
{
    [NotMapped]
    public class ReportResultDailyProductivityandTimeAllocationMatrix
    {
        public DateTime AttendanceDate { get; set; }
        public string Code { get; set; }
        public string Names { get; set; }
        public string SessionDuration { get; set; }
        public string Meal { get; set; }
        public string Prayer { get; set; }
        public string Bio { get; set; }
        public string Coaching { get; set; }
        public string Training { get; set; }
        public string Huddle { get; set; }
        public string TotalBreak { get; set; }
        public string ProductionHours { get; set; }
    }
}
