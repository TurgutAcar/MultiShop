using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using MultiShop.WebUI.GlobalException;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Settings;
using static IdentityModel.OidcConstants;

namespace MultiShop.WebUI.Services.Concrete
{
    public class ClientCredentialTokenService : IClientCredentialTokenService
    {
        private readonly ServiceApiSettings _serviceApiSettings;
        private readonly HttpClient _httpClient;
        private readonly IClientAccessTokenCache _clientAccessTokenCache;
        private readonly ClientSettings _clientSettings;

        public ClientCredentialTokenService(IOptions<ClientSettings> clientSettings, 
            IClientAccessTokenCache clientAccessTokenCache, HttpClient httpClient,IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _clientSettings = clientSettings.Value;
            _clientAccessTokenCache = clientAccessTokenCache;
            _httpClient = httpClient;
            _serviceApiSettings = serviceApiSettings.Value;
        }

        public async Task ClearToken()
        {
            await _clientAccessTokenCache.DeleteAsync("multishoptoken");

        }

        public async Task<string?> GetToken()
        {
            var currentToken = await _clientAccessTokenCache.GetAsync("multishoptoken");
            if(currentToken != null)
            {
                return currentToken.AccessToken;
            }
            var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityServerUrl,
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false
                }
            });
            if (discoveryEndPoint.IsError)
                throw new UiCriticalException("Discovery endpoint erişilemedi.");
            var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = _clientSettings.MultiShopVisitorClient.ClientId,
                ClientSecret = _clientSettings.MultiShopVisitorClient.ClientSecret,
                Address = discoveryEndPoint.TokenEndpoint
            };
            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);
            if (tokenResponse.IsError)
                throw new UiCriticalException("Client token alınamadı.");
            await _clientAccessTokenCache.SetAsync("multishoptoken", tokenResponse.AccessToken, tokenResponse.ExpiresIn);
            return tokenResponse.AccessToken;
        }
    }
}
