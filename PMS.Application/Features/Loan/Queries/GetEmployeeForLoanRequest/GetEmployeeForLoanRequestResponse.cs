namespace PMS.Application.Features.Loan.Queries.GetEmployeeForLoanRequest
{
    public class GetEmployeeForLoanRequestResponse
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime JoiningDate { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Designation { get; set; }
        public DateTime? LastLoanDate { get; set; }
        public Get90DaysLoan NinetyDaysLoan { get; set; }

        public enum Get90DaysLoan
        {
            Yes = 1,
            No = 2,
        }
    }
}
