using MultiShop.DtoLayer.DiscountDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Handlers;
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

        public async Task<GetDiscountCodeDetailByCode> GetDiscountCode(string code)
        {
            var _httpClient = _factory.Create("Discount");
            var responseMessage = await _httpClient.GetAsync($"discounts/GetCodeDetailByCode?code="+code);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<GetDiscountCodeDetailByCode>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new GetDiscountCodeDetailByCode();
            //var values=await responseMessage.Content.ReadFromJsonAsync<GetDiscountCodeDetailByCode>();
            //return values;
        }

        public async Task<int> GetDiscountCouponCountRate(string code)
        {
            var _httpClient = _factory.Create("Discount");

            var responseMessage = await _httpClient.GetAsync($"discounts/GetDiscountCouponCountRate?code="+code);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<int>>(jsonData);
            return values.HandleUiResult(_uiNotifierService);
      
            //var values = await responseMessage.Content.ReadFromJsonAsync<int>();
            //return values;
        }
    }
}
