using Core.Infra.Services.Observers.Entities.Messages;
using Core.Infra.Services.Observers.Interfaces;
using Core.Infra.Services.Observers.Interfaces.Messages;
using System;

namespace ConsoleApp.WebScrapper.InnerServices.Listeners
{
    internal class LogErrorEventListener : CoreListener, IEventListener
    {
        public override Tag Tag { get; } = Tag.Error;

        public override void Update(IMessage message)
        {
            var beforeColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            Log(message);
            Write(message);

            Console.ForegroundColor = beforeColor;
        }
    }
}
