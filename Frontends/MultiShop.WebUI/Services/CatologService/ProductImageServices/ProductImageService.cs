

using MultiShop.DtoLayer.CargoDtos.CargoCustomerDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.ProductImageServices
{
    public class ProductImageService : IProductImageService
    {
        private readonly IUiNotifierService _uiNotifierService;
        private readonly IApiClientFactory _factory;

        public ProductImageService( IUiNotifierService uiNotifierService, IApiClientFactory factory)
        {
            _uiNotifierService = uiNotifierService;
            _factory = factory;
        }

        public async Task<Result<string>> CreateProductImageAsync(CreateProductImageDto createProductImageDto)
        {
            var _httpClient = _factory.Create("Catalog");
            var response=await _httpClient.PostAsJsonAsync<CreateProductImageDto>("ProductImages", createProductImageDto);
            return await response.ReadSafeResultAsync<string>();

        }

        public async Task<Result<string>> DeleteProductImageAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.DeleteAsync("ProductImages?Id="+id);
            return await response.ReadSafeResultAsync<string>();

        }

        public async Task<Result<List<ResultProductImageDto>>> GetAllProductImageAsync()
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("ProductImages");
            return await response.ReadSafeResultAsync<List<ResultProductImageDto>>();

        }

        public async Task<Result<UpdateProductImageDto>> GetByIdProductImageAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("ProductImages/"+id);
            return await response.ReadSafeResultAsync<UpdateProductImageDto>();

        }

        public async Task<Result<UpdateProductImageDto>> GetByProductIdProductImageAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("ProductImages/ProductImagesByProductId/" + id);
            return await response.ReadSafeResultAsync<UpdateProductImageDto>();

        }

        public async Task<Result<string>> UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.PutAsJsonAsync<UpdateProductImageDto>("ProductImages", updateProductImageDto);
            return await response.ReadSafeResultAsync<string>();

        }
    }
}
