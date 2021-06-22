using Ilex.Shared.Models;
using System.Threading.Tasks;

namespace Ilex.Server.Services.Contracts
{
    public interface IAccountService
    {
        Task<bool> CheckIfUserExistByEmailAsync(string email);
        Task<string> GenerateAccountConfirmationToken(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> VerifyingAccountConfirmationToken(User user, string token);
    }
}