using System;
using System.Linq;
using System.Threading.Tasks;
using Core;
using MessageServer.Infrastructure.Entities;
using MessageServer.Infrastructure.Extensions;
using MessageServer.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MessageServer.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ILogger<MessageController> _logger;

        public MessageController(IMessageRepository messageRepository,
            ILogger<MessageController> logger)
        {
            _messageRepository = messageRepository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage([FromBody] AddMessageParameters messageText)
        {
            try
            {
                var message = Message.Create(messageText.MessageText, HttpContext.Connection.RemoteIpAddress);

                await _messageRepository.AddMessageAsync(message);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            try
            {
                var messages = await _messageRepository.ReadMessagesAsync();

                var viewMessages = messages.Select(x => x.GetViewMessage());

                return View(viewMessages);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
    }
}
