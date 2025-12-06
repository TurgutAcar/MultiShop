using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MultiShop.Message.DAL.Context;
using MultiShop.Message.DAL.Entities;
using MultiShop.Message.Dtos;
using MultiShop.Shared.Responses;

namespace MultiShop.Message.Services
{
    public class UserMessageService : IUserMessageService
    {
        private readonly MessageContext _messageContext;
        private readonly IMapper _mapper;

        public UserMessageService(MessageContext messageContext, IMapper mapper)
        {
            _messageContext = messageContext;
            _mapper = mapper;
        }

        public async Task<Result<string>> CreateMessageAsync(CreateMessageDto createMessageDto)
        {
            var value = _mapper.Map<UserMessage>(createMessageDto);
            await _messageContext.UserMessages.AddAsync(value);
            await _messageContext.SaveChangesAsync();
            return "Mesaj oluşturuldu";
        }

        public async Task<Result<string>> DeleteMessageAsync(int id)
        {
            var values = await _messageContext.UserMessages.FindAsync(id);
            _messageContext.UserMessages.Remove(values);
            await _messageContext.SaveChangesAsync();
            return "Mesaj silindi";

        }

        public async Task<Result<List<ResultMessageDto>>> GetAllMessageAsync()
        {
            var values = await _messageContext.UserMessages.ToListAsync();
            return _mapper.Map<List<ResultMessageDto>>(values);
        }

        public async Task<Result<GetByIdMessageDto>> GetByIdMessageAsync(int id)
        {
            var value = await _messageContext.UserMessages.FindAsync(id);
            return _mapper.Map<GetByIdMessageDto>(value);
        }

        public async Task<Result<List<ResultInboxMessageDto>>> GetInboxMessageAsync(string id)
        {
            var values = await _messageContext.UserMessages.Where(x=>x.ReceiverId==id).ToListAsync();
            return _mapper.Map<List<ResultInboxMessageDto>>(values);
        }

        public async Task<Result<List<ResultSendboxMessageDto>>> GetSendboxMessageAsync(string id)
        {
            var values = await _messageContext.UserMessages.Where(x => x.SenderId == id).ToListAsync();
            return _mapper.Map<List<ResultSendboxMessageDto>>(values);
        }

        public async Task<Result<int>> GetTotalMessageCount()
        {
            int values=await _messageContext.UserMessages.CountAsync();
            return values;
        }

        public async Task<Result<int>> GetTotalMessageCountByReceiverId(string id)
        {
           var values=await _messageContext.UserMessages.Where(x=>x.ReceiverId==id).CountAsync();
            return values;
        }

        public async  Task<Result<string>> UpdateMessageAsync(UpdateMessageDto updateMessageDto)
        {
            var values = _mapper.Map<UserMessage>(updateMessageDto);
              _messageContext.UserMessages.Update(values);
            await _messageContext.SaveChangesAsync();
            return "Mesaj kaydedildi";

        }
    }
}
