using Crawlers.Domain.Entities.Interfaces.ObjectValues.Urls;

namespace Crawlers.Domain.Entities.ObjectValues.Urls.ChainOfResponsability
{
    public class UrlClient
    {
        public UrlClient(IUrlHandler urlHandler)
        {
            UrlHandler = urlHandler;
        }

        public IUrlHandler UrlHandler { get; }

        public Url Handle(string url)
        {
            return UrlHandler.Handle(url);
        }
    }
}
