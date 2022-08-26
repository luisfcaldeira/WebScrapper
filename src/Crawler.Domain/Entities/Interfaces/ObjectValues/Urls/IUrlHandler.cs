using Crawlers.Domain.Entities.ObjectValues.Urls;

namespace Crawlers.Domain.Entities.Interfaces.ObjectValues.Urls
{
    public interface IUrlHandler
    {
        IUrlHandler SetNext(IUrlHandler handler);

        Url Handle(string request);
    }
}
