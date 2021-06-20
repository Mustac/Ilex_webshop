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
    }
}
