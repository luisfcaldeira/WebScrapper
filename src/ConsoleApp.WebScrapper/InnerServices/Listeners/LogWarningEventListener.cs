using Core.Infra.Services.Observers.Entities.Messages;
using Core.Infra.Services.Observers.Interfaces;
using Core.Infra.Services.Observers.Interfaces.Messages;

namespace ConsoleApp.WebScrapper.InnerServices.Listeners
{
    internal class LogWarningEventListener : CoreListener, IEventListener
    {
        public override Tag Tag { get; } = Tag.Warning;

        public override void Update(IMessage message)
        {
            Log(message);
        }
    }
}
