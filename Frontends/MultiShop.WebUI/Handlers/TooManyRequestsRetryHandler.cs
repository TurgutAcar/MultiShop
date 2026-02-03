using System.Net;

namespace MultiShop.WebUI.Handlers
{
    //public class TooManyRequestsRetryHandler : DelegatingHandler
    //{
    //    private const int MaxRetry = 2;

    //    protected override async Task<HttpResponseMessage> SendAsync(
    //        HttpRequestMessage request,
    //        CancellationToken cancellationToken)
    //    {
    //        for (int attempt = 0; attempt <= MaxRetry; attempt++)
    //        {
    //            var response = await base.SendAsync(request, cancellationToken);

    //            if (response.StatusCode != HttpStatusCode.TooManyRequests)
    //                return response;

    //            if (attempt == MaxRetry)
    //                return response;

    //            var delay =
    //                response.Headers.RetryAfter?.Delta ??
    //                TimeSpan.FromSeconds(2);

    //            await Task.Delay(delay, cancellationToken);
    //        }

    //        throw new InvalidOperationException("Retry handler reached invalid state.");
    //    }
    //}
    public class TooManyRequestsRetryHandler : DelegatingHandler
    {
        private const int MaxRetry = 2;

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            for (int attempt = 0; attempt <= MaxRetry; attempt++)
            {
                var response = await base.SendAsync(request, cancellationToken);

                if ((int)response.StatusCode >= 500)
                    return response;

                if (attempt == MaxRetry)
                    return response;

                var delay =
                    response.Headers.RetryAfter?.Delta ??
                    TimeSpan.FromSeconds(2);

                await Task.Delay(delay, cancellationToken);
            }

            throw new InvalidOperationException();
        }
    }



}
