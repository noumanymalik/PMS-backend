using PMS.Application.Common.Mappings;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.Employees.Queries.GetAll
{
    public class GetAllEmployeesResponse : IMapFrom<Employee>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
