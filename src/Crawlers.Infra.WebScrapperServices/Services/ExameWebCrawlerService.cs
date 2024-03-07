using Core.Infra.CrossCutting.Interfaces.Services.Configs.Managers;
using Core.Infra.Services.Observers.Interfaces;
using Crawlers.Domains.Entities.Articles;
using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Domains.Interfaces.Services.WebCrawlerServices;
using Crawlers.Infra.WebScrapperServices.Interfaces.InnerServices;
using System.Text;

namespace Crawlers.Infra.WebScrapperServices.Services
{
    public class ExameWebCrawlerService : WebCrawlerService, IExameWebCrawlerService
    {
        public ExameWebCrawlerService(IWebNavigator webNavigator, IEventManager eventManager, IConfigsManager configsManager) : base(webNavigator, eventManager, configsManager)
        {
        }

        public override string? GetContent(Page url)
        {
            var doc = GetDocument(url);
            var divs = doc.DocumentNode.SelectNodes("//div");
            if (divs == null)
                return string.Empty;

            var nodes = divs.Where(d => d.Id == "news-body").ToList();
            var result = new StringBuilder();

            foreach (var node in nodes)
            {
                result.Append(node.InnerText);
            }

            return Decode(result.ToString());
        }

        public override Article GetEntity(Page page)
        {
            return new Article(GetTitle(page), GetContent(page), page, GetPublishDate(page));
        }

        public DateTime? GetPublishDate(Page url)
        {
            var strDate = GetMeta(url, "article:published_time");
            if (strDate == null)
                return null;
            return DateTime.Parse(strDate);
        }
    }
}
