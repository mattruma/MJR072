using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.Helpers;

namespace WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            //builder.Services.AddScoped(options =>
            //{
            //    var httpClient = new HttpClient { BaseAddress = new Uri(builder.Configuration["API_URL"]) };

            //    httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", builder.Configuration["API_KEY"]);
            //    httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Trace", "true");

            //    return httpClient;
            //});

            builder.Services.AddScoped<CustomAuthorizationMessageHandler>();

            builder.Services.AddHttpClient("webapi", options =>
            {
                options.BaseAddress = new Uri(builder.Configuration["API_URL"]);

                options.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", builder.Configuration["API_KEY"]);
                options.DefaultRequestHeaders.Add("Ocp-Apim-Trace", "true");
            })
            .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

            builder.Services.AddScoped(options => options.GetRequiredService<IHttpClientFactory>()
                .CreateClient("webapi"));

            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);

                options.ProviderOptions.DefaultAccessTokenScopes.Add("d5070be4-48dc-4618-beb4-853df62818dc/All");
            });

            await builder.Build().RunAsync();
        }
    }
}
