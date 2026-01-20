

using System.Net.Http.Json;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.FeatureService
{
    public class FeatureService : IFeatureService
    {
        private readonly HttpClient _httpClient;

        public FeatureService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateFeatureAsync(CreateFeatureDto feature)
        {
            await _httpClient.PostAsJsonAsync<CreateFeatureDto>("features", feature);
        }

        public async Task DeleteFeatureAsync(string featureId)
        {
            await _httpClient.DeleteAsync("features?id="+ featureId);
        }

        public async Task<List<ResultFeatureDto>> FeatureListAsync()
        {
            var responseMessage = await _httpClient.GetAsync("features");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return new List<ResultFeatureDto>();
            }
            var contentValue=await responseMessage.Content.ReadAsStringAsync();
            var values=JsonConvert.DeserializeObject<List<ResultFeatureDto>>(contentValue);
            return values;
        }

        public async Task<UpdateFeatureDto> GetByIdFeatureAsync(string featureId)
        {
            var responseMessage = await _httpClient.GetAsync("features/"+featureId);
            var contentValue = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<UpdateFeatureDto>(contentValue);
            return value;
        }

        public async Task UpdateFeatureAsync(UpdateFeatureDto feature)
        {
            await _httpClient.PutAsJsonAsync<UpdateFeatureDto>("features", feature);
        }
    }
}
