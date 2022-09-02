using Crawlers.Domains.Entities.ObjectValues.Urls;
using HtmlAgilityPack;

namespace Crawlers.Infra.WebScrapperServices.Interfaces.InnerServices
{
    public interface IWebNavigator
    {
        HtmlDocument GetDocument(Page page);
    }
}
