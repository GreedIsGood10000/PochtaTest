using System.Collections.Generic;
using System.Threading.Tasks;
using MessageServer.Infrastructure.Entities;

namespace MessageServer.Infrastructure.Repositories
{
    public interface IMessageRepository
    {
        Task AddMessage(Message message);

        Task<IEnumerable<Message>> ReadMessages();
    }
}