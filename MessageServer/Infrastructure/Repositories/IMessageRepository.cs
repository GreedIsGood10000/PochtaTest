using System.Collections.Generic;
using System.Threading.Tasks;
using MessageServer.Infrastructure.Entities;

namespace MessageServer.Infrastructure.Repositories
{
    public interface IMessageRepository
    {
        Task AddMessageAsync(Message message);

        Task<IEnumerable<Message>> ReadMessagesAsync();
    }
}