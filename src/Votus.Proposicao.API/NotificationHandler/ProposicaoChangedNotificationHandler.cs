using Azure.Messaging.ServiceBus;
using MediatR;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;
using Votus.Proposicao.API.Event;

namespace Votus.Proposicao.API.EventHandler
{
    public class ProposicaoChangedNotificationHandler : INotificationHandler<ProposicaoChangedEvent>
    {
        private readonly IConfiguration configuration;
        private readonly ServiceBusClient busClient;

        public ProposicaoChangedNotificationHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
            
            this.busClient = new ServiceBusClient(configuration["ServiceBus:ConnectionString"]);
        }
        public async Task Handle(ProposicaoChangedEvent notification, CancellationToken cancellationToken)
        {
            var messageEvent = notification;
            
            var messageJson = JsonSerializer.Serialize(messageEvent, notification.GetType());
            
            var messageBody = Encoding.UTF8.GetBytes(messageJson);

            var sender = busClient.CreateSender(typeof(ProposicaoChangedEvent).Name.ToLower());

            var message = new ServiceBusMessage
            {
                MessageId = Guid.NewGuid().ToString(),
                Body = new BinaryData(messageBody),
                Subject = typeof(ProposicaoChangedEvent).Name,
            };

            await sender.SendMessageAsync(message);
                
        }
    }
}
