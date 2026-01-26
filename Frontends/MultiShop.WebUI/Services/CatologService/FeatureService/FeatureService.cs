

using System.Net.Http.Json;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.FeatureService
{
    public class FeatureService : IFeatureService
    {
        // private readonly HttpClient _httpClient;
        private readonly IApiClientFactory _factory;

        private readonly IUiNotifierService _uiNotifierService;

        public FeatureService(IUiNotifierService uiNotifierService, IApiClientFactory factory)
        {
           // _httpClient = httpClient;
            _uiNotifierService = uiNotifierService;
            _factory = factory;
        }

        public async Task<string> CreateFeatureAsync(CreateFeatureDto feature)
        {
            var _httpClient = _factory.Create("Catalog");
            var response =await _httpClient.PostAsJsonAsync<CreateFeatureDto>("features", feature);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";

        }

        public async Task<string> DeleteFeatureAsync(string featureId)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.DeleteAsync("features?id="+ featureId);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }

        public async Task<Result<List<ResultFeatureDto>>> FeatureListAsync()
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("features");


            return await response.ReadSafeResultAsync<List<ResultFeatureDto>>();




            //var contentValue=await responseMessage.Content.ReadAsStringAsync();
            //var values=JsonConvert.DeserializeObject<List<ResultFeatureDto>>(contentValue);
            //return values;
        }

        public async Task<UpdateFeatureDto> GetByIdFeatureAsync(string featureId)
        {
            var _httpClient = _factory.Create("Catalog");

            var responseMessage = await _httpClient.GetAsync("features/"+featureId);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<UpdateFeatureDto>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new UpdateFeatureDto();
            // var contentValue = await responseMessage.Content.ReadAsStringAsync();
            // var value = JsonConvert.DeserializeObject<UpdateFeatureDto>(contentValue);
            // return value;
        }

        public async Task<string> UpdateFeatureAsync(UpdateFeatureDto feature)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.PutAsJsonAsync<UpdateFeatureDto>("features", feature);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }
    }
}
