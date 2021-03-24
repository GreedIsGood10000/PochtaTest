using System.Threading.Tasks;

namespace MessageClient.Infrastructure
{
    public interface IMessageSender
    {
        public Task<bool> TrySendAsync(string messageText);
    }
}