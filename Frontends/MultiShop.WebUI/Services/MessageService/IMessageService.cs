using MultiShop.DtoLayer.MessageDtos;

namespace MultiShop.WebUI.Services.MessageService
{
    public interface IMessageService
    {
        Task<List<ResultInboxMessageDto>> GetInboxMessageAsync(string id);
        Task<List<ResultSendboxMessageDto>> GetSendboxMessageAsync(string id);
     
    }
}
