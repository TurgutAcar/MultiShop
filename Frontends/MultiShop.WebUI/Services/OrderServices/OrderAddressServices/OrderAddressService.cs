using System.Net.Http.Json;
using MultiShop.DtoLayer.OrderDtos.OrderAddressDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.OrderServices.OrderAddressServices
{
    public class OrderAddressService : IOrderAddressService
    {
        private readonly IApiClientFactory _factory;
        private readonly IUiNotifierService _uiNotifierService;

        public OrderAddressService(IUiNotifierService uiNotifierService,IApiClientFactory factory)
        {
            _uiNotifierService = uiNotifierService;
            _factory = factory;
        }

        public async Task<string> CreateOrderAddressesAsync(CreateOrderAddressDto createOrderAddressDto)
        {
            var _httpClient = _factory.Create("Order");

            var response = await _httpClient.PostAsJsonAsync<CreateOrderAddressDto>("Addresses", createOrderAddressDto);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }
    }
}
