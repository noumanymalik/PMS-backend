using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMS.Application.Abstractions;
using PMS.Application.DTOs.Options;
using PMS.Application.Interfaces.Services;
using PMS.Infrastructure.Authentication;
using PMS.Infrastructure.Services.DataExportor;
using PMS.Infrastructure.Services.PdfDocumentGenerator;

namespace PMS.Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices();
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            //services.Configure<JwtOptions>(configuration.GetSection("JWTSettings"));
            return services;
        }

        private static void AddServices(this IServiceCollection services)
        {
            services
                .AddTransient<IMediator, Mediator>()
                .AddTransient<IDateTimeService, DateTimeService>()
                .AddTransient<IEmailService, EmailService>()
                .AddTransient<IExcelDocumentGenerator, ExcelGenerator>()
                .AddTransient<IPdfDocumentGenerator, PdfSharpDocumentGenerator>()
                .AddTransient<ILoggedInUserService, LoggedInUserService>()
                .AddTransient<IJwtProvider, JwtProvider>();
        }
    }
}
