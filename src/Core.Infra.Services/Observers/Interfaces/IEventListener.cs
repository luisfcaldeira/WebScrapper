using Core.Infra.Services.Observers.Entities.Messages;
using Core.Infra.Services.Observers.Interfaces.Messages;

namespace Core.Infra.Services.Observers.Interfaces
{
    public interface IEventListener
    {
        Tag Tag { get; }
        void Update(IMessage message);
    }
}
