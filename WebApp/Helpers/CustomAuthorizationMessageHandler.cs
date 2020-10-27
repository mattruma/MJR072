using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;

namespace WebApp.Helpers
{
    public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public CustomAuthorizationMessageHandler(
            IConfiguration configuration,
            IAccessTokenProvider accessTokenProvider,
            NavigationManager navigationManager) : base(accessTokenProvider, navigationManager)
        {
            this.ConfigureHandler(
                authorizedUrls: new[] { configuration["API_URL"] });
        }
    }
}