using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using MultiShop.Services.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecks();
builder.WebHost.UseUrls("https://0.0.0.0:7076", "http://0.0.0.0:5076");
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(7076, listenOptions =>
    {
        var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
        store.Open(OpenFlags.ReadOnly);

        var cert = store.Certificates
            .Find(X509FindType.FindByThumbprint, "D5E5FE11E6E22592F6A9F2C6824FE7C68BBDFADF", false)
            .OfType<X509Certificate2>()
            .First();

        listenOptions.UseHttps(cert);
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true, // Bunu eklemeyi unutma
            ValidIssuer = "tetra-api.com",
            ValidAudience = "tetra-api.com",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("BuSizinCokGizliVeGucluAnahtariniz123!")),
            ClockSkew = TimeSpan.Zero // Zaman farký toleransýný kapat
        };
    });
//.AddRabbitMQ(
//     "rabbitmq:5672",
//     name: "rabbitmq",
//     tags: new[] { "cache", "rabbitmq" }
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(x =>
{
    // O servise ait Consumer'ý ekle
    x.AddConsumer<PaymentServiceRequestedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", 5672, "/", h => {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.MapHealthChecks("/health-check", new HealthCheckOptions //ACILACAK
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    ResultStatusCodes =
    {
        [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Healthy] = StatusCodes.Status200OK,
        [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded] = StatusCodes.Status200OK,
        [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
    }
});
app.Run();
