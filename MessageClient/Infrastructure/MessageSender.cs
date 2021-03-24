using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Core;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace MessageClient.Infrastructure
{
    public class MessageSender : IMessageSender
    {
        private const string JsonContentType = "application/json";

        private readonly IConfiguration _configuration;//TODO: use custom config class
        private readonly HttpClient _httpClient;
        private readonly ILogger<MessageSender> _logger;

        public MessageSender(IConfiguration configuration,
            HttpClient httpClient,
            ILogger<MessageSender> logger)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<bool> TrySendAsync(string messageText)
        {
            try
            {
                var messageParameters = new AddMessageParameters
                {
                    MessageText = messageText
                };

                var serializedParameters = JsonSerializer.Serialize(messageParameters);

                var result = await _httpClient.PostAsync(_configuration["Settings:AddMessageUrl"],
                    new StringContent(serializedParameters, Encoding.UTF8, JsonContentType));

                return result.IsSuccessStatusCode;
            }
            catch (TaskCanceledException e)
            {
                _logger.LogWarning($"Произошёл таймаут при обращении к серверу {_configuration["Settings:ServerUrl"]}");
                _logger.LogWarning(e.ToString());
                return false;
            }
        }
    }
}
