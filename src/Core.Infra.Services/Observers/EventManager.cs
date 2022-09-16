using Core.Infra.Services.Observers.Interfaces;
using Core.Infra.Services.Observers.Interfaces.Messages;
using System.Collections.Generic;
using System.Linq;

namespace Core.Infra.Services.Observers
{
    public class EventManager : IEventManager
    {
        private IList<IEventListener> _listeners;
        public EventManager()
        {
            _listeners = new List<IEventListener>();
        }

        public void Attach(IEventListener eventListener)
        {
            _listeners.Add(eventListener);
        }

        public void Detach(IEventListener eventListener)
        {
            _listeners.Remove(eventListener);
        }

        public void Notify(IMessage message)
        {
            foreach(IEventListener eventListener in _listeners.Where(e => e.Tag == message.Tag))
            {
                eventListener.Update(message);
            }
        }
    }
}
