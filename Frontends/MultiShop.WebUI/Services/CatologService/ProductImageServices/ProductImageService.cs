

using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Handlers;
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

        public async Task<string> CreateProductImageAsync(CreateProductImageDto createProductImageDto)
        {
            var _httpClient = _factory.Create("Catalog");
            var response=await _httpClient.PostAsJsonAsync<CreateProductImageDto>("ProductImages", createProductImageDto);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }

        public async Task<string> DeleteProductImageAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.DeleteAsync("ProductImages?Id="+id);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }

        public async Task<List<ResultProductImageDto>> GetAllProductImageAsync()
        {
            var _httpClient = _factory.Create("Catalog");

            var responseMessage = await _httpClient.GetAsync("ProductImages");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<List<ResultProductImageDto>>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new List<ResultProductImageDto>();
            //var contentValues=await responseMessage.Content.ReadAsStringAsync();
            //var values = JsonConvert.DeserializeObject<List<ResultProductImageDto>>(contentValues);
            // return values;
        }

        public async Task<UpdateProductImageDto> GetByIdProductImageAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var responseMessage = await _httpClient.GetAsync("ProductImages/"+id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<UpdateProductImageDto>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new UpdateProductImageDto();
            //var contentValues = await responseMessage.Content.ReadAsStringAsync();
            //var value = JsonConvert.DeserializeObject<UpdateProductImageDto>(contentValues);
            //return value;
        }

        public async Task<UpdateProductImageDto> GetByProductIdProductImageAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var responseMessage = await _httpClient.GetAsync("ProductImages/ProductImagesByProductId/" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<UpdateProductImageDto>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new UpdateProductImageDto();
            //var contentValues = await responseMessage.Content.ReadAsStringAsync();
            //var value = JsonConvert.DeserializeObject<UpdateProductImageDto>(contentValues);
            //return value;
        }

        public async Task<string> UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.PutAsJsonAsync<UpdateProductImageDto>("ProductImages", updateProductImageDto);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }
    }
}
