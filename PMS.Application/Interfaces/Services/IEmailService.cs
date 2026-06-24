using PMS.Application.DTOs.Config.Email;

namespace PMS.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequestDto request);

    }
}
