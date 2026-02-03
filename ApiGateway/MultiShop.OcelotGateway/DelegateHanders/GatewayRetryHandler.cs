using System.Net;
using Polly;
using Polly.Retry;

namespace MultiShop.OcelotGateway.DelegateHanders
{

    public class GatewayRetryHandler : DelegatingHandler
    {
        private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;

        public GatewayRetryHandler()
        {
            // Gateway seviyesinde 429 veya 5xx hataları için bekleme politikası
            _retryPolicy = Policy
                .HandleResult<HttpResponseMessage>(r =>
                    r.StatusCode == HttpStatusCode.TooManyRequests ||
                    (int)r.StatusCode >= 500)
                .WaitAndRetryAsync(3, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Ocelot üzerinden geçen her istek bu politikanın içinden geçer
            return await _retryPolicy.ExecuteAsync(() => base.SendAsync(request, cancellationToken));
        }
    }
}
