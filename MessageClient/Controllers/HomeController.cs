using System.Threading.Tasks;
using MessageClient.Controllers.Parameters;
using MessageClient.Infrastructure;
using MessageClient.Infrastructure.Entities;
using MessageClient.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MessageClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMessageSender _messageSender;

        public HomeController(IMessageRepository messageRepository, IMessageSender messageSender)
        {
            _messageRepository = messageRepository;
            _messageSender = messageSender;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage(AddMessageParameters messageParameters)
        {
            var isSent = await _messageSender.TrySendAsync(messageParameters.MessageText);

            var message = Message.Create(messageParameters.MessageText, isSent);
            await _messageRepository.AddMessageAsync(message);

            return RedirectToAction(nameof(Index));
        }
    }
}
