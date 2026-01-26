using System.Net;
using System.Net.Http.Headers;
using MultiShop.WebUI.GlobalException;
using MultiShop.WebUI.Services.Interface;

namespace MultiShop.WebUI.Handlers
{
    //public class ClientCredentialTokenHandler:DelegatingHandler
    //{
    //    private readonly IClientCredentialTokenService _clientCredentialTokenService;

    //    public ClientCredentialTokenHandler(IClientCredentialTokenService clientCredentialTokenService)
    //    {
    //        _clientCredentialTokenService = clientCredentialTokenService;
    //    }

    //    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    //    {
    //        var token = await _clientCredentialTokenService.GetToken();
    //        if (string.IsNullOrEmpty(token))
    //            return new HttpResponseMessage(HttpStatusCode.Unauthorized);

    //        request.Headers.Authorization =
    //            new AuthenticationHeaderValue("Bearer", token);

    //        var response = await base.SendAsync(request, cancellationToken);

    //        if (response.StatusCode == HttpStatusCode.Unauthorized)
    //        {
    //            await _clientCredentialTokenService.ClearToken();
    //            token = await _clientCredentialTokenService.GetToken();

    //            if (string.IsNullOrEmpty(token))
    //                return response;

    //            request.Headers.Authorization =
    //                new AuthenticationHeaderValue("Bearer", token);

    //            response = await base.SendAsync(request, cancellationToken);
    //        }

    //        return response;
    //        //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _clientCredentialTokenService.GetToken());
    //        //var response= await base.SendAsync(request,cancellationToken);
    //        //if(response.StatusCode ==HttpStatusCode.Unauthorized)
    //        //{
    //        //    //hata 
    //        //}
    //        //return response;
    //    }
    //}
    public class ClientCredentialTokenHandler : DelegatingHandler
    {
        private readonly IClientCredentialTokenService _tokenService;

        public ClientCredentialTokenHandler(
            IClientCredentialTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = await _tokenService.GetToken();

            if (string.IsNullOrEmpty(token))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await _tokenService.ClearToken();

                token = await _tokenService.GetToken();
                if (string.IsNullOrEmpty(token))
                    return response;

                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                return await base.SendAsync(request, cancellationToken);
            }

            return response;
        }
    }

}
