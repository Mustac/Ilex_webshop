using Ilex.Client.ApiCaller;
using Ilex.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ilex.Client.Services
{
    public class TestApiCall
    {
        private readonly IApiCall _apiCall;

        public TestApiCall(IApiCall apiCall)
        {
            _apiCall = apiCall;
        }

        public async Task<ApiResponse<List<string>>> GetAllAsync()
        {
            var response = await _apiCall.GetAsync<string>("api/test/get");
            return response;
        }

        public async Task<ApiResponse<string>> GetIdAsync(int Id)
        {
            var response = await _apiCall.GetByIdAsync<string>("api/test/get/"+ Id.ToString());
            return response;
        }

        public async Task<ApiResponse> PostAsync(string text)
        {
            var response = await _apiCall.PostAsync("api/test/post", text);
            return response;
        }

        public async Task<ApiResponse> PutAsync(string text)
        {
            var response = await _apiCall.PutAsync("api/test/put", text);
            return response;
        }

        public async Task<ApiResponse> DeleteIdAsync(int Id)
        {
            var response = await _apiCall.DeleteAsync("api/test/delete", Id);
            return response;
        }
    }
}
