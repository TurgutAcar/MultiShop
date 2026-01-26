
using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatologService.SpecialOfferServices
{
    public class SpecialOfferService : ISpecialOfferService
    {
        private readonly IApiClientFactory _factory;

        // private readonly HttpClient _httpClient;
        private readonly IUiNotifierService _uiNotifierService;

        public SpecialOfferService(IUiNotifierService uiNotifierService, IApiClientFactory factory)
        {
            _uiNotifierService = uiNotifierService;
            _factory = factory;
        }

        public async Task<string> CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto)
        {
            var _httpClient = _factory.Create("Catalog");
            var response =await _httpClient.PostAsJsonAsync<CreateSpecialOfferDto>("SpecialOffers", createSpecialOfferDto);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }

        public async Task<string> DeleteSpecialOfferAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.DeleteAsync("SpecialOffers?id="+id);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }

        public async Task<Result<List<ResultSpecialOfferDto>>> GetAllSpecialOfferAsync()
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("SpecialOffers");
            return await response.ReadSafeResultAsync<List<ResultSpecialOfferDto>>();

            //     var jsonData = await responseMessage.Content.ReadAsStringAsync();
            //     var values = JsonConvert.DeserializeObject<Result<List<ResultSpecialOfferDto>>>(jsonData);
            //     return values.HandleUiResult(_uiNotifierService)
            //?? new List<ResultSpecialOfferDto>();
            // var content=await responseMessage.Content.ReadAsStringAsync();
            // var values=JsonConvert.DeserializeObject<List<ResultSpecialOfferDto>>(content);
            // return values;
        }

        public async Task<UpdateSpecialOfferDto> GetByIdSpecialOfferAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var responseMessage = await _httpClient.GetAsync("SpecialOffers/"+id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<UpdateSpecialOfferDto>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new UpdateSpecialOfferDto();
            //var content = await responseMessage.Content.ReadAsStringAsync();
            //var values = JsonConvert.DeserializeObject<UpdateSpecialOfferDto>(content);
            //return values;
        }

        public async Task<string> UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.PutAsJsonAsync<UpdateSpecialOfferDto>("SpecialOffers", updateSpecialOfferDto);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ??"";
        }
    }
}
