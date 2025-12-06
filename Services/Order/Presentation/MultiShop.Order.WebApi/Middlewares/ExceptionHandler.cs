using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using MultiShop.Shared.Responses;
using System.Text.Json;
using Microsoft.Extensions.Logging; 
namespace MultiShop.Order.WebApi.Middlewares
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger; // 👈 ILogger eklendi

        public ExceptionHandler(ILogger<ExceptionHandler> logger) // 👈 Constructor'a ekle
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            Result<string> errorResult;

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = 500;

            if(exception.GetType()== typeof(ValidationException))
            {
                httpContext.Response.StatusCode = 403;
                errorResult = Result<string>.Failure(403, ((ValidationException)exception).Errors.Select(s => s.PropertyName).ToList());
                var validationErrors = ((ValidationException)exception).Errors.Select(s => s.PropertyName).ToList();
                // 📝 Loglama: Doğrulama hataları genellikle uyarı seviyesinde (Warning) loglanır.
                // Çünkü bu hatalar dışarıdan gelen kötü niyetli veya yanlış veriyi gösterir.
                _logger.LogWarning(
                    exception,
                    "Doğrulama Hatası (403/400): {ErrorCount} alan doğrulanamadı. Alanlar: {Fields}. İstek Yolu: {Path}",
                    validationErrors.Count,
                    string.Join(", ", validationErrors),
                    httpContext.Request.Path
                );
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorResult));

                return true;
            }
            errorResult = Result<string>.Failure(exception.Message);
            // 💥 Loglama: Uygulama düzeyindeki beklenmedik hatalar (NullReference, DB Bağlantı vb.)
            // Kritik veya Hata seviyesinde (Error/Critical) loglanmalıdır.
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
