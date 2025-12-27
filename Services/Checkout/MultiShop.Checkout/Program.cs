using MassTransit;
using MultiShop.Checkout.Messaging;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Program.cs (Checkout Service)
builder.Services.AddMassTransit(x =>
{
    x.AddSagaStateMachine<CheckoutStateMachine, CheckoutState>()
        .RedisRepository(r =>
        {
            // ConnectionFactory bir metot, çaðrý þeklinde kullanýlýr
            r.ConnectionFactory(() => ConnectionMultiplexer.Connect("localhost:6379"));

            r.KeyPrefix = "checkout-saga";
        });

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", 5673, "/", h => {
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

app.Run();
