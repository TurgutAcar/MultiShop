

using System.Net;
using System.Net.Http.Json;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.GlobalException;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatologService.FeatureSliderServices
{
    public class FeatureSliderService : IFeatureSliderService
    {
        private readonly IApiClientFactory _factory;

        private readonly IUiNotifierService _uiNotifierService;
        public FeatureSliderService(IUiNotifierService uiNotifierService, IApiClientFactory factory)
        {
            _uiNotifierService = uiNotifierService;
            _factory = factory;
        }

        public async Task<Result<string>> CreateFeatureSliderAsync(CreateFeatureSliderDto featureSliderDto)
        {
            var _httpClient = _factory.Create("Catalog");
            var response =await _httpClient.PostAsJsonAsync<CreateFeatureSliderDto>("FeatureSliders", featureSliderDto);
            return await response.ReadSafeResultAsync<string>();

        }

        public async Task<Result<string>> DeleteFeatureSliderAsync(string featureSliderId)
        {
            var _httpClient = _factory.Create("Catalog");
            var response = await _httpClient.DeleteAsync("FeatureSliders?id="+featureSliderId);
            return await response.ReadSafeResultAsync<string>();

        }


        public async Task<Result<List<ResultFeatureSliderDto>>> GetAllFeatureSliderAsync()
        {
            var _httpClient = _factory.Create("Catalog");
            var response = await _httpClient.GetAsync("FeatureSliders");
            return await response.ReadSafeResultAsync<List<ResultFeatureSliderDto>>();
        }

        public async Task<Result<UpdateFeatureSliderDto>> GetByIdFeatureSliderAsync(string featureSliderId)
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("FeatureSliders/"+featureSliderId);
            return await response.ReadSafeResultAsync<UpdateFeatureSliderDto>();
        }

        public async Task<Result<string>> UpdateFeatureSliderAsync(UpdateFeatureSliderDto featureSliderDto)
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.PutAsJsonAsync<UpdateFeatureSliderDto>("FeatureSliders", featureSliderDto);
            return await response.ReadSafeResultAsync<string>();

        }
    }
}
