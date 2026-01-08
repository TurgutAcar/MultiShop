using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecksUI(setup =>
{
    setup.SetEvaluationTimeInSeconds(30);
    setup.MaximumHistoryEntriesPerEndpoint(50);

    setup.AddHealthCheckEndpoint("Catalog API", "http://catalogapi/health-check");
    setup.AddHealthCheckEndpoint("Identity API", "http://identityserverapi/health-check");
    setup.AddHealthCheckEndpoint("Gateway API", "http://gatewayapi/health-check");
    setup.AddHealthCheckEndpoint("Stock API", "http://stockapi/health-check");
    setup.AddHealthCheckEndpoint("Order API", "http://orderapi/health-check");
    setup.AddHealthCheckEndpoint("Notification API", "http://notificationapi/health-check");
    setup.AddHealthCheckEndpoint("Checkout API", "http://checkoutapi/health-check");
    setup.AddHealthCheckEndpoint("Payment API", "http://paymentapi/health-check");

})
.AddInMemoryStorage(); 

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
app.MapHealthChecksUI(options =>
{
    options.UIPath = "/health-ui";
    options.ApiPath = "/health-ui-api";
});

app.Run();
