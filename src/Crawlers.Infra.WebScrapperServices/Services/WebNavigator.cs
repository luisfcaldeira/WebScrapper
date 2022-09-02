using Crawlers.Domains.Entities.ObjectValues.Urls;
using Crawlers.Infra.WebScrapperServices.Interfaces.InnerServices;
using HtmlAgilityPack;

namespace Crawlers.Infra.WebScrapperServices.Services
{
    public class WebNavigator : IWebNavigator
    {
        public HtmlWeb htmlWeb;

        public WebNavigator(HtmlWeb htmlWeb)
        {
            this.htmlWeb = htmlWeb;
        }

        public HtmlDocument GetDocument(Page page)
        {
            return htmlWeb.Load(page.Url);
        }
    }
}
