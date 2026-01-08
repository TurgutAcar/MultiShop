using MassTransit;
using MultiShop.Checkout.Messaging;
using StackExchange.Redis;
using MassTransit.QuartzIntegration;
using Quartz;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.Audience = "ResourceCheckout";
    options.Authority = builder.Configuration["IdentityServerUrl"];
});
builder.Services.AddHealthChecks()
    .AddRedis(
        "checkoutdb:6379",
        name: "redis",
        tags: new[] { "cache", "redis" }
    )
 .AddRabbitMQ(
        "rabbitmq:5672",
        name: "rabbitmq",
        tags: new[] { "cache", "rabbitmq" }
    );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Program.cs (Checkout Service)
builder.Services.AddQuartz(q =>
{
    // Basit bir thread pool ile scheduler kur
    q.UseMicrosoftDependencyInjectionJobFactory();
});

builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
});
builder.Services.AddMassTransit(x =>
{
    x.AddSagaStateMachine<CheckoutStateMachine, CheckoutState>()
        .RedisRepository(r =>
        {
            // ConnectionFactory bir metot, çaðrý þeklinde kullanýlýr
            r.ConnectionFactory(() => ConnectionMultiplexer.Connect("checkoutdb:6379"));

            r.KeyPrefix = "checkout-saga";
        });
    x.AddMessageScheduler(new Uri("queue:quartz"));
    x.AddQuartzConsumers();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", 5672, "/", h => {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.UseMessageScheduler(new Uri("queue:quartz"));

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
app.UseAuthentication();
app.UseAuthorization();
app.MapHealthChecks("/health-check", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
    }
});
app.MapControllers();

app.Run();
