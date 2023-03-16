using Votus.Common.ServiceBus;

namespace Votus.Voto.API.EventHandlers
{
    public class ProposicaoChangedEventHandler : IEventHandler
    {
        public void Handle(object eventModel)
        {
            Console.Write(eventModel.ToString());
            //throw new NotImplementedException();
        }
    }
}
