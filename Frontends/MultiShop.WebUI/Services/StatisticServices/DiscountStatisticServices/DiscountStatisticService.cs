
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.StatisticServices.DiscountStatisticServices
{
    public class DiscountStatisticService : IDiscountStatisticService
    {
        private readonly IApiClientFactory _factory;
        private readonly IUiNotifierService _uiNotifierService;

        public DiscountStatisticService(IUiNotifierService uiNotifierService, IApiClientFactory factory)
        {
            _uiNotifierService = uiNotifierService;
            _factory = factory;
        }

        public async Task<int> GetDiscountCouponCount()
        {
            var _httpClient = _factory.Create("Discount");

            var response = await _httpClient.GetAsync("discounts/GetDiscountCouponCount");
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<int>>(jsonData);
            return values.HandleUiResult(_uiNotifierService);
            //var value=await response.Content.ReadFromJsonAsync<int>();
            // return value;
        }
    }
}
