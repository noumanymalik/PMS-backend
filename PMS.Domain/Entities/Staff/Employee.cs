using PMS.Domain.Entities.Base;
using PMS.Domain.Entities.Users;
using PMS.Domain.Enums;

namespace PMS.Domain.Entities.Staff
{
    public class Employee : BaseAuditableEntity<int>
    {
        // Basic Info
        public DateTime CreateDate { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public int? SupervisorId { get; set; }
        public Employee? Supervisor { get; set; }
        public ICollection<Employee> Subordinates { get; set; } = new List<Employee>();

        // Organization
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

        public int DesignationId { get; set; }
        public Designation Designation { get; set; } = null!;

        // Employment Info
        public DateTime JoiningDate { get; set; }
        public DateTime? LeavingDate { get; set; }

        public JobStatus JobStatus { get; set; }
        public Active Status { get; set; }   
        public Gender Gender { get; set; }
        public ApplicationUser User { get; set; }

        // Contact Info
        public string PhoneNo1 { get; set; } = null!;
        public string? PhoneNo2 { get; set; }

        public string EmailAddressCompany { get; set; } = null!;
        public string? EmailAddressPersonal { get; set; }

        public string? NextOfKin { get; set; }

        // Financial Info
        public string? BankName { get; set; }
        public string? AccountTittle { get; set; }
        public string? BankAccountNo { get; set; }
        public string? IBAN {  get; set; }

        // Salary Info
        public decimal BasicSalary { get; set; }
        public decimal KPI { get; set; }
        public decimal Incentive { get; set; }
        public SalaryType SalaryType { get; set; }

        // CNIC / Identity Info
        public string? CNICNo { get; set; } = null!;
        public string? FullName { get; set; } = null!;
        public string? FatherOrHusbandName { get; set; } = null!;
        public string? FamilyNo { get; set; }

        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime DateOfExpiry { get; set; }

        public string ExistingAddress { get; set; } = null!;
        public string PermanentAddress { get; set; } = null!;
    }
}
