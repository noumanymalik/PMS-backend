
using AutoMapper;
using PMS.Application.Features.Users.Commands.CreateUser;
using PMS.Application.Interfaces.Repositories;
using PMS.Domain.Entities.Users;

namespace SMS.Application.Features.Users.Commands.CreateUser
{
    internal class CreateUserMapper : Profile
    {
        public CreateUserMapper() 
        {
            //CreateMap<Role, CreateUserCommand>()
            //    .ForMember(_ => _.RoleId, d => d.MapFrom<UserRoleResolver>())
            //    .ReverseMap();


            CreateMap<ApplicationUser, CreateUserCommand>()
                .ForMember(_ => _.EmployeeId, d => d.MapFrom(d => d.EmployeeId))
                .ForMember(_ => _.Email, d => d.MapFrom(d => d.Email))
                .ForMember(_ => _.FirstName, d => d.MapFrom<string>(d => d.FirstName))
                .ForMember(_ => _.LastName, d => d.MapFrom<string>(d => d.LastName))
                .ForMember(_ => _.Password, d => d.MapFrom(d => d.Password))
                //.ForMember(_ => _.RoleId, d => d.MapFrom<UserRoleResolver>())

                //.ForMember(d => d.RoleId, src => src.MapFrom(s => s.Roles.FirstOrDefault().Id))

                .ReverseMap();
        }
    }

    public class UserRoleResolver : IValueResolver<Role, CreateUserCommand, Role>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserRoleResolver(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Role Resolve(Role source, CreateUserCommand destination, Role destMember, ResolutionContext context)
        {
            return Task.Run(async () => await _unitOfWork.RoleRepository.GetByIdAsync(destination.RoleId))
                .Result;
        }

        
    }

    //public class UserRoleResolver : IValueResolver<ApplicationUser, CreateUserCommand, Role>
    //{
    //    private readonly IUnitOfWork _unitOfWork;
    //    public UserRoleResolver(IUnitOfWork unitOfWork)
    //    {
    //        _unitOfWork = unitOfWork;
    //    }

    //    public Role Resolve(ApplicationUser source, CreateUserCommand destination, Role destMember, ResolutionContext context)
    //    {
    //        return Task.Run(async () => await _unitOfWork.RoleRepository.GetByIdAsync(destMember.Id))
    //            .Result;
    //    }
    //}
}
