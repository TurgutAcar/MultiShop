

using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.OfferDiscountServices
{
    public class OfferDiscountService : IOfferDiscountService
    {
        //private readonly HttpClient _httpClient;
        private readonly IApiClientFactory _factory;

        private readonly IUiNotifierService _uiNotifierService;

        public OfferDiscountService(IUiNotifierService uiNotifierService, IApiClientFactory factory)
        {
           // _httpClient = httpClient;
            _uiNotifierService = uiNotifierService;
            _factory = factory;
        }

        public async Task<string> CreateOfferDiscountAsync(CreateOfferDiscountDto createOfferDiscountDto)
        {
            var _httpClient = _factory.Create("Catalog");
            var response =await _httpClient.PostAsJsonAsync("OfferDiscounts", createOfferDiscountDto);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }

        public async Task<string> DeleteOfferDiscountAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");
            var response = await _httpClient.DeleteAsync("OfferDiscounts?id=" + id);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }

        public async Task<UpdateOfferDiscountDto> GetByIdOfferDiscountAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");
            var responseMessage = await _httpClient.GetAsync("offerDiscounts/"+id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<UpdateOfferDiscountDto>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new UpdateOfferDiscountDto();
            // var contentValue = await responseMessage.Content.ReadAsStringAsync();
            // var value = JsonConvert.DeserializeObject<UpdateOfferDiscountDto>(contentValue);
            //  return value;
        }

        public async Task<Result<List<ResultOfferDiscountDto>>> OfferDiscountListAsync()
        {
            var _httpClient = _factory.Create("Catalog");
            var response = await _httpClient.GetAsync("OfferDiscounts");
            return await response.ReadSafeResultAsync<List<ResultOfferDiscountDto>>();


       //     var jsonData = await responseMessage.Content.ReadAsStringAsync();
       //     var values = JsonConvert.DeserializeObject<Result<List<ResultOfferDiscountDto>>>(jsonData);
       //     return values.HandleUiResult(_uiNotifierService)
       //?? new List<ResultOfferDiscountDto>();

            // var contentValue=await responseMessage.Content.ReadAsStringAsync();
            // var values = JsonConvert.DeserializeObject<List<ResultOfferDiscountDto>>(contentValue);
            // return values;
        }

        public async Task<string> UpdateOfferDiscountAsync(UpdateOfferDiscountDto updateOfferDiscountDto)
        {
            var _httpClient = _factory.Create("Catalog");
            var response =await _httpClient.PutAsJsonAsync<UpdateOfferDiscountDto>("OfferDiscounts", updateOfferDiscountDto);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }   
    }
}
