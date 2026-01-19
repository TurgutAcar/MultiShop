using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MultiShop.Notification.Consumers;
using MultiShop.Notification.Health;
using MultiShop.Notification.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecks();
    //.AddCheck<SignalRHealthCheck>(
    //    name: "signalr",
    //    tags: new[] { "realtime" });
    //).AddRabbitMQ(
    //    "rabbitmq:5672",
    //    name: "rabbitmq",
    //    tags: new[] { "cache", "rabbitmq" }
    //); ;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddCors(options => {
    options.AddPolicy("GatewayPolicy", policy => {
        policy.SetIsOriginAllowed(origin => true)
        //policy.WithOrigins("http://localhost:5000") // API Gateway URL'in
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // SignalR için bu þart!
    });
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCompletedConsumer>();
    x.AddConsumer<StockReservationFailedConsumer>();
    x.AddConsumer<PaymentFailedConsumer>();

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

app.UseCors("GatewayPolicy");
app.MapControllers();
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
app.MapHub<CheckoutHub>("/hubs/checkout");
app.Run();
