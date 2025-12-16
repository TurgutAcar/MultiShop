using Catalog.ElasticSyncWorker;
using Elastic.Clients.Elasticsearch;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<ElasticSyncWorker>();
// Elasticsearch Ayar
var esSettings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"))
                    .DefaultIndex("products");

var esClient = new ElasticsearchClient(esSettings);

// DI Container kayd
builder.Services.AddSingleton(esClient);

var host = builder.Build();
host.Run();
