namespace PMS.Application.Features.Loan.Queries.GetLoanRequestDetailbyId
{
    public class GetLoanRequestDetailbyIdResponse
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string EmployeeName { get; set; }
        public DateTime JoiningDate { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Designation { get; set; }
        public Decimal Amount { get; set; }
        public string Installment { get; set; }
        public DateTime? LastLoanDate { get; set; }
        public string Reason { get; set; }

        public Avail90DaysLoan NinetyDaysLoan { get; set; }

        public enum Avail90DaysLoan
        {
            Yes = 1,
            No = 2,
        }
    }
}
