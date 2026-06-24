
namespace PMS.Application.Features.Employees.Queries.GetList
{
    public class GetEmployeeListResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Supervisor { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Active { get; set; }
    }
}
