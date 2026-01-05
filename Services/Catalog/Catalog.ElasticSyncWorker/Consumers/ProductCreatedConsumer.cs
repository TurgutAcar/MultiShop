using MassTransit;
using Elastic.Clients.Elasticsearch;
using MultiShop.Shared.Events;

namespace MultiShop.Catalog.ElasticSyncWorker.Consumers
{

    public class ProductCreatedConsumer(
           ElasticsearchClient _elasticClient
) : IConsumer<ProductCreatedEvent>
    {

        public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            Console.WriteLine("EVENT GELDİ → " + context.Message.ProductId);

            await _elasticClient.IndexAsync(context.Message, i => i
                .Index("products")
                .Id(context.Message.ProductId)
            );
        }

    }

}
