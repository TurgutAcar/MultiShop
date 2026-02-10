using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MultiShop.WebUI.Services.CatologService.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IApiClientFactory _factory;
        private readonly IUiNotifierService _uiNotifierService;
        public ProductService(IUiNotifierService uiNotifierService, IApiClientFactory factory)
        {
            _uiNotifierService = uiNotifierService;
            _factory = factory;
        }

        public async Task<Result<string>> CreateProductAsync(CreateProductDto createProductDto)
        {
            var _httpClient = _factory.Create("Catalog");
            var response = await _httpClient.PostAsJsonAsync<CreateProductDto>("products", createProductDto);
            return await response.ReadSafeResultAsync<string>();

        }

        public async Task<Result<string>> DeleteProductAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.DeleteAsync("products?id=" + id);
            return await response.ReadSafeResultAsync<string>();
        }

        public async Task<Result<List<ResultProductDto>>> GetAllProductAsync()
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("products");
            return await response.ReadSafeResultAsync<List<ResultProductDto>>();

        }

        public async Task<Result<UpdateProductDto>> GetByIdProductAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("products/" + id);
            return await response.ReadSafeResultAsync<UpdateProductDto>();

        }

        public async Task<Result<List<ResultProductsWithCategoryDto>>> GetProductsWithCategoryAsync()
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("Products/ProductListWithCategory");
            return await response.ReadSafeResultAsync<List<ResultProductsWithCategoryDto>> ();

        }

        public async Task<Result<List<ResultProductsWithCategoryDto>>> GetProductsWithCategoryByCategoryIdAsync(string CategoryId)
        {
            var _httpClient = _factory.Create("Catalog");
            var response = await _httpClient.GetAsync("products/ProductListWithCategoryByCategoryId/" + CategoryId);
            return await response.ReadSafeResultAsync<List<ResultProductsWithCategoryDto>>();

        }

        public async Task<Result<string>> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.PutAsJsonAsync<UpdateProductDto>("products", updateProductDto);
            return await response.ReadSafeResultAsync<string>();

        }
    }
}
