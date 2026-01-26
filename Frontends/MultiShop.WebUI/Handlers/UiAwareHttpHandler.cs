using MultiShop.Shared.Enums;
using MultiShop.WebUI.GlobalException;
using System.Net;

namespace MultiShop.WebUI.Handlers
{
    public class UiAwareHttpHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new UiCriticalException(
                    UiCriticality.High,
                    new[] { "Oturum geçersiz." });

            if (response.StatusCode == HttpStatusCode.Forbidden)
                throw new UiCriticalException(
                    UiCriticality.High,
                    new[] { "Yetkiniz yok." });

            if ((int)response.StatusCode >= 500)
                throw new UiCriticalException(
                    UiCriticality.Medium,
                    new[] { "Sunucu geçici olarak yanıt vermiyor." });

            return response;
        }
    }


}
