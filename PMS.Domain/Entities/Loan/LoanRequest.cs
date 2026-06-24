using PMS.Domain.Entities.Base;
using PMS.Domain.Entities.Staff;
using PMS.Domain.Enums;

namespace PMS.Domain.Entities.Loan
{
    public class LoanRequest : BaseAuditableEntity<int>
    {
        public DateTime CreateDate { get; set; }
        public string Code { get; set; }
        public int EmployeeId { get; set; }
        public decimal Amount { get; set; }
        public LoanInstallment Installment { get; set; }
        public LoanApproveStatus Status { get; set; }
        public string Reason { get; set; }
        public Employee Employee { get; set; }
    }
}
