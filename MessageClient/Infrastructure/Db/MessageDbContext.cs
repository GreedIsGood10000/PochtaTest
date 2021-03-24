using MessageClient.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace MessageClient.Infrastructure.Db
{
    public class MessageDbContext : DbContext
    {
        public MessageDbContext(DbContextOptions<MessageDbContext> options) : base(options)
        {
            
        }

        public DbSet<Message> Messages { get; set; }
    }
}
