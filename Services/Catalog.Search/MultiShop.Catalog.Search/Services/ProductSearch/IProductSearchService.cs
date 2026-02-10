

using MultiShop.Catalog.Search.Dtos.ProductDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.Catalog.Search.Services.ProductSearch
{
    public interface IProductSearchService
    {
        Task<Result<PagedResult<ResultProductDto>>> SearchProductsAsync(
   string categoryId,
   int pageSize,
   double? lastPrice,
   string? lastId,string sortField,string sortOrder);
        Task<Result<List<ProductSearchResultDto>>> SearchProductNameAsync(string query, int pageNumber = 1, int pageSize = 10);

    }
}
