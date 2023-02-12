using Crawlers.Domains.Entities.ObjectValues.Pages;
using HtmlAgilityPack;

namespace Crawlers.Infra.WebScrapperServices.Interfaces.InnerServices
{
    public interface IWebNavigator : IDisposable
    {
        HtmlDocument GetDocument(Page page);
    }
}
