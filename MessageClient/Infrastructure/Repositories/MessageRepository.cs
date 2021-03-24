using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageClient.Infrastructure.Db;
using MessageClient.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace MessageClient.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MessageDbContext _messageDbContext;

        public MessageRepository(MessageDbContext messageDbContext)
        {
            _messageDbContext = messageDbContext;
        }

        public async Task AddMessageAsync(Message message)
        {
            _messageDbContext.Add(message);

            await _messageDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetNotSentMessagesAsync()
        {
            return await _messageDbContext
                .Messages
                .AsNoTracking()
                .Where(x => !x.IsSend)
                .ToArrayAsync();
        }

        public async Task SetMessageAsSentAsync(int id)
        {
            var message = _messageDbContext.Messages.First(x => x.Id == id);
            message.IsSend = true;
            _messageDbContext.Update(message);

            await _messageDbContext.SaveChangesAsync();
        }
    }
}
