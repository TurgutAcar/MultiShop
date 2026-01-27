
using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatologService.SpecialOfferServices
{
    public class SpecialOfferService : ISpecialOfferService
    {
        private readonly IApiClientFactory _factory;
        private readonly IUiNotifierService _uiNotifierService;

        public SpecialOfferService(IUiNotifierService uiNotifierService, IApiClientFactory factory)
        {
            _uiNotifierService = uiNotifierService;
            _factory = factory;
        }

        public async Task<Result<string>> CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto)
        {
            var _httpClient = _factory.Create("Catalog");
            var response =await _httpClient.PostAsJsonAsync<CreateSpecialOfferDto>("SpecialOffers", createSpecialOfferDto);
            return await response.ReadSafeResultAsync<string>();
        }

        public async Task<Result<string>> DeleteSpecialOfferAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");
            var response = await _httpClient.DeleteAsync("SpecialOffers?id="+id);
            return await response.ReadSafeResultAsync<string>();

        }

        public async Task<Result<List<ResultSpecialOfferDto>>> GetAllSpecialOfferAsync()
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("SpecialOffers");
            return await response.ReadSafeResultAsync<List<ResultSpecialOfferDto>>();
        }

        public async Task<Result<UpdateSpecialOfferDto>> GetByIdSpecialOfferAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("SpecialOffers/"+id);
            return await response.ReadSafeResultAsync<UpdateSpecialOfferDto>();
        }

        public async Task<Result<string>> UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.PutAsJsonAsync<UpdateSpecialOfferDto>("SpecialOffers", updateSpecialOfferDto);
            return await response.ReadSafeResultAsync<string>();

        }
    }
}
