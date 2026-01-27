

using MultiShop.DtoLayer.CargoDtos.CargoCustomerDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.ProductDetailServices
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly IApiClientFactory _factory;
        private readonly IUiNotifierService _uiNotifierService;

        public ProductDetailService(IApiClientFactory factory, IUiNotifierService uiNotifierService)
        {
            _factory = factory;
            _uiNotifierService = uiNotifierService;
        }

        public async Task<Result<string>> CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto)
        {
            var _httpClient = _factory.Create("Catalog");
            var response =await _httpClient.PostAsJsonAsync<CreateProductDetailDto>("ProductDetails",createProductDetailDto);
            return await response.ReadSafeResultAsync<string>();

        }

        public async Task<Result<string>> DeleteProductDetailAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.DeleteAsync("ProductDetails?id="+id);
            return await response.ReadSafeResultAsync<string>();

        }

        public async Task<Result<List<ResultProductDetailDto>>> GetAllProductDetailAsync()
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("ProductDetails");
            return await response.ReadSafeResultAsync<List<ResultProductDetailDto>>();

        }

        public async Task<Result<UpdateProductDetailDto>> GetByIdProductDetailAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("ProductDetails/"+id);
            return await response.ReadSafeResultAsync<UpdateProductDetailDto>();

        }

        public async Task<Result<UpdateProductDetailDto>> GetByProductIdProductDetailAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("ProductDetails/GetProductDetailByProductId/"+id);
            return await response.ReadSafeResultAsync<UpdateProductDetailDto>();

        }

        public async Task<Result<string>> UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.PutAsJsonAsync<UpdateProductDetailDto>("ProductDetails", updateProductDetailDto);
            return await response.ReadSafeResultAsync<string>();

        }
    }
}
