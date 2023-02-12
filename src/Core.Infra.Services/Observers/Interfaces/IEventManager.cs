using Core.Infra.Services.Observers.Interfaces.Messages;
using System;

namespace Core.Infra.Services.Observers.Interfaces
{
    public interface IEventManager : IDisposable
    {
        void Attach(IEventListener eventListener);
        void Detach(IEventListener eventListener);
        void Notify(IMessage message);
    }
}
