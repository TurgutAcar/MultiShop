using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MultiShop.WebUI.GlobalException;
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
            if (string.IsNullOrEmpty(accessToken))
                throw new UiCriticalException("Oturum bulunamadı.");
            request.Headers.Authorization=new AuthenticationHeaderValue("Bearer",accessToken);

           
            var response= await base.SendAsync(request, cancellationToken);
            if(response.StatusCode==HttpStatusCode.Unauthorized)
            {
                var tokenResponse = await _identityService.GetRefreshToken();
                if(tokenResponse)
                {
                    var newAccessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
                    if (string.IsNullOrEmpty(newAccessToken))
                        throw new UiCriticalException("Yeni token alınamadı.");
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newAccessToken);
                    response = await base.SendAsync(request, cancellationToken);
                }
                    throw new UiCriticalException("Oturum süresi doldu.");


            }
            if (response.StatusCode==HttpStatusCode.Forbidden)
            {
                throw new UiCriticalException("Bu işlem için yetkiniz yok.");
            }
            return response;
        }
    }
}
