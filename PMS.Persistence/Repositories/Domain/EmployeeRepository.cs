using Microsoft.EntityFrameworkCore;
using PMS.Application.Interfaces.Repositories.DomainRepositories;
using PMS.Domain.Entities.Staff;
using PMS.Persistence.Context;

namespace PMS.Persistence.Repositories.Domain
{
    public class EmployeeRepository : GenericRepository<Employee, int>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Employee>> GetAllSupervisor(CancellationToken cancellationToken = default)
        {
            return await DBContext.Employee
                .Where(e => DBContext.Employee.Any(sub => sub.SupervisorId == e.Id))
                .ToListAsync(cancellationToken);
        }

        public async Task<string?> GetEmployeeCodeByEmployeeId(int employeeId, CancellationToken cancellationToken = default)
        {
            return await DBContext.Employee
                .AsNoTracking()
                .Where(x => x.Id == employeeId)
                .Select(x => x.Code)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<int> GetIdByEmployeeCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return (int)await DBContext.Employee
                .AsNoTracking()
                .Where(x => x.Code == code)
                .Select(x => (int?)x.Id)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<List<Employee>> GetBySupervisorId(int supervisorId, CancellationToken cancellationToken = default)
        {
            return await DBContext.Employee
                .Where(x => x.SupervisorId == supervisorId)
                .ToListAsync(cancellationToken);
        }
    }

    public class CorrectiveActionRepository : GenericRepository<CorrectiveAction, int>, ICorrectiveActionRepository
    {
        public CorrectiveActionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

    public class DepartmentRepository : GenericRepository<Department, int>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

    public class DesignationRepository : GenericRepository<Designation, int>, IDesignationRepository
    {
        public DesignationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }



}
