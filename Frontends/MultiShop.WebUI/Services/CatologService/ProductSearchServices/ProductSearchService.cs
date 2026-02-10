using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;

namespace MultiShop.WebUI.Services.CatologService.ProductSearchServices
{
    public class ProductSearchService : IProductSearchService
    {
        private readonly IApiClientFactory _factory;
        public ProductSearchService(IApiClientFactory factory)
        {
            _factory = factory;
        }
        public async Task<Result<PagedResult<ResultProductDto>>> GetPagedProductsByCategoryIdAsync(string categoryId, double? lastPrice = null, string? lastId = null, int pageSize = 10, string sortField = "productPrice", string sortOrder = "asc")
        {
            var _httpClient = _factory.Create("CatalogSearch");
            var response = await _httpClient.GetAsync($"ProductSearchs/sort?categoryId={categoryId}&pageSize={pageSize}&lastPrice={lastPrice}&&lastId={lastId}&sortField={sortField}&sortOrder={sortOrder}");
            return await response.ReadSafeResultAsync<PagedResult<ResultProductDto>>();
        }
    }
}
