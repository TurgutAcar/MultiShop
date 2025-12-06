using Elastic.Clients.Elasticsearch;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.Catalog.Services.ProductSearch
{
    public class ProductSearchService : IProductSearchService
    {
        private readonly ElasticsearchClient _es;

        public ProductSearchService(ElasticsearchClient es)
        {
            _es = es;
        }

        public async Task<Result<List<ProductSearchResultDto>>> SearchProductNameAsync(string query, int pageNumber = 1, int pageSize = 10)
        {
            var from = (pageNumber - 1) * pageSize;

            var response = await _es.SearchAsync<ProductSearchResultDto>(s => s
                .Index("products")
                .From(from)
                .Size(pageSize)
                .Query(q => q
                    .Bool(b => b
                        .Should(
                            sh => sh.Match(m => m.Field(f => f.ProductName).Query(query).Fuzziness(new Fuzziness("AUTO"))),
                            sh => sh.Wildcard(w => w.Field(f => f.ProductName).Value($"*{query}*"))
                        )
                    )
                )
            );

            if (!response.IsValidResponse)
                throw new Exception(response.DebugInformation);

            return response.Documents.ToList();
        }

        public async Task<Result<List<ProductSearchResultDto>>> SearchProductsAsync(
            int pageNumber,
            int pageSize,
            string sortField,
            string sortOrder)
        {
            var from = (pageNumber - 1) * pageSize;

            var response = await _es.SearchAsync<ProductSearchResultDto>(s => s
            .Index("products")
            .From(from)
            .Size(pageSize)
            .Sort(so => so.Field(sortField, f => f.Order(sortOrder == "asc" ? SortOrder.Asc : SortOrder.Desc)))
        );



            if (!response.IsValidResponse)
                throw new Exception(response.DebugInformation);

            return response.Documents.ToList();
        }


    }
}
