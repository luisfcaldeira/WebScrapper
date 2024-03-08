using Core.Infra.CrossCutting.Interfaces.Services.Configs.Managers;
using Core.Infra.Services.Observers.Interfaces;
using Crawlers.Domains.Entities.Articles;
using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Domains.Interfaces.Services.WebCrawlerServices;
using Crawlers.Infra.WebScrapperServices.Interfaces.InnerServices;
using HtmlAgilityPack;
using System.Text;

namespace Crawlers.Infra.WebScrapperServices.Services
{
    public class FolhaWebCrawlerService : WebCrawlerService, IFolhaWebCrawlerService
    {
        public FolhaWebCrawlerService(IWebNavigator webNavigator, IEventManager eventManager, IConfigsManager configsManager) : base(webNavigator, eventManager, configsManager)
        {
        }

        public override string? GetContent(Page url)
        {
            var doc = GetDocument(url);
            var divs = doc.DocumentNode.SelectNodes("//div");
            if (divs == null)
                return string.Empty;

            var nodes = divs.Where(d => d.HasClass("c-news__body")).ToList();
            var result = new StringBuilder();
            
            foreach(var node in nodes)
            {
                result.Append(node.InnerText);  
            }

            return Decode(result.ToString(), Encoding.GetEncoding(doc.Encoding.BodyName));
        }

        public DateTime? GetPublishDate(Page url)
        {
            var strDate = GetMeta(url, "article:published_time");
            if (strDate == null)
                return null;
            return DateTime.Parse(strDate);
        }

        public override Article GetEntity(Page page)
        {
            return new Article(GetTitle(page), GetContent(page), page, GetPublishDate(page));
        }

        public override IList<Page> GetReferralsPages(Page page)
        {
            HtmlDocument doc = GetDocument(page);
            
            var anchors = doc.DocumentNode.SelectNodes($"//div[contains(@class, 'c-news__content')]//a");

            if (anchors == null || anchors.Count == 0 )
            {
                anchors = doc.DocumentNode.SelectNodes($"//main//a");
            }

            if(anchors != null && anchors.Count > 0)
            {
                return ApplyRule(anchors);
            } 

            return new List<Page>();
        }
    }
}
