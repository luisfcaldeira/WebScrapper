using Crawlers.Domains.Entities.ObjectValues.Urls;

namespace Crawlers.Domains.Entities.Interfaces.ObjectValues.Urls
{
    public interface IUrlHandler
    {
        IUrlHandler SetNext(IUrlHandler handler);

        Url Handle(string request);
    }
}
