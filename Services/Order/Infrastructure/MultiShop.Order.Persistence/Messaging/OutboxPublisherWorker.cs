//using AutoMapper;
//using Elastic.Clients.Elasticsearch;
//using MongoDB.Driver;
//using MultiShop.Catalog.Domain.Entities;
//using MultiShop.Catalog.Infrastructure.Settings;
//using System.Text.Json;

//namespace MultiShop.Catalog.Infrastructure.Messaging
//{
//    public class OutboxPublisherWorker : BackgroundService
//    {
//        private readonly IMongoCollection<OutboxMessage> _outboxMessageCollection;
//        private readonly IEventBus _eventBus;

//        public OutboxPublisherWorker(IDatabaseSettings databaseSettings, ElasticsearchClient es, IEventBus eventBus)
//        {
//            var client = new MongoClient(databaseSettings.ConnectionString);
//            var database = client.GetDatabase(databaseSettings.DatabaseName);
//            _outboxMessageCollection = database.GetCollection<OutboxMessage>(databaseSettings.CategoryCollectionName);
//            _eventBus = eventBus;
//        }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            while (!stoppingToken.IsCancellationRequested)
//            {
//                //  Atomic lock al
//                var message = await _outboxMessageCollection.FindOneAndUpdateAsync(
//                    filter: Builders<OutboxMessage>.Filter.Eq(x => x.Status, "Pending"),
//                    update: Builders<OutboxMessage>.Update
//                        .Set(x => x.Status, "Processing")
//                        .Set(x => x.ProcessedOn, DateTime.UtcNow),
//                    options: new FindOneAndUpdateOptions<OutboxMessage>
//                    {
//                        ReturnDocument = ReturnDocument.After
//                    },
//                    cancellationToken: stoppingToken
//                );

//                //  İşlenecek mesaj yok
//                if (message == null)
//                {
//                    await Task.Delay(1000, stoppingToken);
//                    continue;
//                }

//                try
//                {
//                    //  Event’i resolve et
//                    var type = Type.GetType(
//                        $"MultiShop.Shared.Events.Dtos.{message.Type}, MultiShop.Shared"
//                    );

//                    var @event = JsonSerializer.Deserialize(message.Payload, type!);

//                    // Publish
//                    await _eventBus.PublishAsync(@event!);

//                    //  Başarılı işaretle
//                    await _outboxMessageCollection.UpdateOneAsync(
//                        x => x.Id == message.Id,
//                        Builders<OutboxMessage>.Update
//                            .Set(x => x.Status, "Published"),
//                        cancellationToken: stoppingToken
//                    );
//                }
//                catch (Exception ex)
//                {
//                    //  Hata aldıysa tekrar Pending yap (retry için)
//                    await _outboxMessageCollection.UpdateOneAsync(
//                        x => x.Id == message.Id,
//                        Builders<OutboxMessage>.Update
//                            .Set(x => x.Status, "Pending")
//                            .Set(x => x.ProcessedOn, null),
//                        cancellationToken: stoppingToken
//                    );

//                    Console.WriteLine(ex);
//                }
//            }
//        }

//    }

//}
