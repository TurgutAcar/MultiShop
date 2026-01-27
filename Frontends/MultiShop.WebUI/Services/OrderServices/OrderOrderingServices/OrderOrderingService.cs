using MultiShop.DtoLayer.OrderDtos.OrderOrderingDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Extensions;
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

        public async Task<Result<List<ResultOrderingByUserIdDto>>> GetOrderingByUserId(string id)
        {
            var _httpClient = _factory.Create("Order");

            var response = await _httpClient.GetAsync($"orderings/GetOrderingByUserId/{id}");
            return await response.ReadSafeResultAsync<List<ResultOrderingByUserIdDto>>();

        }
    }
}
