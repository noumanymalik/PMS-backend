namespace PMS.Application.Features.Employees.Queries.GetHierarchy
{
    public class GetEmployeeHierarchyResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Designation { get; set; }
        public List<GetEmployeeHierarchyResponse> Children { get; set; } = new();
    }
}
