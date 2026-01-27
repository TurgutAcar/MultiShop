
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Extensions;
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

        public async Task<Result<int>> GetTotalMessageCount()
        {
            var _httpClient = _factory.Create("Message");

            var response = await _httpClient.GetAsync("UserMessages/GetTotalMessageCount");
            return await response.ReadSafeResultAsync<int>();

        }

        public async Task<Result<int>> GetTotalMessageCountByReceiverId(string id)
        {
            var _httpClient = _factory.Create("Message");

            var response = await _httpClient.GetAsync("UserMessages/GetTotalMessageCountByReceiverId?id="+id);
            return await response.ReadSafeResultAsync<int>();

        }
    }
}
