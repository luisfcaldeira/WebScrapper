using Core.Infra.Services.Observers.Entities.Messages;
using Core.Infra.Services.Observers.Interfaces;
using Core.Infra.Services.Observers.Interfaces.Messages;
using System;

namespace ConsoleApp.WebScrapper.InnerServices.Listeners
{
    internal class LogWarningEventListener : CoreListener, IEventListener
    {
        public override Tag Tag { get; } = Tag.Warning;

        public override void Update(IMessage message)
        {
            var beforeColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Write(message);
            Log(message);


            Console.ForegroundColor = beforeColor;
        }
    }
}
