using Ilex.Client.ApiCaller;
using Ilex.Client.Services.Contracts;
using Ilex.Shared.Helpers;
using Ilex.Shared.ModelDTOs.Account;
using System.Threading.Tasks;

namespace Ilex.Client.Services
{
    public class AccountApiCall : IAccountApiCall
    {
        private readonly IApiCall _apiCall;
        private readonly NotificationMessageService _notificationMessageService;

        public AccountApiCall(IApiCall apiCall, NotificationMessageService notificationMessageService)
        {
            _apiCall = apiCall;
            _notificationMessageService = notificationMessageService;
        }


        public async Task<ApiResponse> CreateAccountAsync(UserRegistrationDTO userModel)
        {
            var result = await _apiCall.PostWithNotificationAsync("api/account/create", userModel);
            return result;
        }

        public async Task<ApiResponse<string>> SignInAsync(UserLoginDTO userModel)
        {
            var result = await _apiCall.SignInAsync<string, UserLoginDTO>(userModel);

            return result;
        }

        public async Task<ApiResponse<UserDTO>> GetUserByIdAsync(int userId)
        {
            var result = await _apiCall.GetByIdAsync<UserDTO>("api/account/get/" + userId.ToString());
            return result;
        }

        public async Task<ApiResponse> SendEmailConfirmationAsync(string email)
        {
            var result = await _apiCall.GetWithNotificationAsync("api/account/sendemailconfirmation/" + email);
            return result;
        }

        public async Task<ApiResponse> VerifyAccountAsync(UserActivationDTO userModel)
        {
            var result = await _apiCall.PostWithNotificationAsync("api/account/verify", userModel);
            return result;
        }
    }
}
