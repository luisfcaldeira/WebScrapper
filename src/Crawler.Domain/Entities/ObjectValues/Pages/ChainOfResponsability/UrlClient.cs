using Crawlers.Domains.Interfaces.Entities.ObjectValues.Pages;

namespace Crawlers.Domains.Entities.ObjectValues.Pages.ChainOfResponsability
{
    internal class UrlClient
    {
        public UrlClient(IPageHandler urlHandler)
        {
            UrlHandler = urlHandler;
        }

        public IPageHandler UrlHandler { get; }

        public Page Handle(string url)
        {
            return UrlHandler.Handle(url);
        }
    }
}
