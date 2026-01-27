

using System.Net.Http.Json;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.FeatureService
{
    public class FeatureService : IFeatureService
    {
        private readonly IApiClientFactory _factory;

        private readonly IUiNotifierService _uiNotifierService;

        public FeatureService(IUiNotifierService uiNotifierService, IApiClientFactory factory)
        {
            _uiNotifierService = uiNotifierService;
            _factory = factory;
        }

        public async Task<Result<string>> CreateFeatureAsync(CreateFeatureDto feature)
        {
            var _httpClient = _factory.Create("Catalog");
            var response =await _httpClient.PostAsJsonAsync<CreateFeatureDto>("features", feature);
            return await response.ReadSafeResultAsync<string>();


        }

        public async Task<Result<string>> DeleteFeatureAsync(string featureId)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.DeleteAsync("features?id="+ featureId);
            return await response.ReadSafeResultAsync<string>();

        }

        public async Task<Result<List<ResultFeatureDto>>> FeatureListAsync()
        {
            var _httpClient = _factory.Create("Catalog");
            var response = await _httpClient.GetAsync("features");
            return await response.ReadSafeResultAsync<List<ResultFeatureDto>>();
        }

        public async Task<Result<UpdateFeatureDto>> GetByIdFeatureAsync(string featureId)
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("features/"+featureId);
            return await response.ReadSafeResultAsync<UpdateFeatureDto>();

        }

        public async Task<Result<string>> UpdateFeatureAsync(UpdateFeatureDto feature)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.PutAsJsonAsync<UpdateFeatureDto>("features", feature);
            return await response.ReadSafeResultAsync<string>();

        }
    }
}
