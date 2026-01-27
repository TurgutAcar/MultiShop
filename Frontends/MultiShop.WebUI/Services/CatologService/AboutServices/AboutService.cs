

using System.Net.Http.Json;
using MultiShop.DtoLayer.CargoDtos.CargoCustomerDtos;
using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
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

        public async Task<Result<List<ResultAboutDto>>> AboutListAsync()
        {
            var _httpClient = _factory.Create("Catalog");
            var response = await _httpClient.GetAsync("Abouts");
            return await response.ReadSafeResultAsync<List<ResultAboutDto>>();

        }

        public async Task<Result<string>> CreateAboutAsync(CreateAboutDto createAboutDto)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.PostAsJsonAsync<CreateAboutDto>("Abouts",createAboutDto);
            return await response.ReadSafeResultAsync<string>();
        }

        public async Task<Result<string>> DeleteAboutAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =  await _httpClient.DeleteAsync("Abouts?id="+id);
            return await response.ReadSafeResultAsync<string>();
        }

        public async Task<Result<UpdateAboutDto>> GetByIdAboutAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("Abouts/"+id);
            return await response.ReadSafeResultAsync<UpdateAboutDto>();

        }

        public async Task<Result<string>> UpdateAboutAsync(UpdateAboutDto updateAboutDto)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.PostAsJsonAsync<UpdateAboutDto>("Abouts", updateAboutDto);
            return await response.ReadSafeResultAsync<string>();
        }
    }
}
