using Core.Infra.Services.Observers.Interfaces.Messages;

namespace Core.Infra.Services.Observers.Interfaces
{
    public interface IEventManager
    {
        void Attach(IEventListener eventListener);
        void Detach(IEventListener eventListener);
        void Notify(IMessage message);
    }
}
