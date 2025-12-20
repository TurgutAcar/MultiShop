using Microsoft.AspNetCore.RateLimiting;
using MultiShop.Services.Stock.Core.Application.Services;
using Serilog;
using MultiShop.Services.Stock.Presentation.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using MultiShop.Services.Stock.Persistence;

// Add services to the container.
const string CspPolicy = "default-src 'self'; " +
                         "script-src 'self' 'unsafe-inline'; " + // unsafe-inline'ý kaçýnmak için nonce/hash kullanmak daha iyidir
                         "style-src 'self'; " +
                         "img-src 'self'; " +
                         "object-src 'none'; " + // Eklentileri (flash/java) engeller
                         "frame-ancestors 'none'; " + // Clickjacking'i önler (X-Frame-Options yerine)
                         "upgrade-insecure-requests;"; // Tüm HTTP isteklerini HTTPS'e yükseltir
var builder = WebApplication.CreateBuilder(args);
JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.Audience = "ResourceOrder";
    options.Authority = builder.Configuration["IdentityServerUrl"];
});

builder.Services.AddRateLimiter(options =>
{
    // Mevcut Fixed Window Limiter
    options.AddFixedWindowLimiter("fixed", fixedOptions =>
    {
        fixedOptions.PermitLimit = 100;
        fixedOptions.Window = TimeSpan.FromSeconds(1);
    });

    // YENÝ: Sliding Window Limiter Ekleme
    options.AddSlidingWindowLimiter("sliding", slidingOptions =>
    {
        slidingOptions.PermitLimit = 10;          // 10 isteðe izin ver
        slidingOptions.Window = TimeSpan.FromSeconds(15); // 15 saniyelik pencere
        slidingOptions.SegmentsPerWindow = 3;     // Pencereyi 3 segmente böl (Her segment 5 saniye)
        slidingOptions.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
        slidingOptions.QueueLimit = 0;           // Kuyruk yok, limit aþýlýrsa hemen reddet
    });
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddControllers();
builder.Host.UseSerilog((context, services, configuration) => configuration
    // ?? Konfigürasyon dosyasýndan tüm Serilog bloðunu okur
    .ReadFrom.Configuration(context.Configuration)
    .Enrich.FromLogContext()
// Not: Artýk .WriteTo.Console() veya .MinimumLevel.Warning() gibi 
// ayarlarý burada tutmanýza gerek yok, hepsi appsettings.json'da!
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// CSP Baþlýðýný Uygulama: app.Use'dan sonra eklenmelidir.
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value?.ToLowerInvariant();
    var query = context.Request.QueryString.Value?.ToLowerInvariant();
    var headers = context.Request.Headers;

    // Basit bir kara liste (blacklisting) örneði, 
    // ancak whitelisting her zaman daha güvenlidir.

    // A. URL Sorgu Dizisi Kontrolü (SQL/XSS Enjeksiyonu)
    if (query != null && (query.Contains("<script") || query.Contains("select * from") || query.Contains("exec(")))
    {
        context.Response.StatusCode = 400; // Bad Request
        await context.Response.WriteAsync("Geçersiz sorgu parametresi.");
        return;
    }

    // B. Header Boyut ve Karakter Kontrolü (Buffer Overflow/Geçersiz Format)
    if (headers.ContentLength > 1024 * 1024 * 5) // Maksimum 5MB gövde boyutu
    {
        context.Response.StatusCode = 413; // Payload Too Large
        await context.Response.WriteAsync("Ýstek gövdesi boyutu çok büyük.");
        return;
    }

    // C. Yalnýzca Ýzin Verilen HTTP Metotlarýný Kabul Etme (Whitelisting)
    var allowedMethods = new[] { "GET", "POST", "DELETE", "PUT" };
    if (!allowedMethods.Contains(context.Request.Method))
    {
        context.Response.StatusCode = 405; // Method Not Allowed
        await context.Response.WriteAsync("Ýzin verilmeyen HTTP metodu.");
        return;
    }
    // Yalnýzca HTML yanýtlarýna CSP baþlýðýný ekleyin
    context.Response.Headers.Append("Content-Security-Policy", CspPolicy);
    if (!context.Response.Headers.ContainsKey("X-Content-Type-Options"))
    {
        context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    }
    if (!context.Response.Headers.ContainsKey("X-Frame-Options"))
    {
        // DENY deðeri kullanýlýr: Sayfanýn hiçbir þekilde frame içinde yüklenmesine izin verilmez.
        context.Response.Headers.Append("X-Frame-Options", "DENY");
    }
    // 'strict-origin-when-cross-origin' en güvenli ve yaygýn kullanýlan deðerdir.
    // Ayný kökene giderken tam URL'i gönder, farklý kökene giderken sadece kökeni (protokol+domain) gönder.
    if (!context.Response.Headers.ContainsKey("Referrer-Policy"))
    {
        context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
    }
    await next();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandler();

app.MapControllers().RequireRateLimiting("sliding").RequireAuthorization();
app.Run();
