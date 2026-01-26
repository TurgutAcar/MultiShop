
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Handlers;
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

        public async Task<long> GetBrandCount()
        {
            var _httpClient = _factory.Create("Catalog");

            var responseMessage = await _httpClient.GetAsync("Statistics/GetBrandCount");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<long>>(jsonData);
            return values.HandleUiResult(_uiNotifierService);

            //var values = await responseMessage.Content.ReadFromJsonAsync<long>();
            //return values;
        }

        public async Task<long> GetCategoryCount()
        {
            var _httpClient = _factory.Create("Catalog");

            var responseMessage = await _httpClient.GetAsync("Statistics/GetCategoryCount");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<long>>(jsonData);
            return values.HandleUiResult(_uiNotifierService);
            // var values = await responseMessage.Content.ReadFromJsonAsync<long>();
            // return values;
        }

        public async Task<string> GetMaxPriceProductName()
        {
            var _httpClient = _factory.Create("Catalog");

            var responseMessage = await _httpClient.GetAsync("Statistics/GetMaxPriceProductName");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService);
           // var values = await responseMessage.Content.ReadAsStringAsync();
           // return values;
        }

        public async Task<string> GetMinPriceProductName()
        {
            var _httpClient = _factory.Create("Catalog");

            var responseMessage = await _httpClient.GetAsync("Statistics/GetMinPriceProductName");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService);
            //var values = await responseMessage.Content.ReadAsStringAsync();
            // return values;
        }

        public async Task<decimal> GetProductAvgPrice()
        {
            var _httpClient = _factory.Create("Catalog");

            var responseMessage = await _httpClient.GetAsync("Statistics/GetProductAvgPrice");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<decimal>>(jsonData);
            return values.HandleUiResult(_uiNotifierService);
            //   var values = await responseMessage.Content.ReadFromJsonAsync<decimal>();
            //return values;
        }

        public async Task<long> GetProductCount()
        {
            var _httpClient = _factory.Create("Catalog");

            var responseMessage = await _httpClient.GetAsync("Statistics/GetProductCount");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<long>>(jsonData);
            return values.HandleUiResult(_uiNotifierService);
            // var values = await responseMessage.Content.ReadFromJsonAsync<long>();
            // return values;
        }
    }
}
