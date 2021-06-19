using Blazored.LocalStorage;
using Ilex.Client.ApiCaller;
using Ilex.Client.CustomAuth;
using Ilex.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ilex.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddScoped<IApiCall ,ApiCall>();

            //Custom AuthenticationStateProvider 
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

            builder.Services.AddScoped<TestApiCall>();

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddMudServices();

            await builder.Build().RunAsync();
        }
    }
}
