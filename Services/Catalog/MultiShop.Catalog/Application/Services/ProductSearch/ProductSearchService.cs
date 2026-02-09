using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Clients.Elasticsearch.Core.Search;
using MultiShop.Catalog.Application.Dtos.ProductDtos;
using MultiShop.Shared.Responses;
namespace MultiShop.Catalog.Application.Services.ProductSearch
{
    public class ProductSearchService : IProductSearchService
    {
        private readonly ElasticsearchClient _es;

        public ProductSearchService(ElasticsearchClient es)
        {
            _es = es;
        }
        public async Task<Result<PagedResult<ResultProductDto>>> SearchProductsAsync(
       string categoryId,
       int pageSize,
       double? lastPrice,
       string? lastId,
       string sortField,
       string sortOrder)
        {
            var request = new SearchRequest("products")
            {
                Size = pageSize,
                Sort = new List<SortOptions>
        {
            SortOptions.Field(sortField, new FieldSort
            {
                Order = sortOrder == "asc" ? SortOrder.Asc : SortOrder.Desc
            }),
            SortOptions.Field("productId.keyword", new FieldSort
            {
                Order = sortOrder == "asc" ? SortOrder.Asc : SortOrder.Desc
            })
        }
            };

            // 🔹 Filter
            if (!string.IsNullOrEmpty(categoryId))
            {
                request.Query = new BoolQuery
                {
                    Filter = new List<Query>
            {
                new TermQuery("categoryId.keyword")
                {
                    Value = categoryId
                }
            }
                };
            }

            // 🔹 SearchAfter Cursor
            // SearchAfter cursor
            if (lastPrice.HasValue && !string.IsNullOrEmpty(lastId))
            {
                request.SearchAfter = new List<FieldValue>
        {
            FieldValue.Double(lastPrice.Value),
            FieldValue.String(lastId)
        };
            }

            var response = await _es.SearchAsync<ResultProductDto>(request);

            var items = response.Documents.ToList();
            var lastHit = response.Hits.LastOrDefault();

            double? newLastPrice = null;
            string? newLastId = null;

            // 🔹 Extract Cursor Safely
            if (lastHit != null && lastHit.Sort != null && lastHit.Sort.Count >= 2)
            {
                var priceValue = lastHit.Sort.ElementAt(0);
                var idValue = lastHit.Sort.ElementAt(1);

                if (priceValue.TryGetDouble(out var d))
                    newLastPrice = d;
                else if (priceValue.TryGetLong(out var l))
                    newLastPrice = l;

                if (idValue.TryGetString(out var s))
                    newLastId = s;
            }

            return new PagedResult<ResultProductDto>
            {
                Items = items,
                LastPrice = newLastPrice,
                LastId = newLastId,
                SortField = sortField,
                SortOrder = sortOrder
            };
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
     
        //    public async Task<Result<List<ResultProductDto>>> SearchProductsAsync(string categoryId,
        //int pageNumber,
        //int pageSize,
        //string sortField,
        //string sortOrder)
        //    {
        //        var from = (pageNumber - 1) * pageSize;

        //        var response = await _es.SearchAsync<ResultProductDto>(s => s
        //            .Index("products")
        //            .From(from)
        //            .Size(pageSize)
        //            .Sort(so => so.Field(sortField, f => f.Order(sortOrder == "asc" ? SortOrder.Asc : SortOrder.Desc)))
        //            .Query(q => q
        //                .Bool(b => b
        //                    .Filter(f =>
        //                    {
        //                        // Eğer categoryId doluysa Term query ekle, boşsa boş dön (tümünü getir)
        //                        if (!string.IsNullOrEmpty(categoryId))
        //                        {
        //                            f.Term(t => t.Field("categoryId").Value(categoryId));
        //                        }
        //                    })
        //                )
        //            )
        //        );

        //        if (!response.IsValidResponse)
        //            throw new Exception(response.DebugInformation);

        //        return response.Documents.ToList();
        //    }

        //public async Task<Result<List<ProductSearchResultDto>>> SearchProductsAsync(string categoryId,
        //    int pageNumber,
        //    int pageSize,
        //    string sortField,
        //    string sortOrder)
        //{
        //    var from = (pageNumber - 1) * pageSize;

        //    var response = await _es.SearchAsync<ProductSearchResultDto>(s => s
        //    .Index("products")
        //    .From(from)
        //    .Size(pageSize)
        //    .Sort(so => so.Field(sortField, f => f.Order(sortOrder == "asc" ? SortOrder.Asc : SortOrder.Desc)))



        //);



        //    if (!response.IsValidResponse)
        //        throw new Exception(response.DebugInformation);

        //    return response.Documents.ToList();
        //}


    }
}
