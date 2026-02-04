using System.Net;

namespace MultiShop.Shared.Enums
{
    public enum ResultSource
    {
        Success=200,
        RateLimited=429,        // 429
        Unauthorized=401,       // 401
        Forbidden=403,          // 403
        NotFound=404,           // 404
        ValidationError=400,    // 400
        ServiceUnavailable=503, // 503
        Timeout= HttpStatusCode.RequestTimeout,            // network
        Error=500,           // 5xx
        UnknownError
    }

}
