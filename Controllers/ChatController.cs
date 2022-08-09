using Chat.Models;
using Chat.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class ChatController : ControllerBase
    {
        public readonly MessageService _messageService;

        public ChatController(MessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<List<Message>> Get() =>
            await _messageService.GetAsync();

        [HttpGet("sender/{id}")]
        public async Task<ActionResult<List<Message>>> Get(string sender)
        {
            var message = await _messageService.GetMessagesAsync(sender);
            
            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        [HttpPost("send")]
        public async Task<bool> SendMessage(SendMessageRequest request)
        {
            try
            {
                await _messageService.SendMessage(request.sender, request.recipient, request.txt);
            }
            catch
            {
                return false;
            }
            return true;
        }

        [HttpGet("recent-chat")]
        public async Task<List<Message>> GetChatRecent(string sender, string recipient)
        {
            var msgList = await _messageService.GetChatRecent(sender, recipient);
            return msgList;
        }

        [HttpGet("all-chat")]
        public async Task<List<Message>> GetAllRecent()
        {
            var msgList = await _messageService.GetAllRecent();
            return msgList;
        }
    }
}
