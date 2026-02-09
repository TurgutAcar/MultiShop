using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.AspNetCore.Http;
using MultiShop.OcelotGateway.DelegateHanders;
using System.IdentityModel.Tokens.Jwt;
using MultiShop.OcelotGateway.Extensions;
using Ocelot.Values;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks()
    .AddUrlGroup(
        new Uri("http://catalogapi/health-check"),
        name: "catalog-service",
        failureStatus: HealthStatus.Unhealthy)
    .AddUrlGroup(
        new Uri("http://stockapi/health-check"),
        name: "stock-service",
        failureStatus: HealthStatus.Unhealthy)
    .AddUrlGroup(
        new Uri("http://orderapi/health-check"),
        name: "order-service",
        failureStatus: HealthStatus.Unhealthy)
    .AddUrlGroup(
        new Uri("http://checkoutapi/health-check"),
        name: "checkout-service",
        failureStatus: HealthStatus.Unhealthy)
    .AddUrlGroup(
        new Uri("http://notificationapi/health-check"),
        name: "notification-service",
        failureStatus: HealthStatus.Unhealthy)
      .AddUrlGroup(
        new Uri("http://paymentapi/health-check"),
        name: "payment-service",
        failureStatus: HealthStatus.Unhealthy)
    .AddUrlGroup(
        new Uri("http://identityserverapi/health-check"),
        name: "identity-service",
        failureStatus: HealthStatus.Unhealthy);

builder.Services.AddAuthentication().AddJwtBearer("OcelotAuthenticationScheme", opt =>
{
    //opt.Authority = "http://identityserverapi";
    opt.Authority = "http://localhost:5001";
    opt.Audience = "ResourceOcelot";
    opt.RequireHttpsMetadata = false;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AllowAnonymousHealthCheck", policy =>
    {
        policy.RequireAssertion(context => true); // Herkese izin verir
    });
});



// YARP'ý ekliyoruz
//builder.Services.AddReverseProxy()
//  .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
builder.Configuration
    .AddJsonFile($"configuration.{builder.Environment.EnvironmentName.ToLower()}.json", optional: true)
    .AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration)
.AddDelegatingHandler<ResultNormalizationHandler>(true);
//  .AddDelegatingHandler<GatewayRetryHandler>(global: true); // 'global: true' dersen TÜM mikroservisleri kapsar!


var env = builder.Environment;


builder.Services.AddDefaultCors(env);
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});
var app = builder.Build();
var config = new OcelotPipelineConfiguration
{
    PreAuthorizationMiddleware = async (ctx, next) =>
    {
        // Notification Hub isteklerini yetkilendirme kontrolünden muaf tut
        if (ctx.Request.Path.StartsWithSegments("/notifications/hubs/checkout"))
        {
            await next.Invoke();
            return;
        }

        var method = ctx.Request.Method;
        var user = ctx.User;
        var scopes = user.FindAll("scope").Select(s => s.Value).ToList();

        var isReadMethod = method == "GET";
        var isWriteMethod = method is "POST" or "PUT" or "DELETE";
        var hasFullPermission = scopes.Contains("CatalogFullPermission");
        var hasReadPermission = scopes.Contains("CatalogReadPermission");


        if (isWriteMethod && !hasFullPermission)
        {
            ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
            await ctx.Response.WriteAsync("You do not have permission to perform this operation.");
            return;
        }
        if (isReadMethod && !(hasFullPermission || hasReadPermission))
        {
            ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
            await ctx.Response.WriteAsync("Okuma islemleri icin CatalogReadPermission veya CatalogFullPermission gerekli.");
            return;
        }



        await next.Invoke();
    }
};




app.UseHttpsRedirection();
app.UseResponseCompression();
app.UseWebSockets(); // <--- BU SATIRI EKLE (Ocelot'un üstünde olmalý)
app.UseCors();
app.MapHealthChecks("/health-check", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
    }
}).RequireAuthorization("AllowAnonymousHealthCheck");

app.UseMiddleware<ClientIdDelegateHandler>();

app.MapGet("/", () => "Hello World!");
await app.UseOcelot(config);
//app.MapReverseProxy();

app.Run();
