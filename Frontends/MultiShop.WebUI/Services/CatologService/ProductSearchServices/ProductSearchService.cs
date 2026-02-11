using Microsoft.AspNetCore.WebUtilities;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using System.Globalization;

namespace MultiShop.WebUI.Services.CatologService.ProductSearchServices
{
    public class ProductSearchService : IProductSearchService
    {
        private readonly IApiClientFactory _factory;
        public ProductSearchService(IApiClientFactory factory)
        {
            _factory = factory;
        }
        public async Task<Result<PagedResult<ResultProductDto>>> GetPagedProductsByCategoryIdAsync(
    string categoryId,
    double? lastPrice = null,
    string? lastId = null,
    int pageSize = 10,
    string sortField = "productPrice",
    string sortOrder = "asc")
        {
            var client = _factory.Create("CatalogSearch");

            var query = new Dictionary<string, string?>
            {
                ["categoryId"] = categoryId,
                ["pageSize"] = pageSize.ToString(),
                ["sortField"] = sortField,
                ["sortOrder"] = sortOrder
            };

            if (lastPrice.HasValue)
                query["lastPrice"] = lastPrice.Value.ToString(CultureInfo.InvariantCulture);

            if (!string.IsNullOrEmpty(lastId))
                query["lastId"] = lastId;

            var url = QueryHelpers.AddQueryString("ProductSearchs/sort", query);

            var response = await client.GetAsync(url);
            return await response.ReadSafeResultAsync<PagedResult<ResultProductDto>>();
        }
        //public async Task<Result<PagedResult<ResultProductDto>>> GetPagedProductsByCategoryIdAsync(string categoryId, double? lastPrice = null, string? lastId = null, int pageSize = 10, string sortField = "productPrice", string sortOrder = "asc")
        //{
        //    var _httpClient = _factory.Create("CatalogSearch");
        //    var response = await _httpClient.GetAsync($"ProductSearchs/sort?categoryId={categoryId}&pageSize={pageSize}&lastPrice={lastPrice}&&lastId={lastId}&sortField={sortField}&sortOrder={sortOrder}");
        //    return await response.ReadSafeResultAsync<PagedResult<ResultProductDto>>();
        //}
    }
}
