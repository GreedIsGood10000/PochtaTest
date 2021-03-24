using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MessageServer.Infrastructure.Db;
using MessageServer.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace MessageServer.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MessageDbContext _dbContext;

        public MessageRepository(MessageDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddMessageAsync(Message message)
        {
            _dbContext.Add(message);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> ReadMessagesAsync()
        {
            return await _dbContext.Messages.ToArrayAsync();
        }
    }
}
