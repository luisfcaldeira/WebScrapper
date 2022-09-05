using Crawlers.Domains.Entities.ObjectValues.Pages;
using HtmlAgilityPack;

namespace Crawlers.Infra.WebScrapperServices.Interfaces.InnerServices
{
    public interface IWebNavigator
    {
        HtmlDocument GetDocument(Page page);
    }
}
