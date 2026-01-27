using MultiShop.DtoLayer.DiscountDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.DiscountServices
{
    public class DiscountService : IDiscountService
    {
        private readonly IApiClientFactory _factory;
        private readonly IUiNotifierService _uiNotifierService;


        public DiscountService(IUiNotifierService uiNotifierService, IApiClientFactory factory)
        {
            _uiNotifierService = uiNotifierService;
            _factory = factory;
        }

        public async Task<Result<GetDiscountCodeDetailByCode>> GetDiscountCode(string code)
        {
            var _httpClient = _factory.Create("Discount");
            var response = await _httpClient.GetAsync($"discounts/GetCodeDetailByCode?code="+code);
            return await response.ReadSafeResultAsync<GetDiscountCodeDetailByCode>();
        }

        public async Task<Result<int>> GetDiscountCouponCountRate(string code)
        {
            var _httpClient = _factory.Create("Discount");

            var response = await _httpClient.GetAsync($"discounts/GetDiscountCouponCountRate?code="+code);
            return await response.ReadSafeResultAsync<int>();

        }
    }
}
