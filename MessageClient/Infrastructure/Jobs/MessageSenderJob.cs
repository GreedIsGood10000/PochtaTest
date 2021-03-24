using System;
using System.Threading.Tasks;
using MessageClient.Infrastructure.Db;
using MessageClient.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;

namespace MessageClient.Infrastructure.Jobs
{
    [DisallowConcurrentExecution]
    public class MessageSenderJob : IJob
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageSender _messageSender;
        private readonly ILogger<MessageSenderJob> _logger;

        public MessageSenderJob(
            IServiceProvider serviceProvider,
            IMessageSender messageSender,
            ILogger<MessageSenderJob> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _messageSender = messageSender;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MessageDbContext>();

                await Execute(dbContext);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task Execute(MessageDbContext dbContext)
        {
            var messageRepository = new MessageRepository(dbContext);
            var unsentMessages = await messageRepository.GetNotSentMessagesAsync();

            foreach (var unsentMessage in unsentMessages)
            {
                var isSent = await _messageSender.TrySendAsync(unsentMessage.MessageText);
                if (isSent)
                {
                    await messageRepository.SetMessageAsSentAsync(unsentMessage.Id);
                }
            }
        }
    }
}
