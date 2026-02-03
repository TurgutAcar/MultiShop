using Polly;
using Polly.Extensions.Http;
using System.Net;

namespace MultiShop.WebUI.Helper
{
    public static class ResiliencePolicies
    {
        static readonly HttpMethod[] IdempotentMethods =
{
    HttpMethod.Get,
    HttpMethod.Head,
    HttpMethod.Options
};
       
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return Policy<HttpResponseMessage>
     //.HandleResult(r =>
     //    IdempotentMethods.Contains(r.RequestMessage.Method) &&
     //    ((int)r.StatusCode >= 500 || r.StatusCode == HttpStatusCode.RequestTimeout || r.StatusCode == HttpStatusCode.TooManyRequests))
     .HandleResult(r =>
     {
         // Sadece Idempotent (güvenli) metotlar için hata kontrolü yapıyoruz.
         // Bu liste dışındaki (POST, PATCH gibi) metotlar asla retry edilmez.
         if (!IdempotentMethods.Contains(r.RequestMessage.Method))
             return false;

         // Eğer metot güvenliyse, şu hatalarda retry yap:
         return (int)r.StatusCode >= 500 ||
                r.StatusCode == HttpStatusCode.RequestTimeout ||
                r.StatusCode == HttpStatusCode.TooManyRequests;
         //if (r.StatusCode == HttpStatusCode.TooManyRequests &&
         //    r.Headers.Contains("X-RateLimit-Remaining"))
         //    return false; // Gateway rate limit => NO RETRY

         //return IdempotentMethods.Contains(r.RequestMessage.Method) &&
         //       ((int)r.StatusCode >= 500 || r.StatusCode == HttpStatusCode.RequestTimeout);
     })

        .WaitAndRetryAsync(3, (retryAttempt, delegateResult, context) =>
        {
            // ÖNEMLİ: delegateResult.Result doğrudan HttpResponseMessage döndürür.
            var response = delegateResult.Result;

            // Retry-After header kontrolü
            if (response?.Headers?.RetryAfter?.Delta != null)
            {
                return response.Headers.RetryAfter.Delta.Value;
            }

            // Exponential backoff + Jitter
            return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) +
                   TimeSpan.FromMilliseconds(Random.Shared.Next(0, 100));
        },
        onRetryAsync: async (response, timespan, retryCount, context) =>
        {
            // İstersen buraya log ekleyebilirsin
            await Task.CompletedTask;
        });

        }

        /// <summary>
        /// Üst üste 5 hata alındığında devreyi 30 saniye boyunca açar (istekleri engeller).
        /// </summary>
        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
    }


}

