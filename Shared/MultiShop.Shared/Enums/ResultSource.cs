namespace MultiShop.Shared.Enums
{
    public enum ResultSource
    {
        Success,
        RateLimited,        // 429
        Unauthorized,       // 401
        Forbidden,          // 403
        NotFound,           // 404
        ValidationError,    // 400
        ServiceUnavailable, // 503
        Timeout,            // network
        ApiError,           // 5xx
        UnknownError
    }

}
