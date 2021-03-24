using Microsoft.Extensions.Configuration;

namespace MessageClient.Infrastructure
{
    public class MessageServerConfiguration
    {
        public string ConnectionString { get; }

        public string AddMessageUrl { get; }

        public int ConnectionTimeout { get; }

        public MessageServerConfiguration(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("ClientMessagesConnectionString");
            AddMessageUrl = configuration["Settings:AddMessageUrl"];
            ConnectionTimeout = int.Parse(configuration["Settings:ConnectionTimeout"]);
        }
    }
}
