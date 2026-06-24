using PMS.Domain.Entities.Staff;

namespace PMS.Application.Interfaces.Repositories.DomainRepositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee, int>
    {
        Task<int> GetIdByEmployeeCodeAsync(string code, CancellationToken cancellationToken = default);
        public Task<string?> GetEmployeeCodeByEmployeeId(int employeeId, CancellationToken cancellationToken = default);
        public Task<List<Employee>> GetAllSupervisor(CancellationToken cancellationToken = default);
        public Task<List<Employee>> GetBySupervisorId(int SupervisorId, CancellationToken cancellationToken = default);

    }

    public interface IDepartmentRepository : IGenericRepository<Department, int>
    {
    }

    public interface IDesignationRepository : IGenericRepository<Designation, int>
    {
    }
}
