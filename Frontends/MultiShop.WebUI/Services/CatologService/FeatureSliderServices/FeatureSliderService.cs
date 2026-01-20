

using System.Net;
using System.Net.Http.Json;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.GlobalException;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatologService.FeatureSliderServices
{
    public class FeatureSliderService : IFeatureSliderService
    {
        private readonly HttpClient _httpClient;
        private readonly IUiNotifierService _uiNotifierService;
        public FeatureSliderService(HttpClient httpClient, IUiNotifierService uiNotifierService)
        {
            _httpClient = httpClient;
            _uiNotifierService = uiNotifierService;
        }

        public async Task CreateFeatureSliderAsync(CreateFeatureSliderDto featureSliderDto)
        {
            await _httpClient.PostAsJsonAsync<CreateFeatureSliderDto>("FeatureSliders", featureSliderDto);
        }

        public async Task DeleteFeatureSliderAsync(string featureSliderId)
        {
            await _httpClient.DeleteAsync("FeatureSliders?id="+featureSliderId);
        }

      
        public async Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync()
        {
            var response = await _httpClient.GetAsync("FeatureSliders");
          

           
            var contentValue=await response.Content.ReadAsStringAsync();

                   var values = JsonConvert.DeserializeObject<Result<List<ResultFeatureSliderDto>>>(contentValue);
                    return values.HandleUiResult(_uiNotifierService)
               ?? new List<ResultFeatureSliderDto>();
        }

        public async Task<UpdateFeatureSliderDto> GetByIdFeatureSliderAsync(string featureSliderId)
        {
            var responseMessage = await _httpClient.GetAsync("FeatureSliders/"+featureSliderId);
            var contentValue = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<UpdateFeatureSliderDto>(contentValue);
            return value;
        }

        public async Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto featureSliderDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateFeatureSliderDto>("FeatureSliders", featureSliderDto);
        }
    }
}
