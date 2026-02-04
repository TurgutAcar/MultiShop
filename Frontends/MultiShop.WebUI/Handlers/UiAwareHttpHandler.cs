using MultiShop.Shared.Enums;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.GlobalException;
using System.Net;
using System.Text.Json;

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
            {
                Result<string>? result = null;
                var method = request.Method;

                var body = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrWhiteSpace(body))
                 result = JsonSerializer.Deserialize<Result<string>>(body);
                var criticality = result?.Criticality ?? UiCriticality.Medium;
                if (method == HttpMethod.Post || method == HttpMethod.Put || method == HttpMethod.Delete)
                    criticality = UiCriticality.Low;

                if (criticality == UiCriticality.High)
                    throw new UiCriticalException(UiCriticality.High,
                                result!=null?result.ErrorMessages : new[] { "Sistemsel bir hata oluştu." });
                if (criticality == UiCriticality.Medium)
                    throw new UiCriticalException(UiCriticality.Medium,
                    result != null ? result.ErrorMessages : new[] { "Servis geçici olarak yanıt vermiyor." });

            }


            return response;
        }
    }


}
