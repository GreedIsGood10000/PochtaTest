using System;
using System.Threading.Tasks;
using MessageClient.Controllers.Parameters;
using MessageClient.Infrastructure;
using MessageClient.Infrastructure.Entities;
using MessageClient.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MessageClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMessageSender _messageSender;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IMessageRepository messageRepository,
            IMessageSender messageSender,
            ILogger<HomeController> logger)
        {
            _messageRepository = messageRepository;
            _messageSender = messageSender;
            _logger = logger;
        }

        [HttpGet]
        public ViewResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage([FromForm] AddMessageParameters messageParameters)
        {
            try
            {
                var isSent = await _messageSender.TrySendAsync(messageParameters.MessageText);

                var message = Message.Create(messageParameters.MessageText, isSent);
                await _messageRepository.AddMessageAsync(message);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
    }
}
