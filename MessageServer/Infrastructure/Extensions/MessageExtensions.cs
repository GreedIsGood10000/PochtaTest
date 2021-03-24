using MessageServer.Infrastructure.Entities;
using MessageServer.Models;

namespace MessageServer.Infrastructure.Extensions
{
    public static class MessageExtensions
    {
        public static ViewMessage GetViewMessage(this Message message)
        {
            return new ViewMessage
            {
                MessageText = message.MessageText,
                IpAddress = message.SenderIp
            };
        }
    }
}
