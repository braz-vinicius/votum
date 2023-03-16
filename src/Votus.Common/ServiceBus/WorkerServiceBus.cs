using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Votus.Common.ServiceBus
{
    public class WorkerServiceBus<TTopic,TEvent> : IHostedService, IDisposable where TTopic : class,IEventHandler
    {
        private readonly IConfiguration configuration;
        private readonly IServiceScopeFactory serviceProvider;
        private ServiceBusClient client;
        private ServiceBusProcessor processor;

        public WorkerServiceBus(IConfiguration configuration, IServiceScopeFactory service)
        {
            this.configuration = configuration;
            this.serviceProvider = service;
        }

        public void Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            client = new ServiceBusClient(configuration["ServiceBus:ConnectionString"]);

            processor = client.CreateProcessor(typeof(TEvent).Name.ToLower(), "default", new ServiceBusProcessorOptions());

            // add handler to process messages
            processor.ProcessMessageAsync += MessageHandler;

            // add handler to process any errors
            processor.ProcessErrorAsync += ErrorHandler;

            // start processing 
            await processor.StartProcessingAsync();

        }

        // handle received messages
        async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();

            var @event = JsonSerializer.Deserialize<TEvent>(body);

            using (var scope = serviceProvider.CreateScope()) // this will use `IServiceScopeFactory` internally
            {
                var atividadeService = scope.ServiceProvider.GetService<TTopic>();
                atividadeService.Handle(@event);
                

            }


            // complete the message. messages is deleted from the subscription. 
            await args.CompleteMessageAsync(args.Message);
        }

        // handle any errors when receiving messages
        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await processor.DisposeAsync();
            await client.DisposeAsync();
        }
    }

}
