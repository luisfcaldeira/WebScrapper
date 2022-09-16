using Core.Infra.Services.Observers.Entities.Messages;

namespace Core.Infra.Services.Observers.Interfaces.Messages
{
    public interface IMessage
    {
        Tag Tag { get; }
        string Description { get; }
    }
}
