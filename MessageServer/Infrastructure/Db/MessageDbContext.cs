using MessageServer.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace MessageServer.Infrastructure.Db
{
    public class MessageDbContext : DbContext
    {
        public MessageDbContext(DbContextOptions<MessageDbContext> options) : base(options)
        {
            
        }

        public DbSet<Message> Messages { get; set; }
    }
}
