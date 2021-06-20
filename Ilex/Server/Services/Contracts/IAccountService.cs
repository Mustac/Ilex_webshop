using System.Threading.Tasks;

namespace Ilex.Server.Services.Contracts
{
    public interface IAccountService
    {
        Task<bool> CheckIfUserExistByEmailAsync(string email);
    }
}