
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.BrandServices
{
    public class BrandService : IBrandService
    {
        private readonly IApiClientFactory _factory;

        private readonly IUiNotifierService _uiNotifierService;

        public BrandService(IUiNotifierService uiNotifierService, IApiClientFactory factory)
        {
            _uiNotifierService = uiNotifierService;
            _factory = factory;
        }

        public async Task<Result<List<ResultBrandDto>>> BrandListAsync()
        {
            var _httpClient = _factory.Create("Catalog");
            var response = await _httpClient.GetAsync("Brands");
            return await response.ReadSafeResultAsync<List<ResultBrandDto>>();

        }

        public async Task<Result<string>> CreateBrandAsync(CreateBrandDto createBrandDto)
        {
            var _httpClient = _factory.Create("Catalog");
            var response=await _httpClient.PostAsJsonAsync<CreateBrandDto>("Brands",createBrandDto);
            return await response.ReadSafeResultAsync<string>();

        }

        public async Task<Result<string>> DeleteBrandAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");
            var response =await _httpClient.DeleteAsync("Brands?id="+id);
            return await response.ReadSafeResultAsync<string>();

        }

        public async Task<Result<UpdateBrandDto>> GetByIdBrandAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");
            var response = await _httpClient.GetAsync("Brands/"+id);
            return await response.ReadSafeResultAsync<UpdateBrandDto>();

        }

        public async Task<Result<string>> UpdateBrandAsync(UpdateBrandDto updateBrandDto)
        {
            var _httpClient = _factory.Create("Catalog");
            var response = await _httpClient.PostAsJsonAsync<UpdateBrandDto>("Brands", updateBrandDto);
            return await response.ReadSafeResultAsync<string>();
        }
    }
}
