

using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.OfferDiscountServices
{
    public class OfferDiscountService : IOfferDiscountService
    {
        private readonly HttpClient _httpClient;
        private readonly IUiNotifierService _uiNotifierService;

        public OfferDiscountService(HttpClient httpClient, IUiNotifierService uiNotifierService)
        {
            _httpClient = httpClient;
            _uiNotifierService = uiNotifierService;
        }

        public async Task CreateOfferDiscountAsync(CreateOfferDiscountDto createOfferDiscountDto)
        {
            await _httpClient.PostAsJsonAsync("OfferDiscounts", createOfferDiscountDto);
        }

        public async Task DeleteOfferDiscountAsync(string id)
        {
            await _httpClient.DeleteAsync("OfferDiscounts?id=" + id);
        }

        public async Task<UpdateOfferDiscountDto> GetByIdOfferDiscountAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("offerDiscounts/"+id);
            var contentValue = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<UpdateOfferDiscountDto>(contentValue);
            return value;
        }

        public async Task<List<ResultOfferDiscountDto>> OfferDiscountListAsync()
        {
            var responseMessage = await _httpClient.GetAsync("OfferDiscounts");


            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<List<ResultOfferDiscountDto>>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new List<ResultOfferDiscountDto>();

            // var contentValue=await responseMessage.Content.ReadAsStringAsync();
            // var values = JsonConvert.DeserializeObject<List<ResultOfferDiscountDto>>(contentValue);
            // return values;
        }

        public async Task UpdateOfferDiscountAsync(UpdateOfferDiscountDto updateOfferDiscountDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateOfferDiscountDto>("OfferDiscounts", updateOfferDiscountDto);
        }
    }
}
