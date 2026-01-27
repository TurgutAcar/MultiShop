using MultiShop.DtoLayer.MessageDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MultiShop.WebUI.Services.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly IApiClientFactory _factory;
        private readonly IUiNotifierService _uiNotifierService;


        public MessageService(IUiNotifierService uiNotifierService, IApiClientFactory factory)
        {
            _uiNotifierService = uiNotifierService;
            _factory = factory;
        }

        public async Task<Result<List<ResultInboxMessageDto>>> GetInboxMessageAsync(string id)
        {
            var _httpClient = _factory.Create("Message");
            var response = await _httpClient.GetAsync("userMessages/GetMessageInbox?id=" + id);
            return await response.ReadSafeResultAsync<List<ResultInboxMessageDto>>();

        }

        public async Task<Result<List<ResultSendboxMessageDto>>> GetSendboxMessageAsync(string id)
        {
            var _httpClient = _factory.Create("Message");

            var response = await _httpClient.GetAsync("userMessages/GetMessageSendbox?id=" + id);
            return await response.ReadSafeResultAsync<List<ResultSendboxMessageDto>>();

        }
    }
}
