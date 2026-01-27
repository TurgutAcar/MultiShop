

using MultiShop.DtoLayer.CargoDtos.CargoCustomerDtos;
using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.OfferDiscountServices
{
    public class OfferDiscountService : IOfferDiscountService
    {
        private readonly IApiClientFactory _factory;

        private readonly IUiNotifierService _uiNotifierService;

        public OfferDiscountService(IUiNotifierService uiNotifierService, IApiClientFactory factory)
        {
            _uiNotifierService = uiNotifierService;
            _factory = factory;
        }

        public async Task<Result<string>> CreateOfferDiscountAsync(CreateOfferDiscountDto createOfferDiscountDto)
        {
            var _httpClient = _factory.Create("Catalog");
            var response =await _httpClient.PostAsJsonAsync("OfferDiscounts", createOfferDiscountDto);
            return await response.ReadSafeResultAsync<string>();

        }

        public async Task<Result<string>> DeleteOfferDiscountAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");
            var response = await _httpClient.DeleteAsync("OfferDiscounts?id=" + id);
            return await response.ReadSafeResultAsync<string>();

        }

        public async Task<Result<UpdateOfferDiscountDto>> GetByIdOfferDiscountAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");
            var response = await _httpClient.GetAsync("offerDiscounts/"+id);
            return await response.ReadSafeResultAsync<UpdateOfferDiscountDto>();

        }

        public async Task<Result<List<ResultOfferDiscountDto>>> OfferDiscountListAsync()
        {
            var _httpClient = _factory.Create("Catalog");
            var response = await _httpClient.GetAsync("OfferDiscounts");
            return await response.ReadSafeResultAsync<List<ResultOfferDiscountDto>>();

        }

        public async Task<Result<string>> UpdateOfferDiscountAsync(UpdateOfferDiscountDto updateOfferDiscountDto)
        {
            var _httpClient = _factory.Create("Catalog");
            var response =await _httpClient.PutAsJsonAsync<UpdateOfferDiscountDto>("OfferDiscounts", updateOfferDiscountDto);
            return await response.ReadSafeResultAsync<string>();

        }
    }
}
