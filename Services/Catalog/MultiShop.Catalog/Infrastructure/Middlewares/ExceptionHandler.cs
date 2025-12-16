using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using MultiShop.Shared.Responses;
using System.Text.Json;

namespace MultiShop.Catalog.Infrastructure.Middlewares
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger; 

        public ExceptionHandler(ILogger<ExceptionHandler> logger) 
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            Result<string> errorResult;

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = 500;

            if (exception.GetType() == typeof(ValidationException))
            {
                httpContext.Response.StatusCode = 403;
                errorResult = Result<string>.Failure(403, ((ValidationException)exception).Errors.Select(s => s.PropertyName).ToList());
                var validationErrors = ((ValidationException)exception).Errors.Select(s => s.PropertyName).ToList();
                _logger.LogWarning(
                    exception,
                    "Doğrulama Hatası (403/400): {ErrorCount} alan doğrulanamadı. Alanlar: {Fields}. İstek Yolu: {Path}",
                    validationErrors.Count,
                    string.Join(", ", validationErrors),
                    httpContext.Request.Path
                );
                return true;
            }
            errorResult = Result<string>.Failure(exception.Message);
            _logger.LogError(
               exception,
               "Kritik Sunucu Hatası (500): {ExceptionMessage}. İstek Yolu: {Path}",
               exception.Message,
               httpContext.Request.Path
           );
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorResult));
            return true;

        }
    }
}