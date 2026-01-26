
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.BrandServices
{
    public class BrandService : IBrandService
    {
        private readonly IApiClientFactory _factory;

        //private readonly HttpClient _httpClient;
        private readonly IUiNotifierService _uiNotifierService;

        public BrandService(IUiNotifierService uiNotifierService, IApiClientFactory factory)
        {
            //_httpClient = httpClient;
            _uiNotifierService = uiNotifierService;
            _factory = factory;
        }

        public async Task<Result<List<ResultBrandDto>>> BrandListAsync()
        {
            var _httpClient = _factory.Create("Catalog");
            var response = await _httpClient.GetAsync("Brands");
            return await response.ReadSafeResultAsync<List<ResultBrandDto>>();

            //     var jsonData = await responseMessage.Content.ReadAsStringAsync();
            //     var values = JsonConvert.DeserializeObject<Result<List<ResultBrandDto>>>(jsonData);
            //     return values.HandleUiResult(_uiNotifierService)
            //?? new List<ResultBrandDto>();
            //var contentValue=await responseMessage.Content.ReadAsStringAsync(); 
            //  var values=JsonConvert.DeserializeObject<List<ResultBrandDto>>(contentValue);
            //return values;
        }

        public async Task<string> CreateBrandAsync(CreateBrandDto createBrandDto)
        {
            var _httpClient = _factory.Create("Catalog");
            var response=await _httpClient.PostAsJsonAsync<CreateBrandDto>("Brands",createBrandDto);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ??"";
        }

        public async Task<string> DeleteBrandAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");
            var response =await _httpClient.DeleteAsync("Brands?id="+id);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }

        public async Task<UpdateBrandDto> GetByIdBrandAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");
            var responseMessage = await _httpClient.GetAsync("Brands/"+id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<UpdateBrandDto>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new UpdateBrandDto();
            // var contentValue = await responseMessage.Content.ReadAsStringAsync();
            //  var value = JsonConvert.DeserializeObject<UpdateBrandDto>(contentValue);
            // return value;
        }

        public async Task<string> UpdateBrandAsync(UpdateBrandDto updateBrandDto)
        {
            var _httpClient = _factory.Create("Catalog");
            var response = await _httpClient.PostAsJsonAsync<UpdateBrandDto>("Brands", updateBrandDto);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }
    }
}
