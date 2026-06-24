using AutoMapper;
using PMS.Domain.Entities.Period;
//using SMS.Application.Features.Users.Queries.GetRoles;
//using SMS.Domain.Entities.Accounting;
//using SMS.Domain.Entities.Products;
//using SMS.Domain.Entities.Sales;
//using SMS.Domain.Entities.Suppliers;
//using SMS.Domain.Entities.Vendors;

namespace PMS.Application.Common.Models
{
    public class LookupDto
    {
        public int Id { get; init; }

        public string? Name { get; init; }
        
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<CalenderMonth, LookupDto>();
                //CreateMap<UnitOfMeasure, LookupDto>();
                //CreateMap<Category, LookupDto>();
                //CreateMap<Product, LookupDto>();
                //CreateMap<SalesCreditor, LookupDto>();
                //CreateMap<Broker, LookupDto>();
                //CreateMap<Supplier, LookupDto>();


            }
        }
    }
}
