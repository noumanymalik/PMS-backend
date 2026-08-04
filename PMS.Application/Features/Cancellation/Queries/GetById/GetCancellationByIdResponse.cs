namespace PMS.Application.Features.Cancellation.Queries.GetById
{
    public class GetCancellationByIdResponse
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Remarks { get; set; }
    }
}
