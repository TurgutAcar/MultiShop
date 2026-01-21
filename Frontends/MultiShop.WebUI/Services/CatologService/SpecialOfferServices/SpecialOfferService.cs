
using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatologService.SpecialOfferServices
{
    public class SpecialOfferService : ISpecialOfferService
    {
        private readonly HttpClient _httpClient;
        private readonly IUiNotifierService _uiNotifierService;

        public SpecialOfferService(HttpClient httpClient, IUiNotifierService uiNotifierService)
        {
            _httpClient = httpClient;
            _uiNotifierService = uiNotifierService;
        }

        public async Task CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto)
        {
            await _httpClient.PostAsJsonAsync<CreateSpecialOfferDto>("SpecialOffers", createSpecialOfferDto);
        }

        public async Task DeleteSpecialOfferAsync(string id)
        {
            await _httpClient.DeleteAsync("SpecialOffers?id="+id);

        }

        public async Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync()
        {
            var responseMessage=await _httpClient.GetAsync("SpecialOffers");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<List<ResultSpecialOfferDto>>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new List<ResultSpecialOfferDto>();
            // var content=await responseMessage.Content.ReadAsStringAsync();
            // var values=JsonConvert.DeserializeObject<List<ResultSpecialOfferDto>>(content);
            // return values;
        }

        public async Task<UpdateSpecialOfferDto> GetByIdSpecialOfferAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("SpecialOffers/"+id);
            var content = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateSpecialOfferDto>(content);
            return values;
        }

        public async Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateSpecialOfferDto>("SpecialOffers", updateSpecialOfferDto);
        }
    }
}
