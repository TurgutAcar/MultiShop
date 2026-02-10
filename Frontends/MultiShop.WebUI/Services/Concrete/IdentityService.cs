using System.Security.Claims;
using System.Text.Json;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.Shared.Dtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Settings;

namespace MultiShop.WebUI.Services.Concrete
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClientSettings _clientSettings;
        private readonly ServiceApiSettings _serviceApiSettings;
        public IdentityService(HttpClient _httpClient, IHttpContextAccessor _httpContextAccessor,
               IOptions<ClientSettings> clientSettings, IOptions<ServiceApiSettings> serviceApiSettings)
        {
            this._serviceApiSettings = serviceApiSettings.Value;
            this._clientSettings = clientSettings.Value;
            this._httpClient = _httpClient; 
            this._httpContextAccessor = _httpContextAccessor;
        }
        public async Task<bool> GetRefreshToken()
        {
            var refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            if (string.IsNullOrEmpty(refreshToken))
                return false;
            var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address=_serviceApiSettings.IdentityServerUrl,
                Policy = new DiscoveryPolicy
                {
                    RequireHttps=false
                }
            });
            RefreshTokenRequest refreshTokenRequest = new()
            {
                ClientId = _clientSettings.MultiShopWebClient.ClientId,
                ClientSecret=_clientSettings.MultiShopWebClient.ClientSecret,
                RefreshToken=refreshToken,
                Address=discoveryEndPoint.TokenEndpoint,
                Scope = _clientSettings.MultiShopWebClient.Scopes

            };
            var token = await _httpClient.RequestRefreshTokenAsync(refreshTokenRequest);

            var authenticationToken = new List<AuthenticationToken>()
            {
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.AccessToken,
                    Value=token.AccessToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.RefreshToken,
                    Value=token.RefreshToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.ExpiresIn,
                    Value=DateTime.Now.AddSeconds(token.ExpiresIn).ToString()
                }
            };
            var result = await _httpContextAccessor.HttpContext.AuthenticateAsync();
            var properties = result.Properties;
            properties.StoreTokens(authenticationToken);
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, result.Principal, properties);
            return true;
        }

        public async Task<Result<List<string>>> SignIn(SignInDto signUpDto)
        {
            var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address=_serviceApiSettings.IdentityServerUrl,
                Policy=new DiscoveryPolicy
                {
                    RequireHttps = false
                }
            });
            var passwordTokenRequest = new PasswordTokenRequest
            {
                ClientId = _clientSettings.MultiShopWebClient.ClientId,
                ClientSecret = _clientSettings.MultiShopWebClient.ClientSecret,
                UserName = signUpDto.UserName,
                Password = signUpDto.Password,
                Address = discoveryEndPoint.TokenEndpoint,
                Scope = _clientSettings.MultiShopWebClient.Scopes
            };
            var token=await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest);
            if (token.IsError)
            {
                var responseContent = await token.HttpResponse.Content.ReadAsStringAsync();
                var errorDto = JsonSerializer.Deserialize<ErrorDto>(responseContent, new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });
                return Result<List<string>>.Failure(400, errorDto!.Errors);
            }
            var userInfoRequest = new UserInfoRequest
            {
                Token = token.AccessToken,
                Address = discoveryEndPoint.UserInfoEndpoint,

            };
            var userValues=await _httpClient.GetUserInfoAsync(userInfoRequest);
           
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(userValues.Claims
                ,CookieAuthenticationDefaults.AuthenticationScheme,"name","role");

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            // var authenticationProperties = new AuthenticationProperties();
            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(token.ExpiresIn) 
            };
            authenticationProperties.StoreTokens(new List<AuthenticationToken>()
            {
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.AccessToken,
                    Value=token.AccessToken
                },
                new AuthenticationToken
                {
                     Name=OpenIdConnectParameterNames.ExpiresIn,
                    Value=DateTime.Now.AddSeconds(token.ExpiresIn).ToString()
                }
            });
            authenticationProperties.IsPersistent = false;

            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

            return Result<List<string>>.Succeed(
                    new() { "Giriş başarılı." });
        }
    }
}
