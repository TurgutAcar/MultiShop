using MassTransit;
using MassTransit.SqlTransport.Topology;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MultiShop.Catalog.OutboxWorker.Entities;
using MultiShop.Catalog.OutboxWorker.Infrastructure.Settings;
using System.Text.Json;

namespace MultiShop.Catalog.OutboxWorker.Workers
{
    public sealed class OutboxPublisherWorker : BackgroundService
    {
        private readonly IMongoCollection<OutboxMessage> _outboxMessageCollection;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly OutboxOptions _options;

        public OutboxPublisherWorker(
            IDatabaseSettings databaseSettings,
            IOptions<OutboxOptions> outboxOptions,
            IServiceScopeFactory scopeFactory)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _outboxMessageCollection = database.GetCollection<OutboxMessage>(databaseSettings.OutboxMessageCollectionName);


            _scopeFactory = scopeFactory;
            _options = outboxOptions.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;

                //  ATOMIC LOCK (replica-safe)
                var message = await _outboxMessageCollection.FindOneAndUpdateAsync(
                    filter: Builders<OutboxMessage>.Filter.And(
                        Builders<OutboxMessage>.Filter.Eq(x => x.Status, "Pending"),
                        Builders<OutboxMessage>.Filter.Or(
                            Builders<OutboxMessage>.Filter.Eq(x => x.LockedUntil, null),
                            Builders<OutboxMessage>.Filter.Lt(x => x.LockedUntil, now)
                        )
                    ),
                    update: Builders<OutboxMessage>.Update
                        .Set(x => x.Status, "Processing")
                        .Set(x => x.LockedUntil, now.AddSeconds(_options.LockDurationSeconds)),
                    options: new FindOneAndUpdateOptions<OutboxMessage>
                    {
                        ReturnDocument = ReturnDocument.After
                    },
                    cancellationToken: stoppingToken
                );

                if (message == null)
                {
                    await Task.Delay(
                        TimeSpan.FromSeconds(_options.PollIntervalSeconds),
                        stoppingToken
                    );
                    continue;
                }

                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

                    var eventType = Type.GetType(
                        $"MultiShop.Shared.Events.{message.Type}, MultiShop.Shared"
                    );

                    var @event = JsonSerializer.Deserialize(message.Payload, eventType!);

                    await publishEndpoint.Publish(@event!, stoppingToken);

                    await _outboxMessageCollection.UpdateOneAsync(
                        x => x.Id == message.Id,
                        Builders<OutboxMessage>.Update
                            .Set(x => x.Status, "Published")
                            .Set(x => x.ProcessedOn, DateTime.UtcNow)
                            .Unset(x => x.LockedUntil),
                        cancellationToken: stoppingToken
                    );
                }
                catch (Exception ex)
                {
                    // retry-friendly
                    await _outboxMessageCollection.UpdateOneAsync(
                        x => x.Id == message.Id,
                        Builders<OutboxMessage>.Update
                            .Set(x => x.Status, "Pending")
                            .Unset(x => x.LockedUntil),
                        cancellationToken: stoppingToken
                    );

                    Console.WriteLine(ex);
                }
            }
        }
    }


}
