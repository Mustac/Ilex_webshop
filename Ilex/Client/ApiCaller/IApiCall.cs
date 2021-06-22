﻿using Ilex.Shared.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ilex.Client.ApiCaller
{
    public interface IApiCall
    {
        Task<ApiResponse> DeleteAsync(string url, int id);
        Task<ApiResponse<List<TContent>>> GetAsync<TContent>(string url);
        Task<ApiResponse> GetWithNotificationAsync(string url);
        Task<ApiResponse<TContent>> GetByIdAsync<TContent>(string urlWithId);
        Task<ApiResponse> PostAsync<TSend>(string url, TSend content);
        Task<ApiResponse> PostWithNotificationAsync<TSend>(string url, TSend content);
        Task<ApiResponse> PutAsync<TSend>(string url, TSend content);
        Task<ApiResponse<TContent>> SignInAsync<TContent, TSend>(TSend content);
        Task<ApiResponse> PutWithNotificationAsync<TSend>(string url, TSend content);
    }
}