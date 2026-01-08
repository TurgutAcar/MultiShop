using HealthChecks.UI.Client;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MultiShop.Catalog.OutboxWorker.Infrastructure.Settings;
using MultiShop.Catalog.OutboxWorker.Workers;

var builder = Host.CreateApplicationBuilder(args);

// CONFIG

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

//builder.Services.Configure<MongoOptions>(
//    builder.Configuration.GetSection("Mongo"));

builder.Services.Configure<OutboxOptions>(
    builder.Configuration.GetSection("Outbox"));
builder.Services.AddHealthChecks()
  .AddRabbitMQ(
      "rabbitmq:5672",
      name: "rabbitmq",
      tags: new[] { "cache", "rabbitmq" }
  );
//// MONGO CLIENT
//builder.Services.AddSingleton<IMongoClient>(sp =>
//{
//    var mongo = sp.GetRequiredService<
//        Microsoft.Extensions.Options.IOptions<MongoOptions>>().Value;

//    return new MongoClient(mongo.ConnectionString);
//});

// MASS TRANSIT (SADECE PUBLISH)
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", 5672, "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ConfigureEndpoints(context);
    });
});

// OUTBOX WORKER
builder.Services.AddHostedService<OutboxPublisherWorker>();

var host = builder.Build();
host.Run();
