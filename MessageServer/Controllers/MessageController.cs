using System.Net;
using System.Threading.Tasks;
using MessageServer.Controllers.Parameters;
using MessageServer.Infrastructure.Entities;
using MessageServer.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MessageServer.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageRepository _messageRepository;

        public MessageController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage([FromBody] AddMessageParameters messageText)
        {
            var message = Message.Create(messageText.MessageText, HttpContext.Connection.RemoteIpAddress);

            await _messageRepository.AddMessage(message);

            return Ok();
        }
    }
}
