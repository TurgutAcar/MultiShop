using MultiShop.Message.Dtos;
using MultiShop.Shared.Responses;

namespace MultiShop.Message.Services
{
    public interface IUserMessageService
    {
        Task<Result<List<ResultMessageDto>>> GetAllMessageAsync();
        Task<Result<List<ResultInboxMessageDto>>> GetInboxMessageAsync(string id);
        Task<Result<List<ResultSendboxMessageDto>>> GetSendboxMessageAsync(string id);
        Task<Result<string>> CreateMessageAsync(CreateMessageDto createMessageDto);
        Task<Result<string>> UpdateMessageAsync(UpdateMessageDto updateMessageDto); 
        Task<Result<string>> DeleteMessageAsync(int id);
        Task<Result<GetByIdMessageDto>> GetByIdMessageAsync(int id);
        Task<Result<int>> GetTotalMessageCount();
        Task<Result<int>> GetTotalMessageCountByReceiverId(string id);
    }
}
