using Core.Infra.Services.Observers.Interfaces;
using Core.Infra.Services.Observers.Interfaces.Messages;
using System.Collections.Generic;
using System.Linq;

namespace Core.Infra.Services.Observers
{
    public class EventManager : IEventManager
    {
        private static IList<IEventListener> _listeners = new List<IEventListener>();
        private IList<IEventListener> Listeners {
            get {
                if (_listeners == null)
                    _listeners = new List<IEventListener>();

                return _listeners;
                }
            }

        public void Attach(IEventListener eventListener)
        {
            Listeners.Add(eventListener);
        }

        public void Detach(IEventListener eventListener)
        {
            Listeners.Remove(eventListener);
        }

        public void Dispose()
        {
        }

        public void Notify(IMessage message)
        {
            foreach(IEventListener eventListener in Listeners.Where(e => e.Tag == message.Tag))
            {
                eventListener.Update(message);
            }
        }
    }
}
