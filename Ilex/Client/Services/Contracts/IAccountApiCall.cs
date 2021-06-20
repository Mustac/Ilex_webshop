using Ilex.Shared.Helpers;
using Ilex.Shared.ModelDTOs.Account;
using System.Threading.Tasks;

namespace Ilex.Client.Services.Contracts
{
    public interface IAccountApiCall
    {
        Task<ApiResponse> CreateAccountAsync(UserRegistrationDTO userModel);
        Task<ApiResponse<string>> SignInAsync(UserLoginDTO userModel);
    }
}