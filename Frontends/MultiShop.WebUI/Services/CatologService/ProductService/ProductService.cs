using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatologService.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IApiClientFactory _factory;
        //private readonly HttpClient _httpClient;
        private readonly IUiNotifierService _uiNotifierService;
        public ProductService(IUiNotifierService uiNotifierService, IApiClientFactory factory)
        {
           // _httpClient = httpClient;
            _uiNotifierService = uiNotifierService;
            _factory = factory;
        }

        public async Task<string> CreateProductAsync(CreateProductDto createProductDto)
        {
            var _httpClient = _factory.Create("Catalog");
            var response = await _httpClient.PostAsJsonAsync<CreateProductDto>("products", createProductDto);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }

        public async Task<string> DeleteProductAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.DeleteAsync("products?id=" + id);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";

        }

        public async Task<Result<List<ResultProductDto>>> GetAllProductAsync()
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("products");
            return await response.ReadSafeResultAsync<List<ResultProductDto>>();


            //var jsonData = await responseMessage.Content.ReadAsStringAsync();
            //var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
            // return values;
        }

        public async Task<UpdateProductDto> GetByIdProductAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var responseMessage = await _httpClient.GetAsync("products/" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<UpdateProductDto>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new UpdateProductDto();
            // var values = await responseMessage.Content.ReadFromJsonAsync<UpdateProductDto>();
            //return values;
        }

        public async Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryAsync()
        {
            var _httpClient = _factory.Create("Catalog");

            var responseMessage = await _httpClient.GetAsync("Products/ProductListWithCategory");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<List<ResultProductsWithCategoryDto>>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new List<ResultProductsWithCategoryDto>();
            //var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductsWithCategoryDto>>();
            //return values;
        }

        public async Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryByCategoryIdAsync(string CategoryId)
        {
            var _httpClient = _factory.Create("Catalog");

            var responseMessage = await _httpClient.GetAsync("products/ProductListWithCategoryByCategoryId/" + CategoryId);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<List<ResultProductsWithCategoryDto>>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new List<ResultProductsWithCategoryDto>();
            //var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductsWithCategoryDto>>();
            //return values;
        }

        public async Task<string> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.PutAsJsonAsync<UpdateProductDto>("products", updateProductDto);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }
    }
}
