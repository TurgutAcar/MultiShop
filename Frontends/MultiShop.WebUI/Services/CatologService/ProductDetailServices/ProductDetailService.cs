

using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Handlers;
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

        public async Task<string> CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto)
        {
            var _httpClient = _factory.Create("Catalog");
            var response =await _httpClient.PostAsJsonAsync<CreateProductDetailDto>("ProductDetails",createProductDetailDto);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }

        public async Task<string> DeleteProductDetailAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.DeleteAsync("ProductDetails?id="+id);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }

        public async Task<List<ResultProductDetailDto>> GetAllProductDetailAsync()
        {
            var _httpClient = _factory.Create("Catalog");

            var responseMessage =await _httpClient.GetAsync("ProductDetails");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<List<ResultProductDetailDto>>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ??new List<ResultProductDetailDto>();
            //var contentValues=await responseMessage.Content.ReadAsStringAsync();
            //var values=JsonConvert.DeserializeObject<List<ResultProductDetailDto>>(contentValues);
            //return values;

        }

        public async Task<UpdateProductDetailDto> GetByIdProductDetailAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var responseMessage = await _httpClient.GetAsync("ProductDetails/"+id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<UpdateProductDetailDto>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new UpdateProductDetailDto();
            // var contentValues = await responseMessage.Content.ReadAsStringAsync();
            // var value = JsonConvert.DeserializeObject<UpdateProductDetailDto>(contentValues);
            // return value;
        }

        public async Task<UpdateProductDetailDto> GetByProductIdProductDetailAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var responseMessage = await _httpClient.GetAsync("ProductDetails/GetProductDetailByProductId/"+id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<UpdateProductDetailDto>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new UpdateProductDetailDto();
            //var contentValues = await responseMessage.Content.ReadAsStringAsync();
            //var value = JsonConvert.DeserializeObject<UpdateProductDetailDto>(contentValues);
            //return value;
        }

        public async Task<string> UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.PutAsJsonAsync<UpdateProductDetailDto>("ProductDetails", updateProductDetailDto);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }
    }
}
