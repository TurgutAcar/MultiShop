using MassTransit;
using Elastic.Clients.Elasticsearch;
using MultiShop.Shared.Events;

namespace MultiShop.Catalog.ElasticSyncWorker.Consumers
{

    public class ProductUpdatedConsumer(
           ElasticsearchClient _elasticClient
) : IConsumer<ProductUpdatedEvent>
    {

        public async Task Consume(ConsumeContext<ProductUpdatedEvent> context)
        {
            Console.WriteLine("ELASTIC UPDATE → " + context.Message.ProductId);

            await _elasticClient.IndexAsync(context.Message, i => i
                 .Index("products")
                 .Id(context.Message.ProductId)
             );

        }

    }

}
