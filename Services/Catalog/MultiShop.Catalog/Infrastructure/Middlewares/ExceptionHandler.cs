using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using MongoDB.Driver;
using MultiShop.Catalog.Domain.Exceptions;
using MultiShop.Shared.Enums;
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
            UiCriticality criticality;

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = 500;

            // if (exception.GetType() == typeof(ValidationException))
            // {
            //     httpContext.Response.StatusCode = 403;
            //     errorResult = Result<string>.Failure(403, ((ValidationException)exception).Errors.Select(s => s.PropertyName).ToList());
            //     var validationErrors = ((ValidationException)exception).Errors.Select(s => s.PropertyName).ToList();
            //     _logger.LogWarning(
            //         exception,
            //         "Doğrulama Hatası (403/400): {ErrorCount} alan doğrulanamadı. Alanlar: {Fields}. İstek Yolu: {Path}",
            //         validationErrors.Count,
            //         string.Join(", ", validationErrors),
            //         httpContext.Request.Path
            //     );
            //     return true;
            // }
            // errorResult = Result<string>.Failure(exception.Message);
            // _logger.LogError(
            //    exception,
            //    "Kritik Sunucu Hatası (500): {ExceptionMessage}. İstek Yolu: {Path}",
            //    exception.Message,
            //    httpContext.Request.Path
            //);
            switch (exception)
            {
                case ValidationException validationEx:
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    criticality = UiCriticality.Low;

                    var validationErrors = validationEx.Errors
                        .Select(e => e.PropertyName)
                        .ToList();

                    errorResult = new Result<string>
                    {
                        IsSuccessful = false,
                        StatusCode = 400,
                        ErrorMessages = validationErrors,
                        Criticality = criticality
                    };

                    _logger.LogWarning(
                        validationEx,
                        "Validation hatası. Alanlar: {Fields}. Path: {Path}",
                        string.Join(", ", validationErrors),
                        httpContext.Request.Path
                    );
                    break;

                case UnauthorizedAccessException:
                    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    criticality = UiCriticality.High;

                    errorResult = Result<string>.Failure(401, "Yetkisiz erişim.");
                    errorResult.Criticality = criticality;

                    _logger.LogWarning(
                        exception,
                        "Yetkisiz erişim. Path: {Path}",
                        httpContext.Request.Path
                    );
                    break;

                case BusinessException businessEx:
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    criticality = UiCriticality.Medium;

                    errorResult = Result<string>.Failure(400, businessEx.Message);
                    errorResult.Criticality = criticality;

                    _logger.LogWarning(
                        businessEx,
                        "İş kuralı ihlali. Path: {Path}",
                        httpContext.Request.Path
                    );
                    break;

                case TimeoutException or HttpRequestException:
                    httpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                    criticality = UiCriticality.High;

                    errorResult = Result<string>.Failure(
                        503,
                        "Servise şu anda ulaşılamıyor."
                    );
                    errorResult.Criticality = criticality;

                    _logger.LogError(
                        exception,
                        "Servis erişim hatası. Path: {Path}",
                        httpContext.Request.Path
                    );
                    break;
                case MongoException mongoEx:
                case FormatException:
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    criticality = UiCriticality.Low; // Developer bug
                    errorResult = Result<string>.Failure(500, "Sistem hatası.");
                    errorResult.Criticality = criticality;

                    break;


                default:
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    criticality = UiCriticality.High;

                    errorResult = Result<string>.Failure(
                        500,
                        "Beklenmeyen bir hata oluştu."
                    );
                    errorResult.Criticality = criticality;

                    _logger.LogError(
                        exception,
                        "Kritik sistem hatası. Path: {Path}",
                        httpContext.Request.Path
                    );
                    break;
            }


            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorResult));
            return true;

        }
    }
}