using System.Threading.Tasks;

namespace Ilex.Server.Services.Contracts
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string emailAddress, string emailMessage, string emailSubject);
    }
}