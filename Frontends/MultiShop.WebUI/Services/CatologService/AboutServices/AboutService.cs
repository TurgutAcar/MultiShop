

using System.Net.Http.Json;
using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.AboutServices
{
    public class AboutService : IAboutService
    {
        private readonly IUiNotifierService _uiNotifierService;
        private readonly IApiClientFactory _factory;


        public AboutService(IUiNotifierService uiNotifierService, IApiClientFactory factory)
        {
            _uiNotifierService = uiNotifierService;
            _factory = factory;
        }

        public async Task<List<ResultAboutDto>> AboutListAsync()
        {
            var _httpClient = _factory.Create("Catalog");
            var responseMessage =await _httpClient.GetAsync("Abouts");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<List<ResultAboutDto>>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new List<ResultAboutDto>();
            // var contentValues=await responseMessage.Content.ReadAsStringAsync();
            // var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(contentValues);
            // return values;

        }

        public async Task<string> CreateAboutAsync(CreateAboutDto createAboutDto)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.PostAsJsonAsync<CreateAboutDto>("Abouts",createAboutDto);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }

        public async Task<string> DeleteAboutAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =  await _httpClient.DeleteAsync("Abouts?id="+id);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }

        public async Task<UpdateAboutDto> GetByIdAboutAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var responseMessage = await _httpClient.GetAsync("Abouts/"+id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<UpdateAboutDto>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new UpdateAboutDto();
            // var contentValues = await responseMessage.Content.ReadAsStringAsync();
            //var value = JsonConvert.DeserializeObject<UpdateAboutDto>(contentValues);
            // return value;
        }

        public async Task<string> UpdateAboutAsync(UpdateAboutDto updateAboutDto)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.PostAsJsonAsync<UpdateAboutDto>("Abouts", updateAboutDto);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService);
        }
    }
}
