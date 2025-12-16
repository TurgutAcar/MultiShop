using MultiShop.Catalog.Application.Dtos.ProductDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.Catalog.Application.Services.ProductSearch
{
    public interface IProductSearchService
    {
        Task<Result<List<ProductSearchResultDto>>> SearchProductsAsync(
        int pageNumber,
        int pageSize,
        string sortField,
        string sortOrder);
        Task<Result<List<ProductSearchResultDto>>> SearchProductNameAsync(string query, int pageNumber = 1, int pageSize = 10);

    }
}
