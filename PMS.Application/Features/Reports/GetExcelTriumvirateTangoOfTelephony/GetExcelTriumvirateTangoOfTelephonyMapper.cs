using AutoMapper;
using PMS.Domain.Entities.Reporting;
using System.Security.Principal;

namespace PMS.Application.Features.Reports.GetExcelTriumvirateTangoOfTelephony
{
    public class GetExcelTriumvirateTangoOfTelephonyMapper : Profile
    {
        public GetExcelTriumvirateTangoOfTelephonyMapper()
        {
            CreateMap<ReportResultTriumvirateTangoOfTelephony, GetExcelTriumvirateTangoOfTelephonyResponse>()
            .ForMember(d => d.CreateDate, _ => _.MapFrom(src => src.CreateDate));
        }
    }
}
