using Crawlers.Domains.Entities.Interfaces.ObjectValues.Urls;

namespace Crawlers.Domains.Entities.ObjectValues.Urls.ChainOfResponsability
{
    internal class UrlClient
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
