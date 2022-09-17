using Core.Infra.Services.Observers.Entities.Messages;
using Core.Infra.Services.Observers.Interfaces;
using Core.Infra.Services.Observers.Interfaces.Messages;
using System;
using System.Diagnostics;

namespace ConsoleApp.WebScrapper.InnerServices.Listeners
{
    public abstract class CoreListener : IEventListener
    {
        public abstract Tag Tag { get; }

        protected void Write(IMessage message)
        {
            Console.WriteLine($"[{message.Tag}] '{message.Description}'");
        }

        protected void Log(IMessage message)
        {
            Debug.WriteLine($"[{message.Tag}] '{message.Description}'");
        }

        public virtual void Update(IMessage message)
        {
            Write(message);
        }
    }
}
