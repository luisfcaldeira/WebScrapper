using Core.Infra.Services.Observers.Entities.Messages;
using Core.Infra.Services.Observers.Interfaces;

namespace ConsoleApp.WebScrapper.InnerServices.Listeners
{
    public class LogMessageEventListener : CoreListener, IEventListener
    {
        public override Tag Tag { get; } = Tag.LogMessage;

    }
}
