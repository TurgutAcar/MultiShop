
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.BrandServices
{
    public class BrandService : IBrandService
    {
        private readonly HttpClient _httpClient;
        private readonly IUiNotifierService _uiNotifierService;

        public BrandService(HttpClient httpClient, IUiNotifierService uiNotifierService)
        {
            _httpClient = httpClient;
            _uiNotifierService = uiNotifierService;
        }

        public async Task<List<ResultBrandDto>> BrandListAsync()
        {
            var responseMessage = await _httpClient.GetAsync("Brands");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<List<ResultBrandDto>>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new List<ResultBrandDto>();
            //var contentValue=await responseMessage.Content.ReadAsStringAsync(); 
            //  var values=JsonConvert.DeserializeObject<List<ResultBrandDto>>(contentValue);
            //return values;
        }

        public async Task CreateBrandAsync(CreateBrandDto createBrandDto)
        {
            await _httpClient.PostAsJsonAsync<CreateBrandDto>("Brands",createBrandDto); 
        }

        public async Task DeleteBrandAsync(string id)
        {
            await _httpClient.DeleteAsync("Brands?id="+id);
        }

        public async Task<UpdateBrandDto> GetByIdBrandAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("Brands/"+id);
            var contentValue = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<UpdateBrandDto>(contentValue);
            return value;
        }

        public async Task UpdateBrandAsync(UpdateBrandDto updateBrandDto)
        {
            await _httpClient.PostAsJsonAsync<UpdateBrandDto>("Brands", updateBrandDto);
        }
    }
}
