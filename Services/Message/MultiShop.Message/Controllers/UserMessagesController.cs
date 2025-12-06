using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Message.Dtos;
using MultiShop.Message.Services;

namespace MultiShop.Message.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMessagesController : ControllerBase
    {
        private IUserMessageService _userMessageService;

        public UserMessagesController(IUserMessageService userMessageService)
        {
            _userMessageService = userMessageService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMessage()
        {
            var response=await _userMessageService.GetAllMessageAsync();
            return StatusCode(response.StatusCode,response);
        }
        [HttpGet("GetByIdMessage")]
        public async Task<IActionResult> GetByIdMessage(int id)
        {
            var response = await _userMessageService.GetByIdMessageAsync(id);
            return StatusCode(response.StatusCode, response);
          
        }
        [HttpGet("GetMessageSendbox")]
        public async Task<IActionResult> GetMessageSendbox(string id)
        {
            var response = await _userMessageService.GetSendboxMessageAsync(id);
            return StatusCode(response.StatusCode, response);
          
        }
        [HttpGet("GetMessageInbox")]
        public async Task<IActionResult> GetMessageInbox(string id)
        {
            var response = await _userMessageService.GetInboxMessageAsync(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMessageAsync(CreateMessageDto createMessageDto)
        {
            var response = await _userMessageService.CreateMessageAsync(createMessageDto);
            return StatusCode(response.StatusCode, response);
          
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMessageAsync(int id)
        {

            var response = await _userMessageService.DeleteMessageAsync(id);
            return StatusCode(response.StatusCode, response);
          
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMessageAsync(UpdateMessageDto updateMessageDto)
        {
            var response = await _userMessageService.UpdateMessageAsync(updateMessageDto);
            return StatusCode(response.StatusCode, response);
           
        }
        [HttpGet("GetTotalMessageCount")]
        public async Task<IActionResult> GetTotalMessageCount()
        {
            var response = await _userMessageService.GetTotalMessageCount();
            return StatusCode(response.StatusCode, response);
          
        }
        [HttpGet("GetTotalMessageCountByReceiverId")]
        public async Task<IActionResult> GetTotalMessageCountByReceiverId(string id)
        {
            var response = await _userMessageService.GetTotalMessageCountByReceiverId(id);
            return StatusCode(response.StatusCode, response);
         
        }
    }
}
