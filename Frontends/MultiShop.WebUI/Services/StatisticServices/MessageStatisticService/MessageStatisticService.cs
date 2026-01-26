
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.StatisticServices.MessageStatisticService
{
    public class MessageStatisticService : IMessageStatisticService
    {
        private readonly IApiClientFactory _factory;
        private readonly IUiNotifierService _uiNotifierService;

        public MessageStatisticService(IUiNotifierService uiNotifierService, IApiClientFactory factory)
        {
            _uiNotifierService = uiNotifierService;
            _factory = factory;
        }

        public async Task<int> GetTotalMessageCount()
        {
            var _httpClient = _factory.Create("Message");

            var response = await _httpClient.GetAsync("UserMessages/GetTotalMessageCount");
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<int>>(jsonData);
            return values.HandleUiResult(_uiNotifierService);

            // var value=await response.Content.ReadFromJsonAsync<int>();
            //  return value;
        }

        public async Task<int> GetTotalMessageCountByReceiverId(string id)
        {
            var _httpClient = _factory.Create("Message");

            var response = await _httpClient.GetAsync("UserMessages/GetTotalMessageCountByReceiverId?id="+id);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<int>>(jsonData);
            return values.HandleUiResult(_uiNotifierService);
    

            // var value = await response.Content.ReadFromJsonAsync<int>();
            // return value;
        }
    }
}
