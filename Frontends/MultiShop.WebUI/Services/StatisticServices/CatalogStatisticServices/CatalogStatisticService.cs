
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices
{
    public class CatalogStatisticService : ICatalogStatisticService
    {
        private readonly IApiClientFactory _factory;
        private readonly IUiNotifierService _uiNotifierService;

        public CatalogStatisticService(IUiNotifierService uiNotifierService, IApiClientFactory factory)
        {
            _uiNotifierService = uiNotifierService;
            _factory = factory;
        }

        public async Task<Result<long>> GetBrandCount()
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("Statistics/GetBrandCount");
            return await response.ReadSafeResultAsync<long>();

        }

        public async Task<Result<long>> GetCategoryCount()
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("Statistics/GetCategoryCount");
            return await response.ReadSafeResultAsync<long>();

        }

        public async Task<Result<string>> GetMaxPriceProductName()
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("Statistics/GetMaxPriceProductName");
            return await response.ReadSafeResultAsync<string>();

        }

        public async Task<Result<string>> GetMinPriceProductName()
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("Statistics/GetMinPriceProductName");
            return await response.ReadSafeResultAsync<string>();

        }

        public async Task<Result<decimal>> GetProductAvgPrice()
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("Statistics/GetProductAvgPrice");
            return await response.ReadSafeResultAsync<decimal>();

        }

        public async Task<Result<long>> GetProductCount()
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("Statistics/GetProductCount");
            return await response.ReadSafeResultAsync<long>();

        }
    }
}
