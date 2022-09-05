using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Infra.WebScrapperServices.Interfaces.InnerServices;
using HtmlAgilityPack;

namespace Mockers.Contexts.Crawlers.Infra.Services
{
    public class WebNavigatorMock : IWebNavigator
    {
        public string Html { get; set; } = string.Empty;

        public HtmlDocument GetDocument(Page page)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(Html);
            return htmlDocument;
        }
    }
}