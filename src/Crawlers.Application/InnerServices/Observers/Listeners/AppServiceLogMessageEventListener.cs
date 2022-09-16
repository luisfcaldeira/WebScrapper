using Core.Infra.Services.Observers.Entities.Messages;
using Core.Infra.Services.Observers.Interfaces;
using Core.Infra.Services.Observers.Interfaces.Messages;
using System;

namespace Crawlers.Application.InnerServices.Observers.Listeners
{
    internal class AppServiceLogMessageEventListener : IEventListener
    {
        public Tag Tag { get; set; } = Tag.LogMessage;

        public void Update(IMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
