using MultiShop.DtoLayer.MessageDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.MessageService
{
    public interface IMessageService
    {
        Task<Result<List<ResultInboxMessageDto>>> GetInboxMessageAsync(string id);
        Task<Result<List<ResultSendboxMessageDto>>> GetSendboxMessageAsync(string id);
     
    }
}
