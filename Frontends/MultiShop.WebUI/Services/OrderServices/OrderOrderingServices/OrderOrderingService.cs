using MultiShop.DtoLayer.OrderDtos.OrderOrderingDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MultiShop.WebUI.Services.OrderServices.OrderOrderingServices
{
    public class OrderOrderingService : IOrderOrderingService
    {
        private readonly IApiClientFactory _factory;
        private readonly IUiNotifierService _uiNotifierService;

        public OrderOrderingService(IApiClientFactory factory, IUiNotifierService uiNotifierService)
        {
            _factory = factory;
            _uiNotifierService = uiNotifierService;
        }

        public async Task<List<ResultOrderingByUserIdDto>> GetOrderingByUserId(string id)
        {
            var _httpClient = _factory.Create("Order");

            var responseMessage = await _httpClient.GetAsync($"orderings/GetOrderingByUserId/{id}");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<List<ResultOrderingByUserIdDto>>> (jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new List<ResultOrderingByUserIdDto>();
            // var jsonData=await responseMessage.Content.ReadAsStringAsync();
            // var values= JsonConvert.DeserializeObject<List<ResultOrderingByUserIdDto>>(jsonData);
            // return values;

        }
    }
}
