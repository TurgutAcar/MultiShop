using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.CatologService.ProductSearchServices
{
    public interface IProductSearchService
    {
        Task<Result<PagedResult<ResultProductDto>>> GetPagedProductsByCategoryIdAsync(string categoryId, double? lastPrice = null,string? lastId = null, int pageSize = 10, string sortField = "productPrice", string sortOrder = "asc");

    }
}
