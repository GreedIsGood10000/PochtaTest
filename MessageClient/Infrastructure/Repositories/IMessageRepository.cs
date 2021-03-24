using System.Collections.Generic;
using System.Threading.Tasks;
using MessageClient.Infrastructure.Entities;

namespace MessageClient.Infrastructure.Repositories
{
    public interface IMessageRepository
    {
        Task AddMessageAsync(Message message);

        Task<IEnumerable<Message>> GetNotSentMessagesAsync();

        Task SetMessageAsSentAsync(int id);
    }
}