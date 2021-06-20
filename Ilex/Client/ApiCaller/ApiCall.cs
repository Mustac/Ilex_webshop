using Ilex.Client.Services;
using Ilex.Shared.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ilex.Client.ApiCaller
{
    public class ApiCall : IApiCall
    {
        private readonly HttpClient _httpClient;
        private readonly NotificationMessageService _notificationService;


        public ApiCall(HttpClient httpClient, NotificationMessageService notificationService)
        {
            _httpClient = httpClient;
            _notificationService = notificationService;
        }


        #region standard api calls to my api server
        /// <summary>
        /// HttpGet Method for getting data from Advanturers Shop Api
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<ApiResponse<List<TContent>>> GetAsync<TContent>(string url)
        {
            try
            {

                HttpResponseMessage result = await _httpClient.GetAsync(url);

                var apiResponse = await DeserializeAsync<List<TContent>>(result);

                return apiResponse;
            }
            catch
            {
                return new ApiResponse<List<TContent>>
                {
                    ResponseCode = System.Net.HttpStatusCode.BadGateway,
                    Error = "Server might be offline",
                    Success = false
                };
            }

        }

        /// <summary>
        /// HttpGet Method for getting data from Advanturers Shop Api
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponse<TContent>> GetByIdAsync<TContent>(string url, int id)
        {
            try
            {

                HttpResponseMessage result = await _httpClient.GetAsync($"{url}/{id}");

                var apiResponse = await DeserializeAsync<TContent>(result);

                return apiResponse;

            }
            catch
            {
                return new ApiResponse<TContent>
                {
                    ResponseCode = System.Net.HttpStatusCode.BadGateway,
                    Error = "Server might be offline",
                    Success = false
                };
            }

        }


        /// <summary>
        /// HTTPPost method give it a type of data we are are sending 
        /// </summary>
        /// <typeparam name="TSend"></typeparam>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ApiResponse> PostAsync<TSend>(string url, TSend content)
        {
            try
            {

                var httpContent = HttpStringContent(content);

                HttpResponseMessage response = await _httpClient.PostAsync(url, httpContent);

                var apiResponse = await DeserializeAsync(response);

                return apiResponse;

            }
            catch
            {
                return new ApiResponse
                {
                    ResponseCode = System.Net.HttpStatusCode.BadGateway,
                    Error = "Server might be offline",
                    Success = false
                };
            }

        }


      



        /// <summary>
        /// HTTPPut method give it a type of data we are sending
        /// </summary>
        /// <typeparam name="TSend"></typeparam>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ApiResponse> PutAsync<TSend>(string url, TSend content)
        {
            try
            {
                var httpContent = HttpStringContent(content);

                HttpResponseMessage response = await _httpClient.PutAsync(url, httpContent);

                var apiResponse = await DeserializeAsync(response);

                return apiResponse;

            }
            catch
            {
                return new ApiResponse
                {
                    ResponseCode = System.Net.HttpStatusCode.BadGateway,
                    Error = "Server might be offline",
                    Success = false
                };
            }
        }


        /// <summary>
        /// HTTPDelete give it an url and id we want to delete
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponse> DeleteAsync(string url, int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{url}/{id}");

                var apiResponse = await DeserializeAsync(response);

                return apiResponse;
            }
            catch
            {
                return new ApiResponse
                {
                    ResponseCode = System.Net.HttpStatusCode.BadGateway,
                    Error = "Server might be offline",
                    Success = false
                };
            }


        }
#endregion

        #region private utility classes
        ///  /// <summary>
        /// Deserializing HttpResponseMessage to ApiResponse with generic TContent
        /// </summary>
        /// <returns>ApiResponse with generic TContent</returns>
        private async Task<ApiResponse<TContent>> DeserializeAsync<TContent>(HttpResponseMessage httpResponseMessage)
        {
            var response = await httpResponseMessage.Content.ReadAsStringAsync();

            var deserializedContent = JsonConvert.DeserializeObject<ApiResponse<TContent>>(response);

            return deserializedContent;
        }

        ///  /// <summary>
        /// Deserializing HttpResponseMessage to ApiResponse
        /// </summary>
        /// <returns>ApiResponse</returns>
        private async Task<ApiResponse> DeserializeAsync(HttpResponseMessage httpResponseMessage)
        {
            var response = await httpResponseMessage.Content.ReadAsStringAsync();

            var deserializedContent = JsonConvert.DeserializeObject<ApiResponse>(response);

            return deserializedContent;
        }

        ///  /// <summary>
        /// Convert T into string content
        /// </summary>
        /// <returns>HttpContent</returns>
        private HttpContent HttpStringContent<T>(T content)
        {
            var jsonContent = JsonConvert.SerializeObject(content);

            HttpContent stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            return stringContent;
        }

        #endregion

        #region ApiRequests That trigger automatic notifications
        /// <summary>
        /// HTTPPost method give it a type of data we are are sending, returns ApiResponse and trigger notification 
        /// </summary>
        /// <typeparam name="TSend"></typeparam>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ApiResponse> PostWithNotificationAsync<TSend>(string url, TSend content)
        {
            try
            {

                var httpContent = HttpStringContent(content);

                HttpResponseMessage response = await _httpClient.PostAsync(url, httpContent);

                var apiResponse = await DeserializeAsync(response);

                _notificationService.NotifyFromApiResponse(apiResponse.Success, apiResponse.Success?apiResponse.Message:apiResponse.Error);

                return apiResponse;

            }
            catch
            {
                var apiResponse = new ApiResponse
                {
                    ResponseCode = System.Net.HttpStatusCode.BadGateway,
                    Error = "Server might be offline",
                    Success = false
                };

                _notificationService.NotifyFromApiResponse(apiResponse.Success, apiResponse.Success ? apiResponse.Message : apiResponse.Error);
                return apiResponse;
            }

        }

        /// <summary>
        /// HTTPPost method give it a type of data we are are sending 
        /// </summary>
        /// <typeparam name="TSend"></typeparam>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ApiResponse<TContent>> SignInAsync<TContent, TSend>(TSend content)
        {
            try
            {

                var httpContent = HttpStringContent(content);

                HttpResponseMessage response = await _httpClient.PostAsync("api/account/login", httpContent);
                var apiResponse = await DeserializeAsync<TContent>(response);
                _notificationService.NotifyFromApiResponse(apiResponse.Success, apiResponse.Success ? apiResponse.Message : apiResponse.Error);
                return apiResponse;

            }
            catch
            {
                var apiResponse = new ApiResponse<TContent>
                {
                    ResponseCode = System.Net.HttpStatusCode.BadGateway,
                    Error = "Server might be offline",
                    Success = false
                };

                _notificationService.NotifyFromApiResponse(apiResponse.Success, apiResponse.Success ? apiResponse.Message : apiResponse.Error);
                return apiResponse;
            }

        }

        #endregion
    }
}
