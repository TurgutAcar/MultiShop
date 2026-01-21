

using System.Net.Http.Json;
using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.AboutServices
{
    public class AboutService : IAboutService
    {
        private readonly HttpClient _httpClient;
        private readonly IUiNotifierService _uiNotifierService;

        public AboutService(HttpClient httpClient, IUiNotifierService uiNotifierService)
        {
            _httpClient = httpClient;
            _uiNotifierService = uiNotifierService;
        }

        public async Task<List<ResultAboutDto>> AboutListAsync()
        {
            var responseMessage =await _httpClient.GetAsync("Abouts");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<List<ResultAboutDto>>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new List<ResultAboutDto>();
            // var contentValues=await responseMessage.Content.ReadAsStringAsync();
            // var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(contentValues);
            // return values;

        }

        public async Task CreateAboutAsync(CreateAboutDto createAboutDto)
        {
            await _httpClient.PostAsJsonAsync<CreateAboutDto>("Abouts",createAboutDto);
        }

        public async Task DeleteAboutAsync(string id)
        {
            await _httpClient.DeleteAsync("Abouts?id="+id);
        }

        public async Task<UpdateAboutDto> GetByIdAboutAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("Abouts/"+id);
            var contentValues = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<UpdateAboutDto>(contentValues);
            return value;
        }

        public async Task UpdateAboutAsync(UpdateAboutDto updateAboutDto)
        {
            await _httpClient.PostAsJsonAsync<UpdateAboutDto>("Abouts", updateAboutDto);
        }
    }
}
