using MultiShop.DtoLayer.MessageDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Handlers;
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

        public async Task<List<ResultInboxMessageDto>> GetInboxMessageAsync(string id)
        {
            var _httpClient = _factory.Create("Message");

            var responseMessage = await _httpClient.GetAsync("userMessages/GetMessageInbox?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<List<ResultInboxMessageDto>>> (jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new List<ResultInboxMessageDto>();
            //var values =await responseMessage.Content.ReadFromJsonAsync<List<ResultInboxMessageDto>>();
            //return values;
        }

        public async Task<List<ResultSendboxMessageDto>> GetSendboxMessageAsync(string id)
        {
            var _httpClient = _factory.Create("Message");

            var responseMessage = await _httpClient.GetAsync("userMessages/GetMessageSendbox?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<List<ResultSendboxMessageDto>>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new List<ResultSendboxMessageDto>();
            // var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultSendboxMessageDto>>();
            //return values;
        }
    }
}
