

using System.Net.Http.Json;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatologService.FeatureSliderServices
{
    public class FeatureSliderService : IFeatureSliderService
    {
        private readonly HttpClient _httpClient;

        public FeatureSliderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
            var responseMessage =await _httpClient.GetAsync("FeatureSliders");
            var contentValue=await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultFeatureSliderDto>>(contentValue);
            return values;
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
