using Elastic.Clients.Elasticsearch;
using MassTransit;
using MultiShop.Catalog.ElasticSyncWorker.Consumers;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHealthChecks();
//  .AddRabbitMQ(
//      "rabbitmq:5672",
//      name: "rabbitmq",
//      tags: new[] { "cache", "rabbitmq" }
//  ).AddElasticsearch(
//      "http://elasticsearch:9200",
//      name: "rabbitmq",
//      tags: new[] { "cache", "elasticsearch" }
//  );
builder.Services.AddMassTransit(x =>
{
    // Consumer'ý ekle
    x.AddConsumer<ProductCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", 5672, "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        // GLOBAL POLICIES (tüm consumer’lara uygulanýr)
        cfg.UseMessageRetry(r =>
        {
            r.Interval(3, TimeSpan.FromSeconds(10));
        });

        cfg.UseDelayedRedelivery(r =>
        {
            r.Intervals(
                TimeSpan.FromMinutes(1),
                TimeSpan.FromMinutes(5)
            );
        });

        // EVENT  QUEUE  CONSUMER baðlamasýný otomatik yapar
        cfg.ConfigureEndpoints(context);
    });
});



//builder.Services.AddHostedService<ElasticSyncWorker>();
// Elasticsearch Ayar
var esSettings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"))
                    .DefaultIndex("products");

var esClient = new ElasticsearchClient(esSettings);

// DI Container kayd
builder.Services.AddSingleton(esClient);

var host = builder.Build();
host.Run();
