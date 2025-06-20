
namespace MultiShop.WebUI.Services.StatisticServices.DiscountStatisticServices
{
    public class DiscountStatisticService : IDiscountStatisticService
    {
        private readonly HttpClient _httpClient;

        public DiscountStatisticService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetDiscountCouponCount()
        {
            var response = await _httpClient.GetAsync("discounts/GetDiscountCouponCount");
            var value=await response.Content.ReadFromJsonAsync<int>();
            return value;
        }
    }
}
