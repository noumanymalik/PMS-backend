namespace PMS.Application.Features.Loan.Queries.LoanRequestListbyEmployeeId
{
    public class LoanRequestListbyEmployeeIdResponse
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Code { get; set; }
        public string EmployeeName { get; set; }
        public decimal Amount { get; set; }
        public string Installment { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }
}
