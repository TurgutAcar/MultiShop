using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MultiShop.WebUI.Services.Interface;

namespace MultiShop.WebUI.Handlers
{
    public class ResourceOwnerPasswordTokenHandler:DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityService _identityService;

        public ResourceOwnerPasswordTokenHandler(IIdentityService identityService, IHttpContextAccessor httpContextAccessor)
        {
            _identityService = identityService;
            _httpContextAccessor = httpContextAccessor;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            request.Headers.Authorization=new AuthenticationHeaderValue("Bearer",accessToken);

            if (string.IsNullOrEmpty(accessToken))
            {
                await _httpContextAccessor.HttpContext.SignOutAsync();
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);

            }
            var response= await base.SendAsync(request, cancellationToken);
            if(response.StatusCode==HttpStatusCode.Unauthorized)
            {
                var tokenResponse = await _identityService.GetRefreshToken();
                if(tokenResponse)
                {
                    var newAccessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
                    if (string.IsNullOrEmpty(newAccessToken))
                        return new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newAccessToken);
                    response = await base.SendAsync(request, cancellationToken);
                }
                await _httpContextAccessor.HttpContext.SignOutAsync();

                return new HttpResponseMessage(HttpStatusCode.Unauthorized);

            }
            if (response.StatusCode==HttpStatusCode.Forbidden)
            {

            }
            return response;
        }
    }
}
