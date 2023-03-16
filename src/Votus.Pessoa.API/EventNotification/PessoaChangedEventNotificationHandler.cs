using Azure.Messaging.ServiceBus;
using MediatR;
using System.Configuration;
using System.Text;
using System.Text.Json;
using Votus.Pessoa.API.Events;

namespace Votus.Pessoa.API.EventNotification
{
    public class PessoaChangedEventNotificationHandler : INotificationHandler<PessoaChangedEvent>
    {
        private IConfiguration configuration;
        private ServiceBusClient busClient;

        public PessoaChangedEventNotificationHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.busClient = new ServiceBusClient(configuration["ServiceBus:ConnectionString"]);
        }
        public async Task Handle(PessoaChangedEvent notification, CancellationToken cancellationToken)
        {
            var messageEvent = notification;
            var messageJson = JsonSerializer.Serialize(messageEvent, notification.GetType());
            var messageBody = Encoding.UTF8.GetBytes(messageJson);

            var sender = busClient.CreateSender(typeof(PessoaChangedEvent).Name.ToLower());

            var message = new ServiceBusMessage
            {
                MessageId = Guid.NewGuid().ToString(),
                Body = new BinaryData(messageBody),
                Subject = typeof(PessoaChangedEvent).Name,
            };

            await sender.SendMessageAsync(message);
        }
    }
}
