using PMS.Domain.Entities.Base;
using PMS.Domain.Entities.Period;
using PMS.Domain.Entities.Staff;

namespace PMS.Domain.Entities.Shedule
{
    public class Rota : BaseAuditableEntity<int>
    {
        public int EmployeeId { get; set; }
        public int ShiftId { get; set; }
        public Employee Employee { get; set; }
        public Shift Shift {  get; set; }
        public DateTime ShiftDate { get; set; }
        
        //public int CalenderDateId { get; set; }
        //public CalenderDate CalenderDate { get; set; }
    }
}
