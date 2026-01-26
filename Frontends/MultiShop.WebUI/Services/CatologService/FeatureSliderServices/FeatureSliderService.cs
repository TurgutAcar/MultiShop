

using System.Net;
using System.Net.Http.Json;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.GlobalException;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatologService.FeatureSliderServices
{
    public class FeatureSliderService : IFeatureSliderService
    {
        //private readonly HttpClient _httpClient;
        private readonly IApiClientFactory _factory;

        private readonly IUiNotifierService _uiNotifierService;
        public FeatureSliderService(IUiNotifierService uiNotifierService, IApiClientFactory factory)
        {
            // _httpClient = httpClient;
            _uiNotifierService = uiNotifierService;
            _factory = factory;
        }

        public async Task<string> CreateFeatureSliderAsync(CreateFeatureSliderDto featureSliderDto)
        {
            var _httpClient = _factory.Create("Catalog");
            var response =await _httpClient.PostAsJsonAsync<CreateFeatureSliderDto>("FeatureSliders", featureSliderDto);
            var contentValue = await response.Content.ReadAsStringAsync();

            var values = JsonConvert.DeserializeObject<Result<string>>(contentValue);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }

        public async Task<string> DeleteFeatureSliderAsync(string featureSliderId)
        {
            var _httpClient = _factory.Create("Catalog");
            var response = await _httpClient.DeleteAsync("FeatureSliders?id="+featureSliderId);
            var contentValue = await response.Content.ReadAsStringAsync();

            var values = JsonConvert.DeserializeObject<Result<string>>(contentValue);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }

      
        public async Task<Result<List<ResultFeatureSliderDto>>> GetAllFeatureSliderAsync()
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("FeatureSliders");

            return await response.ReadSafeResultAsync<List<ResultFeatureSliderDto>>();
            //var response = await _httpClient.GetAsync("FeatureSliders");

            //var contentValue=await response.Content.ReadAsStringAsync();

            //       var values = JsonConvert.DeserializeObject<Result<List<ResultFeatureSliderDto>>>(contentValue);
            //        return values.HandleUiResult(_uiNotifierService)
            //   ?? new List<ResultFeatureSliderDto>();
        }

        public async Task<UpdateFeatureSliderDto> GetByIdFeatureSliderAsync(string featureSliderId)
        {
            var _httpClient = _factory.Create("Catalog");

            var responseMessage = await _httpClient.GetAsync("FeatureSliders/"+featureSliderId);
            var contentValue = await responseMessage.Content.ReadAsStringAsync();

            var values = JsonConvert.DeserializeObject<Result<UpdateFeatureSliderDto>>(contentValue);
            return values.HandleUiResult(_uiNotifierService)
       ?? new UpdateFeatureSliderDto();
            //var contentValue = await responseMessage.Content.ReadAsStringAsync();
            //  var value = JsonConvert.DeserializeObject<UpdateFeatureSliderDto>(contentValue);
            //  return value;
        }

        public async Task<string> UpdateFeatureSliderAsync(UpdateFeatureSliderDto featureSliderDto)
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.PutAsJsonAsync<UpdateFeatureSliderDto>("FeatureSliders", featureSliderDto);
            var contentValue = await response.Content.ReadAsStringAsync();

            var values = JsonConvert.DeserializeObject<Result<string>>(contentValue);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }
    }
}
