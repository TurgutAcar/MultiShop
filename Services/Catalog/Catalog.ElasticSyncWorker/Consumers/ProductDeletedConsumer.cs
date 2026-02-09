using MassTransit;
using Elastic.Clients.Elasticsearch;
using MultiShop.Shared.Events;

namespace MultiShop.Catalog.ElasticSyncWorker.Consumers
{

    public class ProductDeletedConsumer(
           ElasticsearchClient _elasticClient
) : IConsumer<ProductDeletedEvent>
    {

        public async Task Consume(ConsumeContext<ProductDeletedEvent> context)
        {
            Console.WriteLine("ELASTIC DELETE → " + context.Message.ProductId);

            await _elasticClient.DeleteAsync<ProductDeletedEvent>(
                index: "products",
                id: context.Message.ProductId
            );
        }

    }

}
