using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MessageClient.Infrastructure
{
    public class MessageSender : IMessageSender
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public MessageSender(IConfiguration configuration,
            HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<bool> TrySendAsync(string messageText)
        {
            throw new NotImplementedException();
        }
    }
}
