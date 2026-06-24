using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Interfaces.Services;
using PMS.Domain.Entities.Period;
using PMS.Domain.Entities.Shedule;
using PMS.Domain.Entities.Staff;
using PMS.Domain.Entities.Users;
using PMS.Domain.Enums;
using PMS.Persistence.Context;
using PMS.Persistence.Extensions;
using PMS.Persistence.Settings;
using System;

namespace PMS.Persistence.Seed
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public DatabaseInitializer(ApplicationDbContext context, IConfiguration configuration, ILogger<DatabaseInitializer> logger, IUnitOfWork unitOfWork)
        {
            _dbContext = context;
            _configuration = configuration;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task SeedAsync()
        {
            var settings = _configuration.GetConfigOptions<ApplicationDbSettings>();
            try
            {
                if (settings.EnableAutoMigrate == true && _dbContext.Database.IsSqlServer())
                    await _dbContext.Database.MigrateAsync().ConfigureAwait(false);

                if (settings.EnableAutoSeed == true)
                {
                    await SeedCalenderYear();
                    await SeedCalenderMonth();
                    await SeedCalenderWeek();
                    await SeedCalenderDate();
                    await SeedDepartment();
                    await SeedDesignation();
                    await SeedShift();
                    await SeedTestEmployee();
                    await SeedPermissions();
                    await SeedUsers();
                    await SeedRoles();
                    await RolePermission();
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "An error occurred while migrating or seeding the database.");
                throw;
            }
        }

        private async Task SeedTestEmployee()
        {
            if (!_dbContext.Employee.Any())
            {
                var employee = new List<Employee>
                {
                    new Employee { CreateDate = DateTime.Now, Code = "8888", Name = "Ahsan Saeed", DepartmentId = 3, DesignationId = 36, JoiningDate = new DateTime(2024, 11, 18), LeavingDate = null, Gender = (Gender)(int)Gender.Male, Status = (Active)(int)Active.Active, },
                    new Employee { CreateDate = DateTime.Now, Code = "9999", Name = "Muhammad Dawood", SupervisorId = 1, DepartmentId = 3, DesignationId = 35, JoiningDate = new DateTime(2024, 11, 03), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },
                    new Employee { CreateDate = DateTime.Now, Code = "1351", Name = "Mehroz Sultan", SupervisorId = 2, DepartmentId = 3, DesignationId = 33, JoiningDate = new DateTime(2025, 08, 22), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },
                    new Employee { CreateDate = DateTime.Now, Code = "1007", Name = "Saad Ullah", SupervisorId = 3, DepartmentId = 3, DesignationId = 7, JoiningDate = new DateTime(2025, 04, 25), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },
                    new Employee { CreateDate = DateTime.Now, Code = "1050", Name = "Junaid Manzoor", SupervisorId = 3, DepartmentId = 3, DesignationId = 7, JoiningDate = new DateTime(2026, 01, 29), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },
                    new Employee { CreateDate = DateTime.Now, Code = "1049", Name = "Haider Imam", SupervisorId = 4, DepartmentId = 3, DesignationId = 3, JoiningDate = new DateTime(2025, 08, 06), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },
                    new Employee { CreateDate = DateTime.Now, Code = "1097", Name = "Huda Tariq", SupervisorId = 4, DepartmentId = 3, DesignationId = 3, JoiningDate = new DateTime(2025, 08, 06), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },
                    new Employee { CreateDate = DateTime.Now, Code = "1055", Name = "Khawar Qureshi", SupervisorId = 5, DepartmentId = 3, DesignationId = 3, JoiningDate = new DateTime(2025, 08, 06), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },
                    new Employee { CreateDate = DateTime.Now, Code = "1058", Name = "Syed Zain", SupervisorId = 5, DepartmentId = 3, DesignationId = 3, JoiningDate = new DateTime(2025, 08, 06), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },


                    new Employee { CreateDate = DateTime.Now, Code = "1189", Name = "Abdullah Imtiaz", SupervisorId = 6, DepartmentId = 1, DesignationId = 2, JoiningDate = new DateTime(2025, 07, 25), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },
                    new Employee { CreateDate = DateTime.Now, Code = "1333", Name = "Abdulrehman Qureshi", SupervisorId = 6, DepartmentId = 1, DesignationId = 2, JoiningDate = new DateTime(2026, 04, 14), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },
                    new Employee { CreateDate = DateTime.Now, Code = "1151", Name = "Abdulrehman Shoukat", SupervisorId = 6, DepartmentId = 1, DesignationId = 2,  JoiningDate = new DateTime(2025, 04, 24), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },
                    new Employee { CreateDate = DateTime.Now, Code = "1191", Name = "Abdulrehman Tariq", SupervisorId = 6, DepartmentId = 1, DesignationId = 2, JoiningDate = new DateTime(2025, 07, 25), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },

                    new Employee { CreateDate = DateTime.Now, Code = "1102", Name = "Musatafa Maqsood", SupervisorId = 7, DepartmentId = 1, DesignationId = 2, JoiningDate = new DateTime(2026, 06, 01), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },
                    new Employee { CreateDate = DateTime.Now, Code = "1072", Name = "Rana Shujat", SupervisorId = 7, DepartmentId = 1, DesignationId = 2, JoiningDate = new DateTime(2026, 02, 14), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },
                    new Employee { CreateDate = DateTime.Now, Code = "1217", Name = "Abdul Moeez", SupervisorId = 7, DepartmentId = 1, DesignationId = 2,  JoiningDate = new DateTime(2025, 04, 24), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },
                    new Employee { CreateDate = DateTime.Now, Code = "1047", Name = "Abdul Samad", SupervisorId = 7, DepartmentId = 1, DesignationId = 2, JoiningDate = new DateTime(2025, 07, 25), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },

                    new Employee { CreateDate = DateTime.Now, Code = "1275", Name = "Daniyal Ahmad", SupervisorId = 8, DepartmentId = 1, DesignationId = 2, JoiningDate = new DateTime(2025, 07, 25), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },
                    new Employee { CreateDate = DateTime.Now, Code = "1163", Name = "Daniyal Waseem", SupervisorId = 8, DepartmentId = 1, DesignationId = 2, JoiningDate = new DateTime(2026, 02, 14), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },
                    new Employee { CreateDate = DateTime.Now, Code = "1202", Name = "Ejaz Hassan", SupervisorId = 8, DepartmentId = 1, DesignationId = 2,  JoiningDate = new DateTime(2025, 04, 24), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },
                    new Employee { CreateDate = DateTime.Now, Code = "1316", Name = "Eleazar Saleem", SupervisorId = 8, DepartmentId = 1, DesignationId = 2, JoiningDate = new DateTime(2025, 07, 25), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },

                    new Employee { CreateDate = DateTime.Now, Code = "1129", Name = "Jamal Mujtaba", SupervisorId = 9, DepartmentId = 1, DesignationId = 2, JoiningDate = new DateTime(2025, 07, 25), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },
                    new Employee { CreateDate = DateTime.Now, Code = "1239", Name = "Jazym Rashid", SupervisorId = 9, DepartmentId = 1, DesignationId = 2, JoiningDate = new DateTime(2026, 02, 14), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },
                    new Employee { CreateDate = DateTime.Now, Code = "1138", Name = "Karoline Arif", SupervisorId = 9, DepartmentId = 1, DesignationId = 2,  JoiningDate = new DateTime(2025, 04, 24), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },
                    new Employee { CreateDate = DateTime.Now, Code = "1112", Name = "Khawaja Usama", SupervisorId = 9, DepartmentId = 1, DesignationId = 2, JoiningDate = new DateTime(2025, 07, 25), LeavingDate = null, Gender = (Gender)(int)Gender.Male,Status = (Active)(int)Active.Active, },

               };

                await _dbContext.Employee.AddRangeAsync(employee);
                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task SeedDepartment()
        {
            if (!_dbContext.Department.Any())
            {
                var department = new List<Department>
                {
                    new Department { Code = "DT-01", Name = "Sales", },
                    new Department { Code = "DT-02", Name = "Quality Assurance", },
                    new Department { Code = "DT-03", Name = "Operations", },
                    new Department { Code = "DT-04", Name = "Administration", },
                    new Department { Code = "DT-05", Name = "Admin & Accounts", },
                    new Department { Code = "DT-06", Name = "Information Technology", },
                    new Department { Code = "DT-07", Name = "Coordinator", },
                    new Department { Code = "DT-08", Name = "Human Resource", },
                    new Department { Code = "DT-09", Name = "Driver", },
                    new Department { Code = "DT-10", Name = "Legal", },
                    new Department { Code = "DT-11", Name = "Business Intelligence", },
                    new Department { Code = "DT-12", Name = "Advisory", },
                    new Department { Code = "DT-13", Name = "Higher Management", },
                };

                await _dbContext.Department.AddRangeAsync(department);
                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task SeedDesignation()
        {
            if (!_dbContext.Designation.Any())
            {
                var designation = new List<Designation>
                {
                    new Designation { Code = "DS-01", Name = "T Mobile", },
                    new Designation { Code = "DS-02", Name = "P50", },
                    new Designation { Code = "DS-03", Name = "Supervisor", },
                    new Designation { Code = "DS-04", Name = "QAE", },
                    new Designation { Code = "DS-05", Name = "QAE Supervisor", },
                    new Designation { Code = "DS-06", Name = "Operations Trainer", },
                    new Designation { Code = "DS-07", Name = "Lead Supervisor", },
                    new Designation { Code = "DS-08", Name = "Operations Executive", },
                    new Designation { Code = "DS-09", Name = "SME (Subject Matter Expert)", },
                    new Designation { Code = "DS-10", Name = "WFM Executive", },
                    new Designation { Code = "DS-11", Name = "Real Time Analys", },
                    new Designation { Code = "DS-12", Name = "Janitorial Services", },
                    new Designation { Code = "DS-13", Name = "Office Boy", },
                    new Designation { Code = "DS-14", Name = "Chef", },
                    new Designation { Code = "DS-15", Name = "Security Guard", },
                    new Designation { Code = "DS-16", Name = "Electrician", },
                    new Designation { Code = "DS-17", Name = "Accounts Officer", },
                    new Designation { Code = "DS-18", Name = "IT Executive", },
                    new Designation { Code = "DS-19", Name = "IT Manager", },
                    new Designation { Code = "DS-20", Name = "IT Consultant", },
                    new Designation { Code = "DS-21", Name = "Coordinator to CEO", },
                    new Designation { Code = "DS-22", Name = "Marketing Executive", },
                    new Designation { Code = "DS-23", Name = "HR Executive", },
                    new Designation { Code = "DS-24", Name = "CEO Driver", },
                    new Designation { Code = "DS-25", Name = "Chef", },
                    new Designation { Code = "DS-26", Name = "Cook", },
                    new Designation { Code = "DS-27", Name = "Lead Admin & Facilitator", },
                    new Designation { Code = "DS-28", Name = "CT Driver", },
                    new Designation { Code = "DS-29", Name = "CT Manager", },
                    new Designation { Code = "DS-30", Name = "Legal Counsel", },
                    new Designation { Code = "DS-31", Name = "Data Analyst", },
                    new Designation { Code = "DS-32", Name = "Lead Data Analyst", },
                    new Designation { Code = "DS-33", Name = "Manager Operations", },
                    new Designation { Code = "DS-34", Name = "Business Advisor", },
                    new Designation { Code = "DS-35", Name = "Managing Director", },
                    new Designation { Code = "DS-36", Name = "CEO", },
                };

                await _dbContext.Designation.AddRangeAsync(designation);
                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task SeedCalenderYear()
        {
            if (!_dbContext.CalenderYear.Any())
            {
                var calenderYear = new List<CalenderYear>
                {
                    new CalenderYear {
                    Code = "CY26",
                    Name = "CY 26",
                    StartDate = new DateTime(2025, 12, 28),
                    EndDate = new DateTime(2026, 12, 27),
                    IsActive = true
                    },

                };

                await _dbContext.CalenderYear.AddRangeAsync(calenderYear);
                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task SeedCalenderMonth()
        {
            if (!_dbContext.CalenderMonth.Any())
            {
                var calenderMonth = new List<CalenderMonth>
                {
                    new CalenderMonth {
                    CalenderYearId = 1,
                    Code = "CM01",
                    Name = "January 26",
                    StartDate = new DateTime(2025, 12, 28),
                    EndDate = new DateTime(2026, 01, 27),
                    IsActive = false
                    },
                    new CalenderMonth {
                    CalenderYearId = 1,
                    Code = "CM02",
                    Name = "February 26",
                    StartDate = new DateTime(2026, 01, 28),
                    EndDate = new DateTime(2026, 02, 27),
                    IsActive = false
                    },
                    new CalenderMonth {
                    CalenderYearId = 1,
                    Code = "CM03",
                    Name = "March 26",
                    StartDate = new DateTime(2026, 02, 28),
                    EndDate = new DateTime(2026, 03, 27),
                    IsActive = true
                    },
                    new CalenderMonth {
                    CalenderYearId = 1,
                    Code = "CM04",
                    Name = "April 26",
                    StartDate = new DateTime(2026, 03, 28),
                    EndDate = new DateTime(2026, 04, 27),
                    IsActive = false
                    },
                    new CalenderMonth {
                    CalenderYearId = 1,
                    Code = "CM05",
                    Name = "May 26",
                    StartDate = new DateTime(2026, 04, 28),
                    EndDate = new DateTime(2026, 05, 27),
                    IsActive = false
                    },
                    new CalenderMonth {
                    CalenderYearId = 1,
                    Code = "CM06",
                    Name = "June 26",
                    StartDate = new DateTime(2026, 05, 28),
                    EndDate = new DateTime(2026, 06, 27),
                    IsActive = false
                    },
                    new CalenderMonth {
                    CalenderYearId = 1,
                    Code = "CM07",
                    Name = "July 26",
                    StartDate = new DateTime(2026, 06, 28),
                    EndDate = new DateTime(2026, 07, 27),
                    IsActive = false
                    },
                    new CalenderMonth {
                    CalenderYearId = 1,
                    Code = "CM08",
                    Name = "August 26",
                    StartDate = new DateTime(2026, 07, 28),
                    EndDate = new DateTime(2026, 08, 27),
                    IsActive = false
                    },
                    new CalenderMonth {
                    CalenderYearId = 1,
                    Code = "CM09",
                    Name = "September 26",
                    StartDate = new DateTime(2026, 08, 28),
                    EndDate = new DateTime(2026, 09, 27),
                    IsActive = false
                    },
                    new CalenderMonth {
                    CalenderYearId = 1,
                    Code = "CM10",
                    Name = "October 26",
                    StartDate = new DateTime(2026, 09, 28),
                    EndDate = new DateTime(2026, 10, 27),
                    IsActive = false
                    },
                    new CalenderMonth {
                    CalenderYearId = 1,
                    Code = "CM11",
                    Name = "November 26",
                    StartDate = new DateTime(2026, 10, 28),
                    EndDate = new DateTime(2026, 11, 27),
                    IsActive = false
                    },
                    new CalenderMonth {
                    CalenderYearId = 1,
                    Code = "CM12",
                    Name = "December 26",
                    StartDate = new DateTime(2026, 11, 28),
                    EndDate = new DateTime(2026, 12, 27),
                    IsActive = false
                    },

                };

                await _dbContext.CalenderMonth.AddRangeAsync(calenderMonth);
                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task SeedCalenderWeek()
        {
            if (!_dbContext.CalenderWeek.Any())
            {
                var calenderWeek = new List<CalenderWeek>
                {
                    new CalenderWeek {CalenderMonthId = 1,  Code = "CW01", Name = "CW 01", StartDate = new DateTime(2025, 12, 29), EndDate = new DateTime(2026, 01, 04) },
                    new CalenderWeek {CalenderMonthId = 1,  Code = "CW02", Name = "CW 02", StartDate = new DateTime(2026, 01, 05), EndDate = new DateTime(2026, 01, 11) },
                    new CalenderWeek {CalenderMonthId = 1,  Code = "CW03", Name = "CW 03", StartDate = new DateTime(2026, 01, 12), EndDate = new DateTime(2026, 01, 18) },
                    new CalenderWeek {CalenderMonthId = 1,  Code = "CW04", Name = "CW 04", StartDate = new DateTime(2026, 01, 19), EndDate = new DateTime(2026, 01, 25) },
                    new CalenderWeek {CalenderMonthId = 1,  Code = "CW05", Name = "CW 05", StartDate = new DateTime(2026, 01, 26), EndDate = new DateTime(2026, 02, 01) },
                    new CalenderWeek {CalenderMonthId = 2,  Code = "CW06", Name = "CW 06", StartDate = new DateTime(2026, 02, 02), EndDate = new DateTime(2026, 02, 08) },
                    new CalenderWeek {CalenderMonthId = 2,  Code = "CW07", Name = "CW 07", StartDate = new DateTime(2026, 02, 09), EndDate = new DateTime(2026, 02, 15) },
                    new CalenderWeek {CalenderMonthId = 2,  Code = "CW08", Name = "CW 08", StartDate = new DateTime(2026, 02, 16), EndDate = new DateTime(2026, 02, 22) },
                    new CalenderWeek {CalenderMonthId = 2,  Code = "CW09", Name = "CW 09", StartDate = new DateTime(2026, 02, 23), EndDate = new DateTime(2026, 03, 01) },
                    new CalenderWeek {CalenderMonthId = 3,  Code = "CW10", Name = "CW 10", StartDate = new DateTime(2026, 03, 02), EndDate = new DateTime(2026, 03, 08) },
                    new CalenderWeek {CalenderMonthId = 3,  Code = "CW11", Name = "CW 11", StartDate = new DateTime(2026, 03, 09), EndDate = new DateTime(2026, 03, 15) },
                    new CalenderWeek {CalenderMonthId = 3,  Code = "CW12", Name = "CW 12", StartDate = new DateTime(2026, 03, 16), EndDate = new DateTime(2026, 03, 22) },
                    new CalenderWeek {CalenderMonthId = 3,  Code = "CW13", Name = "CW 13", StartDate = new DateTime(2026, 03, 23), EndDate = new DateTime(2026, 03, 29) },
                    new CalenderWeek {CalenderMonthId = 3,  Code = "CW14", Name = "CW 14", StartDate = new DateTime(2026, 03, 30), EndDate = new DateTime(2026, 04, 05) },
                    new CalenderWeek {CalenderMonthId = 4,  Code = "CW15", Name = "CW 15", StartDate = new DateTime(2026, 04, 06), EndDate = new DateTime(2026, 04, 12) },
                    new CalenderWeek {CalenderMonthId = 4,  Code = "CW16", Name = "CW 16", StartDate = new DateTime(2026, 04, 13), EndDate = new DateTime(2026, 04, 19) },
                    new CalenderWeek {CalenderMonthId = 4,  Code = "CW17", Name = "CW 17", StartDate = new DateTime(2026, 04, 20), EndDate = new DateTime(2026, 04, 26) },
                    new CalenderWeek {CalenderMonthId = 4,  Code = "CW18", Name = "CW 18", StartDate = new DateTime(2026, 04, 27), EndDate = new DateTime(2026, 05, 03) },
                    new CalenderWeek {CalenderMonthId = 5,  Code = "CW19", Name = "CW 19", StartDate = new DateTime(2026, 05, 04), EndDate = new DateTime(2026, 05, 10) },
                    new CalenderWeek {CalenderMonthId = 5,  Code = "CW20", Name = "CW 20", StartDate = new DateTime(2026, 05, 11), EndDate = new DateTime(2026, 05, 17) },
                    new CalenderWeek {CalenderMonthId = 5,  Code = "CW21", Name = "CW 21", StartDate = new DateTime(2026, 05, 18), EndDate = new DateTime(2026, 05, 24) },
                    new CalenderWeek {CalenderMonthId = 5,  Code = "CW22", Name = "CW 22", StartDate = new DateTime(2026, 05, 25), EndDate = new DateTime(2026, 05, 31) },
                    new CalenderWeek {CalenderMonthId = 6,  Code = "CW23", Name = "CW 23", StartDate = new DateTime(2026, 06, 01), EndDate = new DateTime(2026, 06, 07) },
                    new CalenderWeek {CalenderMonthId = 6,  Code = "CW24", Name = "CW 24", StartDate = new DateTime(2026, 06, 08), EndDate = new DateTime(2026, 06, 14) },
                    new CalenderWeek {CalenderMonthId = 6,  Code = "CW25", Name = "CW 25", StartDate = new DateTime(2026, 06, 15), EndDate = new DateTime(2026, 06, 21) },
                    new CalenderWeek {CalenderMonthId = 6,  Code = "CW26", Name = "CW 26", StartDate = new DateTime(2026, 06, 22), EndDate = new DateTime(2026, 06, 28) },
                    new CalenderWeek {CalenderMonthId = 7,  Code = "CW27", Name = "CW 27", StartDate = new DateTime(2026, 06, 29), EndDate = new DateTime(2026, 07, 05) },
                    new CalenderWeek {CalenderMonthId = 7,  Code = "CW28", Name = "CW 28", StartDate = new DateTime(2026, 07, 06), EndDate = new DateTime(2026, 07, 12) },
                    new CalenderWeek {CalenderMonthId = 7,  Code = "CW29", Name = "CW 29", StartDate = new DateTime(2026, 07, 13), EndDate = new DateTime(2026, 07, 19) },
                    new CalenderWeek {CalenderMonthId = 7,  Code = "CW30", Name = "CW 30", StartDate = new DateTime(2026, 07, 20), EndDate = new DateTime(2026, 07, 26) },
                    new CalenderWeek {CalenderMonthId = 7,  Code = "CW31", Name = "CW 31", StartDate = new DateTime(2026, 07, 27), EndDate = new DateTime(2026, 08, 02) },
                    new CalenderWeek {CalenderMonthId = 8,  Code = "CW32", Name = "CW 32", StartDate = new DateTime(2026, 08, 03), EndDate = new DateTime(2026, 08, 09) },
                    new CalenderWeek {CalenderMonthId = 8,  Code = "CW33", Name = "CW 33", StartDate = new DateTime(2026, 08, 10), EndDate = new DateTime(2026, 08, 16) },
                    new CalenderWeek {CalenderMonthId = 8,  Code = "CW34", Name = "CW 34", StartDate = new DateTime(2026, 08, 17), EndDate = new DateTime(2026, 08, 23) },
                    new CalenderWeek {CalenderMonthId = 8,  Code = "CW35", Name = "CW 35", StartDate = new DateTime(2026, 08, 24), EndDate = new DateTime(2026, 08, 30) },
                    new CalenderWeek {CalenderMonthId = 9,  Code = "CW36", Name = "CW 36", StartDate = new DateTime(2026, 08, 31), EndDate = new DateTime(2026, 09, 06) },
                    new CalenderWeek {CalenderMonthId = 9,  Code = "CW37", Name = "CW 37", StartDate = new DateTime(2026, 09, 07), EndDate = new DateTime(2026, 09, 13) },
                    new CalenderWeek {CalenderMonthId = 9,  Code = "CW38", Name = "CW 38", StartDate = new DateTime(2026, 09, 14), EndDate = new DateTime(2026, 09, 20) },
                    new CalenderWeek {CalenderMonthId = 9,  Code = "CW39", Name = "CW 39", StartDate = new DateTime(2026, 09, 21), EndDate = new DateTime(2026, 09, 27) },
                    new CalenderWeek {CalenderMonthId = 10,  Code = "CW40", Name = "CW 40", StartDate = new DateTime(2026, 09, 28), EndDate = new DateTime(2026, 10, 04) },
                    new CalenderWeek {CalenderMonthId = 10,  Code = "CW41", Name = "CW 41", StartDate = new DateTime(2026, 10, 05), EndDate = new DateTime(2026, 10, 11) },
                    new CalenderWeek {CalenderMonthId = 10,  Code = "CW42", Name = "CW 42", StartDate = new DateTime(2026, 10, 12), EndDate = new DateTime(2026, 10, 18) },
                    new CalenderWeek {CalenderMonthId = 10,  Code = "CW43", Name = "CW 43", StartDate = new DateTime(2026, 10, 19), EndDate = new DateTime(2026, 10, 25) },
                    new CalenderWeek {CalenderMonthId = 10,  Code = "CW44", Name = "CW 44", StartDate = new DateTime(2026, 10, 26), EndDate = new DateTime(2026, 11, 01) },
                    new CalenderWeek {CalenderMonthId = 11,  Code = "CW45", Name = "CW 45", StartDate = new DateTime(2026, 11, 02), EndDate = new DateTime(2026, 11, 08) },
                    new CalenderWeek {CalenderMonthId = 11,  Code = "CW46", Name = "CW 46", StartDate = new DateTime(2026, 11, 09), EndDate = new DateTime(2026, 11, 15) },
                    new CalenderWeek {CalenderMonthId = 11,  Code = "CW47", Name = "CW 47", StartDate = new DateTime(2026, 11, 16), EndDate = new DateTime(2026, 11, 22) },
                    new CalenderWeek {CalenderMonthId = 11,  Code = "CW48", Name = "CW 48", StartDate = new DateTime(2026, 11, 23), EndDate = new DateTime(2026, 11, 29) },
                    new CalenderWeek {CalenderMonthId = 12,  Code = "CW49", Name = "CW 49", StartDate = new DateTime(2026, 11, 30), EndDate = new DateTime(2026, 12, 06) },
                    new CalenderWeek {CalenderMonthId = 12,  Code = "CW50", Name = "CW 50", StartDate = new DateTime(2026, 12, 07), EndDate = new DateTime(2026, 12, 13) },
                    new CalenderWeek {CalenderMonthId = 12,  Code = "CW51", Name = "CW 51", StartDate = new DateTime(2026, 12, 14), EndDate = new DateTime(2026, 12, 20) },
                    new CalenderWeek {CalenderMonthId = 12,  Code = "CW52", Name = "CW 52", StartDate = new DateTime(2026, 12, 21), EndDate = new DateTime(2026, 12, 27) },
                };

                await _dbContext.CalenderWeek.AddRangeAsync(calenderWeek);
                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task SeedCalenderDate()
        {
            if (!_dbContext.CalenderDate.Any())
            {
                var calenderDate = new List<CalenderDate>
                {
                    new CalenderDate {CalenderWeekId = 1,  Date = new DateTime(2025, 12, 29) },
                    new CalenderDate {CalenderWeekId = 1,  Date = new DateTime(2025, 12, 30) },
                    new CalenderDate {CalenderWeekId = 1,  Date = new DateTime(2025, 12, 31) },
                    new CalenderDate {CalenderWeekId = 1,  Date = new DateTime(2026, 01, 01) },
                    new CalenderDate {CalenderWeekId = 1,  Date = new DateTime(2026, 01, 02) },
                    new CalenderDate {CalenderWeekId = 1,  Date = new DateTime(2026, 01, 03) },
                    new CalenderDate {CalenderWeekId = 1,  Date = new DateTime(2026, 01, 04) },
                    new CalenderDate {CalenderWeekId = 2,  Date = new DateTime(2026, 01, 05) },
                    new CalenderDate {CalenderWeekId = 2,  Date = new DateTime(2026, 01, 06) },
                    new CalenderDate {CalenderWeekId = 2,  Date = new DateTime(2026, 01, 07) },
                    new CalenderDate {CalenderWeekId = 2,  Date = new DateTime(2026, 01, 08) },
                    new CalenderDate {CalenderWeekId = 2,  Date = new DateTime(2026, 01, 09) },
                    new CalenderDate {CalenderWeekId = 2,  Date = new DateTime(2026, 01, 10) },
                    new CalenderDate {CalenderWeekId = 2,  Date = new DateTime(2026, 01, 11) },
                    new CalenderDate {CalenderWeekId = 3,  Date = new DateTime(2026, 01, 12) },
                    new CalenderDate {CalenderWeekId = 3,  Date = new DateTime(2026, 01, 13) },
                    new CalenderDate {CalenderWeekId = 3,  Date = new DateTime(2026, 01, 14) },
                    new CalenderDate {CalenderWeekId = 3,  Date = new DateTime(2026, 01, 15) },
                    new CalenderDate {CalenderWeekId = 3,  Date = new DateTime(2026, 01, 16) },
                    new CalenderDate {CalenderWeekId = 3,  Date = new DateTime(2026, 01, 17) },
                    new CalenderDate {CalenderWeekId = 3,  Date = new DateTime(2026, 01, 18) },
                    new CalenderDate {CalenderWeekId = 4,  Date = new DateTime(2026, 01, 19) },
                    new CalenderDate {CalenderWeekId = 4,  Date = new DateTime(2026, 01, 20) },
                    new CalenderDate {CalenderWeekId = 4,  Date = new DateTime(2026, 01, 21) },
                    new CalenderDate {CalenderWeekId = 4,  Date = new DateTime(2026, 01, 22) },
                    new CalenderDate {CalenderWeekId = 4,  Date = new DateTime(2026, 01, 23) },
                    new CalenderDate {CalenderWeekId = 4,  Date = new DateTime(2026, 01, 24) },
                    new CalenderDate {CalenderWeekId = 4,  Date = new DateTime(2026, 01, 25) },
                    new CalenderDate {CalenderWeekId = 5,  Date = new DateTime(2026, 01, 26) },
                    new CalenderDate {CalenderWeekId = 5,  Date = new DateTime(2026, 01, 27) },
                    new CalenderDate {CalenderWeekId = 5,  Date = new DateTime(2026, 01, 28) },
                    new CalenderDate {CalenderWeekId = 5,  Date = new DateTime(2026, 01, 29) },
                    new CalenderDate {CalenderWeekId = 5,  Date = new DateTime(2026, 01, 30) },
                    new CalenderDate {CalenderWeekId = 5,  Date = new DateTime(2026, 01, 31) },
                    new CalenderDate {CalenderWeekId = 5,  Date = new DateTime(2026, 02, 01) },
                    new CalenderDate {CalenderWeekId = 6,  Date = new DateTime(2026, 02, 02) },
                    new CalenderDate {CalenderWeekId = 6,  Date = new DateTime(2026, 02, 03) },
                    new CalenderDate {CalenderWeekId = 6,  Date = new DateTime(2026, 02, 04) },
                    new CalenderDate {CalenderWeekId = 6,  Date = new DateTime(2026, 02, 05) },
                    new CalenderDate {CalenderWeekId = 6,  Date = new DateTime(2026, 02, 06) },
                    new CalenderDate {CalenderWeekId = 6,  Date = new DateTime(2026, 02, 07) },
                    new CalenderDate {CalenderWeekId = 6,  Date = new DateTime(2026, 02, 08) },
                    new CalenderDate {CalenderWeekId = 7,  Date = new DateTime(2026, 02, 09) },
                    new CalenderDate {CalenderWeekId = 7,  Date = new DateTime(2026, 02, 10) },
                    new CalenderDate {CalenderWeekId = 7,  Date = new DateTime(2026, 02, 11) },
                    new CalenderDate {CalenderWeekId = 7,  Date = new DateTime(2026, 02, 12) },
                    new CalenderDate {CalenderWeekId = 7,  Date = new DateTime(2026, 02, 13) },
                    new CalenderDate {CalenderWeekId = 7,  Date = new DateTime(2026, 02, 14) },
                    new CalenderDate {CalenderWeekId = 7,  Date = new DateTime(2026, 02, 15) },
                    new CalenderDate {CalenderWeekId = 8,  Date = new DateTime(2026, 02, 16) },
                    new CalenderDate {CalenderWeekId = 8,  Date = new DateTime(2026, 02, 17) },
                    new CalenderDate {CalenderWeekId = 8,  Date = new DateTime(2026, 02, 18) },
                    new CalenderDate {CalenderWeekId = 8,  Date = new DateTime(2026, 02, 19) },
                    new CalenderDate {CalenderWeekId = 8,  Date = new DateTime(2026, 02, 20) },
                    new CalenderDate {CalenderWeekId = 8,  Date = new DateTime(2026, 02, 21) },
                    new CalenderDate {CalenderWeekId = 8,  Date = new DateTime(2026, 02, 22) },
                    new CalenderDate {CalenderWeekId = 9,  Date = new DateTime(2026, 02, 23) },
                    new CalenderDate {CalenderWeekId = 9,  Date = new DateTime(2026, 02, 24) },
                    new CalenderDate {CalenderWeekId = 9,  Date = new DateTime(2026, 02, 25) },
                    new CalenderDate {CalenderWeekId = 9,  Date = new DateTime(2026, 02, 26) },
                    new CalenderDate {CalenderWeekId = 9,  Date = new DateTime(2026, 02, 27) },
                    new CalenderDate {CalenderWeekId = 9,  Date = new DateTime(2026, 02, 28) },
                    new CalenderDate {CalenderWeekId = 9,  Date = new DateTime(2026, 03, 01) },
                    new CalenderDate {CalenderWeekId = 10,  Date = new DateTime(2026, 03, 02) },
                    new CalenderDate {CalenderWeekId = 10,  Date = new DateTime(2026, 03, 03) },
                    new CalenderDate {CalenderWeekId = 10,  Date = new DateTime(2026, 03, 04) },
                    new CalenderDate {CalenderWeekId = 10,  Date = new DateTime(2026, 03, 05) },
                    new CalenderDate {CalenderWeekId = 10,  Date = new DateTime(2026, 03, 06) },
                    new CalenderDate {CalenderWeekId = 10,  Date = new DateTime(2026, 03, 07) },
                    new CalenderDate {CalenderWeekId = 10,  Date = new DateTime(2026, 03, 08) },
                    new CalenderDate {CalenderWeekId = 11,  Date = new DateTime(2026, 03, 09) },
                    new CalenderDate {CalenderWeekId = 11,  Date = new DateTime(2026, 03, 10) },
                    new CalenderDate {CalenderWeekId = 11,  Date = new DateTime(2026, 03, 11) },
                    new CalenderDate {CalenderWeekId = 11,  Date = new DateTime(2026, 03, 12) },
                    new CalenderDate {CalenderWeekId = 11,  Date = new DateTime(2026, 03, 13) },
                    new CalenderDate {CalenderWeekId = 11,  Date = new DateTime(2026, 03, 14) },
                    new CalenderDate {CalenderWeekId = 11,  Date = new DateTime(2026, 03, 15) },
                    new CalenderDate {CalenderWeekId = 12,  Date = new DateTime(2026, 03, 16) },
                    new CalenderDate {CalenderWeekId = 12,  Date = new DateTime(2026, 03, 17) },
                    new CalenderDate {CalenderWeekId = 12,  Date = new DateTime(2026, 03, 18) },
                    new CalenderDate {CalenderWeekId = 12,  Date = new DateTime(2026, 03, 19) },
                    new CalenderDate {CalenderWeekId = 12,  Date = new DateTime(2026, 03, 20) },
                    new CalenderDate {CalenderWeekId = 12,  Date = new DateTime(2026, 03, 21) },
                    new CalenderDate {CalenderWeekId = 12,  Date = new DateTime(2026, 03, 22) },
                    new CalenderDate {CalenderWeekId = 13,  Date = new DateTime(2026, 03, 23) },
                    new CalenderDate {CalenderWeekId = 13,  Date = new DateTime(2026, 03, 24) },
                    new CalenderDate {CalenderWeekId = 13,  Date = new DateTime(2026, 03, 25) },
                    new CalenderDate {CalenderWeekId = 13,  Date = new DateTime(2026, 03, 26) },
                    new CalenderDate {CalenderWeekId = 13,  Date = new DateTime(2026, 03, 27) },
                    new CalenderDate {CalenderWeekId = 13,  Date = new DateTime(2026, 03, 28) },
                    new CalenderDate {CalenderWeekId = 13,  Date = new DateTime(2026, 03, 29) },
                    new CalenderDate {CalenderWeekId = 14,  Date = new DateTime(2026, 03, 30) },
                    new CalenderDate {CalenderWeekId = 14,  Date = new DateTime(2026, 03, 31) },
                    new CalenderDate {CalenderWeekId = 14,  Date = new DateTime(2026, 04, 01) },
                    new CalenderDate {CalenderWeekId = 14,  Date = new DateTime(2026, 04, 02) },
                    new CalenderDate {CalenderWeekId = 14,  Date = new DateTime(2026, 04, 03) },
                    new CalenderDate {CalenderWeekId = 14,  Date = new DateTime(2026, 04, 04) },
                    new CalenderDate {CalenderWeekId = 14,  Date = new DateTime(2026, 04, 05) },
                    new CalenderDate {CalenderWeekId = 15,  Date = new DateTime(2026, 04, 06) },
                    new CalenderDate {CalenderWeekId = 15,  Date = new DateTime(2026, 04, 07) },
                    new CalenderDate {CalenderWeekId = 15,  Date = new DateTime(2026, 04, 08) },
                    new CalenderDate {CalenderWeekId = 15,  Date = new DateTime(2026, 04, 09) },
                    new CalenderDate {CalenderWeekId = 15,  Date = new DateTime(2026, 04, 10) },
                    new CalenderDate {CalenderWeekId = 15,  Date = new DateTime(2026, 04, 11) },
                    new CalenderDate {CalenderWeekId = 15,  Date = new DateTime(2026, 04, 12) },
                    new CalenderDate {CalenderWeekId = 16,  Date = new DateTime(2026, 04, 13) },
                    new CalenderDate {CalenderWeekId = 16,  Date = new DateTime(2026, 04, 14) },
                    new CalenderDate {CalenderWeekId = 16,  Date = new DateTime(2026, 04, 15) },
                    new CalenderDate {CalenderWeekId = 16,  Date = new DateTime(2026, 04, 16) },
                    new CalenderDate {CalenderWeekId = 16,  Date = new DateTime(2026, 04, 17) },
                    new CalenderDate {CalenderWeekId = 16,  Date = new DateTime(2026, 04, 18) },
                    new CalenderDate {CalenderWeekId = 16,  Date = new DateTime(2026, 04, 19) },
                    new CalenderDate {CalenderWeekId = 17,  Date = new DateTime(2026, 04, 20) },
                    new CalenderDate {CalenderWeekId = 17,  Date = new DateTime(2026, 04, 21) },
                    new CalenderDate {CalenderWeekId = 17,  Date = new DateTime(2026, 04, 22) },
                    new CalenderDate {CalenderWeekId = 17,  Date = new DateTime(2026, 04, 23) },
                    new CalenderDate {CalenderWeekId = 17,  Date = new DateTime(2026, 04, 24) },
                    new CalenderDate {CalenderWeekId = 17,  Date = new DateTime(2026, 04, 25) },
                    new CalenderDate {CalenderWeekId = 17,  Date = new DateTime(2026, 04, 26) },
                    new CalenderDate {CalenderWeekId = 18,  Date = new DateTime(2026, 04, 27) },
                    new CalenderDate {CalenderWeekId = 18,  Date = new DateTime(2026, 04, 28) },
                    new CalenderDate {CalenderWeekId = 18,  Date = new DateTime(2026, 04, 29) },
                    new CalenderDate {CalenderWeekId = 18,  Date = new DateTime(2026, 04, 30) },
                    new CalenderDate {CalenderWeekId = 18,  Date = new DateTime(2026, 05, 01) },
                    new CalenderDate {CalenderWeekId = 18,  Date = new DateTime(2026, 05, 02) },
                    new CalenderDate {CalenderWeekId = 18,  Date = new DateTime(2026, 05, 03) },
                    new CalenderDate {CalenderWeekId = 19,  Date = new DateTime(2026, 05, 04) },
                    new CalenderDate {CalenderWeekId = 19,  Date = new DateTime(2026, 05, 05) },
                    new CalenderDate {CalenderWeekId = 19,  Date = new DateTime(2026, 05, 06) },
                    new CalenderDate {CalenderWeekId = 19,  Date = new DateTime(2026, 05, 07) },
                    new CalenderDate {CalenderWeekId = 19,  Date = new DateTime(2026, 05, 08) },
                    new CalenderDate {CalenderWeekId = 19,  Date = new DateTime(2026, 05, 09) },
                    new CalenderDate {CalenderWeekId = 19,  Date = new DateTime(2026, 05, 10) },
                    new CalenderDate {CalenderWeekId = 20,  Date = new DateTime(2026, 05, 11) },
                    new CalenderDate {CalenderWeekId = 20,  Date = new DateTime(2026, 05, 12) },
                    new CalenderDate {CalenderWeekId = 20,  Date = new DateTime(2026, 05, 13) },
                    new CalenderDate {CalenderWeekId = 20,  Date = new DateTime(2026, 05, 14) },
                    new CalenderDate {CalenderWeekId = 20,  Date = new DateTime(2026, 05, 15) },
                    new CalenderDate {CalenderWeekId = 20,  Date = new DateTime(2026, 05, 16) },
                    new CalenderDate {CalenderWeekId = 20,  Date = new DateTime(2026, 05, 17) },
                    new CalenderDate {CalenderWeekId = 21,  Date = new DateTime(2026, 05, 18) },
                    new CalenderDate {CalenderWeekId = 21,  Date = new DateTime(2026, 05, 19) },
                    new CalenderDate {CalenderWeekId = 21,  Date = new DateTime(2026, 05, 20) },
                    new CalenderDate {CalenderWeekId = 21,  Date = new DateTime(2026, 05, 21) },
                    new CalenderDate {CalenderWeekId = 21,  Date = new DateTime(2026, 05, 22) },
                    new CalenderDate {CalenderWeekId = 21,  Date = new DateTime(2026, 05, 23) },
                    new CalenderDate {CalenderWeekId = 21,  Date = new DateTime(2026, 05, 24) },
                    new CalenderDate {CalenderWeekId = 22,  Date = new DateTime(2026, 05, 25) },
                    new CalenderDate {CalenderWeekId = 22,  Date = new DateTime(2026, 05, 26) },
                    new CalenderDate {CalenderWeekId = 22,  Date = new DateTime(2026, 05, 27) },
                    new CalenderDate {CalenderWeekId = 22,  Date = new DateTime(2026, 05, 28) },
                    new CalenderDate {CalenderWeekId = 22,  Date = new DateTime(2026, 05, 29) },
                    new CalenderDate {CalenderWeekId = 22,  Date = new DateTime(2026, 05, 30) },
                    new CalenderDate {CalenderWeekId = 22,  Date = new DateTime(2026, 05, 31) },
                    new CalenderDate {CalenderWeekId = 23,  Date = new DateTime(2026, 06, 01) },
                    new CalenderDate {CalenderWeekId = 23,  Date = new DateTime(2026, 06, 02) },
                    new CalenderDate {CalenderWeekId = 23,  Date = new DateTime(2026, 06, 03) },
                    new CalenderDate {CalenderWeekId = 23,  Date = new DateTime(2026, 06, 04) },
                    new CalenderDate {CalenderWeekId = 23,  Date = new DateTime(2026, 06, 05) },
                    new CalenderDate {CalenderWeekId = 23,  Date = new DateTime(2026, 06, 06) },
                    new CalenderDate {CalenderWeekId = 23,  Date = new DateTime(2026, 06, 07) },
                    new CalenderDate {CalenderWeekId = 24,  Date = new DateTime(2026, 06, 08) },
                    new CalenderDate {CalenderWeekId = 24,  Date = new DateTime(2026, 06, 09) },
                    new CalenderDate {CalenderWeekId = 24,  Date = new DateTime(2026, 06, 10) },
                    new CalenderDate {CalenderWeekId = 24,  Date = new DateTime(2026, 06, 11) },
                    new CalenderDate {CalenderWeekId = 24,  Date = new DateTime(2026, 06, 12) },
                    new CalenderDate {CalenderWeekId = 24,  Date = new DateTime(2026, 06, 13) },
                    new CalenderDate {CalenderWeekId = 24,  Date = new DateTime(2026, 06, 14) },
                    new CalenderDate {CalenderWeekId = 25,  Date = new DateTime(2026, 06, 15) },
                    new CalenderDate {CalenderWeekId = 25,  Date = new DateTime(2026, 06, 16) },
                    new CalenderDate {CalenderWeekId = 25,  Date = new DateTime(2026, 06, 17) },
                    new CalenderDate {CalenderWeekId = 25,  Date = new DateTime(2026, 06, 18) },
                    new CalenderDate {CalenderWeekId = 25,  Date = new DateTime(2026, 06, 19) },
                    new CalenderDate {CalenderWeekId = 25,  Date = new DateTime(2026, 06, 20) },
                    new CalenderDate {CalenderWeekId = 25,  Date = new DateTime(2026, 06, 21) },
                    new CalenderDate {CalenderWeekId = 26,  Date = new DateTime(2026, 06, 22) },
                    new CalenderDate {CalenderWeekId = 26,  Date = new DateTime(2026, 06, 23) },
                    new CalenderDate {CalenderWeekId = 26,  Date = new DateTime(2026, 06, 24) },
                    new CalenderDate {CalenderWeekId = 26,  Date = new DateTime(2026, 06, 25) },
                    new CalenderDate {CalenderWeekId = 26,  Date = new DateTime(2026, 06, 26) },
                    new CalenderDate {CalenderWeekId = 26,  Date = new DateTime(2026, 06, 27) },
                    new CalenderDate {CalenderWeekId = 26,  Date = new DateTime(2026, 06, 28) },
                    new CalenderDate {CalenderWeekId = 27,  Date = new DateTime(2026, 06, 29) },
                    new CalenderDate {CalenderWeekId = 27,  Date = new DateTime(2026, 06, 30) },
                    new CalenderDate {CalenderWeekId = 27,  Date = new DateTime(2026, 07, 01) },
                    new CalenderDate {CalenderWeekId = 27,  Date = new DateTime(2026, 07, 02) },
                    new CalenderDate {CalenderWeekId = 27,  Date = new DateTime(2026, 07, 03) },
                    new CalenderDate {CalenderWeekId = 27,  Date = new DateTime(2026, 07, 04) },
                    new CalenderDate {CalenderWeekId = 27,  Date = new DateTime(2026, 07, 05) },
                    new CalenderDate {CalenderWeekId = 28,  Date = new DateTime(2026, 07, 06) },
                    new CalenderDate {CalenderWeekId = 28,  Date = new DateTime(2026, 07, 07) },
                    new CalenderDate {CalenderWeekId = 28,  Date = new DateTime(2026, 07, 08) },
                    new CalenderDate {CalenderWeekId = 28,  Date = new DateTime(2026, 07, 09) },
                    new CalenderDate {CalenderWeekId = 28,  Date = new DateTime(2026, 07, 10) },
                    new CalenderDate {CalenderWeekId = 28,  Date = new DateTime(2026, 07, 11) },
                    new CalenderDate {CalenderWeekId = 28,  Date = new DateTime(2026, 07, 12) },
                    new CalenderDate {CalenderWeekId = 29,  Date = new DateTime(2026, 07, 13) },
                    new CalenderDate {CalenderWeekId = 29,  Date = new DateTime(2026, 07, 14) },
                    new CalenderDate {CalenderWeekId = 29,  Date = new DateTime(2026, 07, 15) },
                    new CalenderDate {CalenderWeekId = 29,  Date = new DateTime(2026, 07, 16) },
                    new CalenderDate {CalenderWeekId = 29,  Date = new DateTime(2026, 07, 17) },
                    new CalenderDate {CalenderWeekId = 29,  Date = new DateTime(2026, 07, 18) },
                    new CalenderDate {CalenderWeekId = 29,  Date = new DateTime(2026, 07, 19) },
                    new CalenderDate {CalenderWeekId = 30,  Date = new DateTime(2026, 07, 20) },
                    new CalenderDate {CalenderWeekId = 30,  Date = new DateTime(2026, 07, 21) },
                    new CalenderDate {CalenderWeekId = 30,  Date = new DateTime(2026, 07, 22) },
                    new CalenderDate {CalenderWeekId = 30,  Date = new DateTime(2026, 07, 23) },
                    new CalenderDate {CalenderWeekId = 30,  Date = new DateTime(2026, 07, 24) },
                    new CalenderDate {CalenderWeekId = 30,  Date = new DateTime(2026, 07, 25) },
                    new CalenderDate {CalenderWeekId = 30,  Date = new DateTime(2026, 07, 26) },
                    new CalenderDate {CalenderWeekId = 31,  Date = new DateTime(2026, 07, 27) },
                    new CalenderDate {CalenderWeekId = 31,  Date = new DateTime(2026, 07, 28) },
                    new CalenderDate {CalenderWeekId = 31,  Date = new DateTime(2026, 07, 29) },
                    new CalenderDate {CalenderWeekId = 31,  Date = new DateTime(2026, 07, 30) },
                    new CalenderDate {CalenderWeekId = 31,  Date = new DateTime(2026, 07, 31) },
                    new CalenderDate {CalenderWeekId = 31,  Date = new DateTime(2026, 08, 01) },
                    new CalenderDate {CalenderWeekId = 31,  Date = new DateTime(2026, 08, 02) },
                    new CalenderDate {CalenderWeekId = 32,  Date = new DateTime(2026, 08, 03) },
                    new CalenderDate {CalenderWeekId = 32,  Date = new DateTime(2026, 08, 04) },
                    new CalenderDate {CalenderWeekId = 32,  Date = new DateTime(2026, 08, 05) },
                    new CalenderDate {CalenderWeekId = 32,  Date = new DateTime(2026, 08, 06) },
                    new CalenderDate {CalenderWeekId = 32,  Date = new DateTime(2026, 08, 07) },
                    new CalenderDate {CalenderWeekId = 32,  Date = new DateTime(2026, 08, 08) },
                    new CalenderDate {CalenderWeekId = 32,  Date = new DateTime(2026, 08, 09) },
                    new CalenderDate {CalenderWeekId = 33,  Date = new DateTime(2026, 08, 10) },
                    new CalenderDate {CalenderWeekId = 33,  Date = new DateTime(2026, 08, 11) },
                    new CalenderDate {CalenderWeekId = 33,  Date = new DateTime(2026, 08, 12) },
                    new CalenderDate {CalenderWeekId = 33,  Date = new DateTime(2026, 08, 13) },
                    new CalenderDate {CalenderWeekId = 33,  Date = new DateTime(2026, 08, 14) },
                    new CalenderDate {CalenderWeekId = 33,  Date = new DateTime(2026, 08, 15) },
                    new CalenderDate {CalenderWeekId = 33,  Date = new DateTime(2026, 08, 16) },
                    new CalenderDate {CalenderWeekId = 34,  Date = new DateTime(2026, 08, 17) },
                    new CalenderDate {CalenderWeekId = 34,  Date = new DateTime(2026, 08, 18) },
                    new CalenderDate {CalenderWeekId = 34,  Date = new DateTime(2026, 08, 19) },
                    new CalenderDate {CalenderWeekId = 34,  Date = new DateTime(2026, 08, 20) },
                    new CalenderDate {CalenderWeekId = 34,  Date = new DateTime(2026, 08, 21) },
                    new CalenderDate {CalenderWeekId = 34,  Date = new DateTime(2026, 08, 22) },
                    new CalenderDate {CalenderWeekId = 34,  Date = new DateTime(2026, 08, 23) },
                    new CalenderDate {CalenderWeekId = 35,  Date = new DateTime(2026, 08, 24) },
                    new CalenderDate {CalenderWeekId = 35,  Date = new DateTime(2026, 08, 25) },
                    new CalenderDate {CalenderWeekId = 35,  Date = new DateTime(2026, 08, 26) },
                    new CalenderDate {CalenderWeekId = 35,  Date = new DateTime(2026, 08, 27) },
                    new CalenderDate {CalenderWeekId = 35,  Date = new DateTime(2026, 08, 28) },
                    new CalenderDate {CalenderWeekId = 35,  Date = new DateTime(2026, 08, 29) },
                    new CalenderDate {CalenderWeekId = 35,  Date = new DateTime(2026, 08, 30) },
                    new CalenderDate {CalenderWeekId = 36,  Date = new DateTime(2026, 08, 31) },
                    new CalenderDate {CalenderWeekId = 36,  Date = new DateTime(2026, 09, 01) },
                    new CalenderDate {CalenderWeekId = 36,  Date = new DateTime(2026, 09, 02) },
                    new CalenderDate {CalenderWeekId = 36,  Date = new DateTime(2026, 09, 03) },
                    new CalenderDate {CalenderWeekId = 36,  Date = new DateTime(2026, 09, 04) },
                    new CalenderDate {CalenderWeekId = 36,  Date = new DateTime(2026, 09, 05) },
                    new CalenderDate {CalenderWeekId = 36,  Date = new DateTime(2026, 09, 06) },
                    new CalenderDate {CalenderWeekId = 37,  Date = new DateTime(2026, 09, 07) },
                    new CalenderDate {CalenderWeekId = 37,  Date = new DateTime(2026, 09, 08) },
                    new CalenderDate {CalenderWeekId = 37,  Date = new DateTime(2026, 09, 09) },
                    new CalenderDate {CalenderWeekId = 37,  Date = new DateTime(2026, 09, 10) },
                    new CalenderDate {CalenderWeekId = 37,  Date = new DateTime(2026, 09, 11) },
                    new CalenderDate {CalenderWeekId = 37,  Date = new DateTime(2026, 09, 12) },
                    new CalenderDate {CalenderWeekId = 37,  Date = new DateTime(2026, 09, 13) },
                    new CalenderDate {CalenderWeekId = 38,  Date = new DateTime(2026, 09, 14) },
                    new CalenderDate {CalenderWeekId = 38,  Date = new DateTime(2026, 09, 15) },
                    new CalenderDate {CalenderWeekId = 38,  Date = new DateTime(2026, 09, 16) },
                    new CalenderDate {CalenderWeekId = 38,  Date = new DateTime(2026, 09, 17) },
                    new CalenderDate {CalenderWeekId = 38,  Date = new DateTime(2026, 09, 18) },
                    new CalenderDate {CalenderWeekId = 38,  Date = new DateTime(2026, 09, 19) },
                    new CalenderDate {CalenderWeekId = 38,  Date = new DateTime(2026, 09, 20) },
                    new CalenderDate {CalenderWeekId = 39,  Date = new DateTime(2026, 09, 21) },
                    new CalenderDate {CalenderWeekId = 39,  Date = new DateTime(2026, 09, 22) },
                    new CalenderDate {CalenderWeekId = 39,  Date = new DateTime(2026, 09, 23) },
                    new CalenderDate {CalenderWeekId = 39,  Date = new DateTime(2026, 09, 24) },
                    new CalenderDate {CalenderWeekId = 39,  Date = new DateTime(2026, 09, 25) },
                    new CalenderDate {CalenderWeekId = 39,  Date = new DateTime(2026, 09, 26) },
                    new CalenderDate {CalenderWeekId = 39,  Date = new DateTime(2026, 09, 27) },
                    new CalenderDate {CalenderWeekId = 40,  Date = new DateTime(2026, 09, 28) },
                    new CalenderDate {CalenderWeekId = 40,  Date = new DateTime(2026, 09, 29) },
                    new CalenderDate {CalenderWeekId = 40,  Date = new DateTime(2026, 09, 30) },
                    new CalenderDate {CalenderWeekId = 40,  Date = new DateTime(2026, 10, 01) },
                    new CalenderDate {CalenderWeekId = 40,  Date = new DateTime(2026, 10, 02) },
                    new CalenderDate {CalenderWeekId = 40,  Date = new DateTime(2026, 10, 03) },
                    new CalenderDate {CalenderWeekId = 40,  Date = new DateTime(2026, 10, 04) },
                    new CalenderDate {CalenderWeekId = 41,  Date = new DateTime(2026, 10, 05) },
                    new CalenderDate {CalenderWeekId = 41,  Date = new DateTime(2026, 10, 06) },
                    new CalenderDate {CalenderWeekId = 41,  Date = new DateTime(2026, 10, 07) },
                    new CalenderDate {CalenderWeekId = 41,  Date = new DateTime(2026, 10, 08) },
                    new CalenderDate {CalenderWeekId = 41,  Date = new DateTime(2026, 10, 09) },
                    new CalenderDate {CalenderWeekId = 41,  Date = new DateTime(2026, 10, 10) },
                    new CalenderDate {CalenderWeekId = 41,  Date = new DateTime(2026, 10, 11) },
                    new CalenderDate {CalenderWeekId = 42,  Date = new DateTime(2026, 10, 12) },
                    new CalenderDate {CalenderWeekId = 42,  Date = new DateTime(2026, 10, 13) },
                    new CalenderDate {CalenderWeekId = 42,  Date = new DateTime(2026, 10, 14) },
                    new CalenderDate {CalenderWeekId = 42,  Date = new DateTime(2026, 10, 15) },
                    new CalenderDate {CalenderWeekId = 42,  Date = new DateTime(2026, 10, 16) },
                    new CalenderDate {CalenderWeekId = 42,  Date = new DateTime(2026, 10, 17) },
                    new CalenderDate {CalenderWeekId = 42,  Date = new DateTime(2026, 10, 18) },
                    new CalenderDate {CalenderWeekId = 43,  Date = new DateTime(2026, 10, 19) },
                    new CalenderDate {CalenderWeekId = 43,  Date = new DateTime(2026, 10, 20) },
                    new CalenderDate {CalenderWeekId = 43,  Date = new DateTime(2026, 10, 21) },
                    new CalenderDate {CalenderWeekId = 43,  Date = new DateTime(2026, 10, 22) },
                    new CalenderDate {CalenderWeekId = 43,  Date = new DateTime(2026, 10, 23) },
                    new CalenderDate {CalenderWeekId = 43,  Date = new DateTime(2026, 10, 24) },
                    new CalenderDate {CalenderWeekId = 43,  Date = new DateTime(2026, 10, 25) },
                    new CalenderDate {CalenderWeekId = 44,  Date = new DateTime(2026, 10, 26) },
                    new CalenderDate {CalenderWeekId = 44,  Date = new DateTime(2026, 10, 27) },
                    new CalenderDate {CalenderWeekId = 44,  Date = new DateTime(2026, 10, 28) },
                    new CalenderDate {CalenderWeekId = 44,  Date = new DateTime(2026, 10, 29) },
                    new CalenderDate {CalenderWeekId = 44,  Date = new DateTime(2026, 10, 30) },
                    new CalenderDate {CalenderWeekId = 44,  Date = new DateTime(2026, 10, 31) },
                    new CalenderDate {CalenderWeekId = 44,  Date = new DateTime(2026, 11, 01) },
                    new CalenderDate {CalenderWeekId = 45,  Date = new DateTime(2026, 11, 02) },
                    new CalenderDate {CalenderWeekId = 45,  Date = new DateTime(2026, 11, 03) },
                    new CalenderDate {CalenderWeekId = 45,  Date = new DateTime(2026, 11, 04) },
                    new CalenderDate {CalenderWeekId = 45,  Date = new DateTime(2026, 11, 05) },
                    new CalenderDate {CalenderWeekId = 45,  Date = new DateTime(2026, 11, 06) },
                    new CalenderDate {CalenderWeekId = 45,  Date = new DateTime(2026, 11, 07) },
                    new CalenderDate {CalenderWeekId = 45,  Date = new DateTime(2026, 11, 08) },
                    new CalenderDate {CalenderWeekId = 46,  Date = new DateTime(2026, 11, 09) },
                    new CalenderDate {CalenderWeekId = 46,  Date = new DateTime(2026, 11, 10) },
                    new CalenderDate {CalenderWeekId = 46,  Date = new DateTime(2026, 11, 11) },
                    new CalenderDate {CalenderWeekId = 46,  Date = new DateTime(2026, 11, 12) },
                    new CalenderDate {CalenderWeekId = 46,  Date = new DateTime(2026, 11, 13) },
                    new CalenderDate {CalenderWeekId = 46,  Date = new DateTime(2026, 11, 14) },
                    new CalenderDate {CalenderWeekId = 46,  Date = new DateTime(2026, 11, 15) },
                    new CalenderDate {CalenderWeekId = 47,  Date = new DateTime(2026, 11, 16) },
                    new CalenderDate {CalenderWeekId = 47,  Date = new DateTime(2026, 11, 17) },
                    new CalenderDate {CalenderWeekId = 47,  Date = new DateTime(2026, 11, 18) },
                    new CalenderDate {CalenderWeekId = 47,  Date = new DateTime(2026, 11, 19) },
                    new CalenderDate {CalenderWeekId = 47,  Date = new DateTime(2026, 11, 20) },
                    new CalenderDate {CalenderWeekId = 47,  Date = new DateTime(2026, 11, 21) },
                    new CalenderDate {CalenderWeekId = 47,  Date = new DateTime(2026, 11, 22) },
                    new CalenderDate {CalenderWeekId = 48,  Date = new DateTime(2026, 11, 23) },
                    new CalenderDate {CalenderWeekId = 48,  Date = new DateTime(2026, 11, 24) },
                    new CalenderDate {CalenderWeekId = 48,  Date = new DateTime(2026, 11, 25) },
                    new CalenderDate {CalenderWeekId = 48,  Date = new DateTime(2026, 11, 26) },
                    new CalenderDate {CalenderWeekId = 48,  Date = new DateTime(2026, 11, 27) },
                    new CalenderDate {CalenderWeekId = 48,  Date = new DateTime(2026, 11, 28) },
                    new CalenderDate {CalenderWeekId = 48,  Date = new DateTime(2026, 11, 29) },
                    new CalenderDate {CalenderWeekId = 49,  Date = new DateTime(2026, 11, 30) },
                    new CalenderDate {CalenderWeekId = 49,  Date = new DateTime(2026, 12, 01) },
                    new CalenderDate {CalenderWeekId = 49,  Date = new DateTime(2026, 12, 02) },
                    new CalenderDate {CalenderWeekId = 49,  Date = new DateTime(2026, 12, 03) },
                    new CalenderDate {CalenderWeekId = 49,  Date = new DateTime(2026, 12, 04) },
                    new CalenderDate {CalenderWeekId = 49,  Date = new DateTime(2026, 12, 05) },
                    new CalenderDate {CalenderWeekId = 49,  Date = new DateTime(2026, 12, 06) },
                    new CalenderDate {CalenderWeekId = 50,  Date = new DateTime(2026, 12, 07) },
                    new CalenderDate {CalenderWeekId = 50,  Date = new DateTime(2026, 12, 08) },
                    new CalenderDate {CalenderWeekId = 50,  Date = new DateTime(2026, 12, 09) },
                    new CalenderDate {CalenderWeekId = 50,  Date = new DateTime(2026, 12, 10) },
                    new CalenderDate {CalenderWeekId = 50,  Date = new DateTime(2026, 12, 11) },
                    new CalenderDate {CalenderWeekId = 50,  Date = new DateTime(2026, 12, 12) },
                    new CalenderDate {CalenderWeekId = 50,  Date = new DateTime(2026, 12, 13) },
                    new CalenderDate {CalenderWeekId = 51,  Date = new DateTime(2026, 12, 14) },
                    new CalenderDate {CalenderWeekId = 51,  Date = new DateTime(2026, 12, 15) },
                    new CalenderDate {CalenderWeekId = 51,  Date = new DateTime(2026, 12, 16) },
                    new CalenderDate {CalenderWeekId = 51,  Date = new DateTime(2026, 12, 17) },
                    new CalenderDate {CalenderWeekId = 51,  Date = new DateTime(2026, 12, 18) },
                    new CalenderDate {CalenderWeekId = 51,  Date = new DateTime(2026, 12, 19) },
                    new CalenderDate {CalenderWeekId = 51,  Date = new DateTime(2026, 12, 20) },
                    new CalenderDate {CalenderWeekId = 52,  Date = new DateTime(2026, 12, 21) },
                    new CalenderDate {CalenderWeekId = 52,  Date = new DateTime(2026, 12, 22) },
                    new CalenderDate {CalenderWeekId = 52,  Date = new DateTime(2026, 12, 23) },
                    new CalenderDate {CalenderWeekId = 52,  Date = new DateTime(2026, 12, 24) },
                    new CalenderDate {CalenderWeekId = 52,  Date = new DateTime(2026, 12, 25) },
                    new CalenderDate {CalenderWeekId = 52,  Date = new DateTime(2026, 12, 26) },
                    new CalenderDate {CalenderWeekId = 52,  Date = new DateTime(2026, 12, 27) },

                };

                await _dbContext.CalenderDate.AddRangeAsync(calenderDate);
                await _dbContext.SaveChangesAsync();
            }
        }

        

        private async Task SeedShift()
        {
            if (!_dbContext.Shift.Any())
            {
                var shift = new List<Shift>
                {
                    new Shift { Code = "1", Name = "01 am - 10 pm", TimeFrom = new TimeOnly(1,0,0), TimeTo = new TimeOnly(22,0,0), },
                    new Shift { Code = "2", Name = "02 am - 11 pm", TimeFrom = new TimeOnly(2,0,0), TimeTo = new TimeOnly(23,0,0), },
                    new Shift { Code = "3", Name = "03 am - 12 pm", TimeFrom = new TimeOnly(3,0,0), TimeTo = new TimeOnly(23,59,59), },
                    new Shift { Code = "4", Name = "04 am - 13 pm", TimeFrom = new TimeOnly(4,0,0), TimeTo = new TimeOnly(1,0,0), },
                    new Shift { Code = "5", Name = "05 am - 14 pm", TimeFrom = new TimeOnly(5,0,0), TimeTo = new TimeOnly(2,0,0), },
                    new Shift { Code = "6", Name = "06 am - 15 pm", TimeFrom = new TimeOnly(6,0,0), TimeTo = new TimeOnly(3,0,0), },
                    new Shift { Code = "7", Name = "07 am - 16 pm", TimeFrom = new TimeOnly(7,0,0), TimeTo = new TimeOnly(4,0,0), },
                    new Shift { Code = "8", Name = "08 am - 17 pm", TimeFrom = new TimeOnly(8,0,0), TimeTo = new TimeOnly(5,0,0), },
                    new Shift { Code = "9", Name = "09 am - 18 pm", TimeFrom = new TimeOnly(9,0,0), TimeTo = new TimeOnly(6,0,0), },
                    new Shift { Code = "10", Name = "10 am - 19 pm", TimeFrom = new TimeOnly(10,0,0), TimeTo = new TimeOnly(7,0,0), },
                    new Shift { Code = "11", Name = "11 am - 20 pm", TimeFrom = new TimeOnly(11,0,0), TimeTo = new TimeOnly(8,0,0), },
                    new Shift { Code = "12", Name = "12 am - 21 pm", TimeFrom = new TimeOnly(12,0,0), TimeTo = new TimeOnly(9,0,0), },
                    new Shift { Code = "13", Name = "01 pm - 10 am", TimeFrom = new TimeOnly(13,0,0), TimeTo = new TimeOnly(10,0,0), },
                    new Shift { Code = "14", Name = "02 pm - 11 am", TimeFrom = new TimeOnly(14,0,0), TimeTo = new TimeOnly(11,0,0), },
                    new Shift { Code = "15", Name = "03 pm - 12 am", TimeFrom = new TimeOnly(15,0,0), TimeTo = new TimeOnly(12,0,0), },
                    new Shift { Code = "16", Name = "04 pm - 01 am", TimeFrom = new TimeOnly(16,0,0), TimeTo = new TimeOnly(13,0,0), },
                    new Shift { Code = "17", Name = "05 pm - 02 am", TimeFrom = new TimeOnly(17,0,0), TimeTo = new TimeOnly(14,0,0), },
                    new Shift { Code = "18", Name = "06 pm - 03 am", TimeFrom = new TimeOnly(18,0,0), TimeTo = new TimeOnly(15,0,0), },
                    new Shift { Code = "19", Name = "07 pm - 04 am", TimeFrom = new TimeOnly(19,0,0), TimeTo = new TimeOnly(16,0,0), },
                    new Shift { Code = "20", Name = "08 pm - 05 am", TimeFrom = new TimeOnly(20,0,0), TimeTo = new TimeOnly(17,0,0), },
                    new Shift { Code = "21", Name = "09 pm - 06 am", TimeFrom = new TimeOnly(21,0,0), TimeTo = new TimeOnly(18,0,0), },
                    new Shift { Code = "22", Name = "10 pm - 07 am", TimeFrom = new TimeOnly(22,0,0), TimeTo = new TimeOnly(19,0,0), },
                    new Shift { Code = "23", Name = "11 pm - 08 am", TimeFrom = new TimeOnly(23,0,0), TimeTo = new TimeOnly(20,0,0), },
                    new Shift { Code = "24", Name = "12 am - 09 am", TimeFrom = new TimeOnly(23,59,59), TimeTo = new TimeOnly(21,0,0), },
                    new Shift { Code = "100", Name = "OFF", TimeFrom = new TimeOnly(0,0,0), TimeTo = new TimeOnly(0,0,0), },
                    new Shift { Code = "200", Name = "ANNUAL LEAVE", TimeFrom = new TimeOnly(0,0,0), TimeTo = new TimeOnly(0,0,0), },
                    new Shift { Code = "201", Name = "UNPAID ANNUAL LEAVE", TimeFrom = new TimeOnly(0,0,0), TimeTo = new TimeOnly(0,0,0), },
                    new Shift { Code = "202", Name = "MATERNITY LEAVE", TimeFrom = new TimeOnly(0,0,0), TimeTo = new TimeOnly(0,0,0), },
                    new Shift { Code = "203", Name = "PATERNITY LEAVE", TimeFrom = new TimeOnly(0,0,0), TimeTo = new TimeOnly(0,0,0), },
                    new Shift { Code = "204", Name = "UMRAH LEAVE", TimeFrom = new TimeOnly(0,0,0), TimeTo = new TimeOnly(0,0,0), },
                    new Shift { Code = "205", Name = "HAJJ LEAVE", TimeFrom = new TimeOnly(0,0,0), TimeTo = new TimeOnly(0,0,0), },
                    new Shift { Code = "300", Name = "PROMOTED", TimeFrom = new TimeOnly(0,0,0), TimeTo = new TimeOnly(0,0,0), },
                    new Shift { Code = "400", Name = "OFF-CALLS", TimeFrom = new TimeOnly(0,0,0), TimeTo = new TimeOnly(0,0,0), },
                    new Shift { Code = "500", Name = "SUSPENDED", TimeFrom = new TimeOnly(0,0,0), TimeTo = new TimeOnly(0,0,0), },
                    new Shift { Code = "600", Name = "RESIGNED", TimeFrom = new TimeOnly(0,0,0), TimeTo = new TimeOnly(0,0,0), },
                    new Shift { Code = "700", Name = "TERMINATED", TimeFrom = new TimeOnly(0,0,0), TimeTo = new TimeOnly(0,0,0), },
                };

                await _dbContext.Shift.AddRangeAsync(shift);
                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task SeedUsers(CancellationToken cancellationToken = default)
        {
            if (!_dbContext.Users.Any())
            {
                _logger.LogInformation("Generating inbuilt Users");
                var users = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        EmployeeId = 2,
                        Email = "Muhammad.Dawood@techtiksglobal.com",
                        FirstName = "Muhammad",
                        LastName = "Dawood",
                        Password = "123",
                        LastLoggedIn = DateTime.Now,
                        DateCreated = DateTime.Now, CreatedBy = 1,
                    },
                };

                await _dbContext.Users.AddRangeAsync(users);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Inbuilt Users generation completed.");
            }
        }

        private async Task SeedRoles(CancellationToken cancellationToken = default)
        {
            if (!_dbContext.Roles.Any())
            {
                _logger.LogInformation("Generating inbuilt Roles");
                var roles = new List<Role>
                {
                    new Role
                    {
                        Name = "Admin",

                    },
                };

                await _dbContext.Roles.AddRangeAsync(roles);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Inbuilt Roles generation completed.");

                await _dbContext.Database.ExecuteSqlAsync($"insert into ApplicationUserRole values(1,1)");
                await _dbContext.SaveChangesAsync();

            }
        }

        private async Task SeedPermissions(CancellationToken cancellationToken = default)
        {
            if (!_dbContext.Permissions.Any())
            {
                _logger.LogInformation("Generating inbuilt Permissions");

                IEnumerable<Domain.Entities.Users.Permission> permissions = Enum
                .GetValues<Domain.Enums.Permission>()
                .Select(p => new Domain.Entities.Users.Permission
                {
                    Name = p.ToString()
                });

                await _dbContext.Permissions.AddRangeAsync(permissions);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Inbuilt Permissions generation completed.");
            }
        }

        private async Task RolePermission(CancellationToken cancellationToken = default)
        {
            if (!_dbContext.RolePermissions.Any())
            {
                _logger.LogInformation("Generating inbuilt RolePermission");

                IEnumerable<Domain.Entities.Users.Permission> permissions = Enum
                .GetValues<Domain.Enums.Permission>()
                .Select(p => new Domain.Entities.Users.Permission
                {
                    Id = (int)p,
                    Name = p.ToString()
                });

                var rolePermissions = new List<RolePermission>();

                foreach (var permission in permissions)
                {
                    rolePermissions.Add(new RolePermission { RoleId = 1, PermissionId = permission.Id });
                }

                await _dbContext.RolePermissions.AddRangeAsync(rolePermissions);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Inbuilt RolePermission generation completed.");
            }
        }
    }
}
