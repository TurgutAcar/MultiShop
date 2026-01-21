using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatologService.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IUiNotifierService _uiNotifierService;
        public ProductService(HttpClient httpClient, IUiNotifierService uiNotifierService)
        {
            _httpClient = httpClient;
            _uiNotifierService = uiNotifierService;
        }

        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            await _httpClient.PostAsJsonAsync<CreateProductDto>("products", createProductDto);

        }

        public async Task DeleteProductAsync(string id)
        {
            await _httpClient.DeleteAsync("products?id=" + id);

        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var responseMessage = await _httpClient.GetAsync("products");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<List<ResultProductDto>>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new List<ResultProductDto>();

            //var jsonData = await responseMessage.Content.ReadAsStringAsync();
            //var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
            // return values;
        }

        public async Task<UpdateProductDto> GetByIdProductAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("products/" + id);
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateProductDto>();
            return values;
        }

        public async Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryAsync()
        {
            var responseMessage = await _httpClient.GetAsync("Products/ProductListWithCategory");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductsWithCategoryDto>>();
            return values;
        }

        public async Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryByCategoryIdAsync(string CategoryId)
        {
            var responseMessage = await _httpClient.GetAsync("products/ProductListWithCategoryByCategoryId/" + CategoryId);
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductsWithCategoryDto>>();
            return values;
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateProductDto>("products", updateProductDto);
        }
    }
}
