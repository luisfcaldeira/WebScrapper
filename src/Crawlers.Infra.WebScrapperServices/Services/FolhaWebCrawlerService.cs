using Crawlers.Domains.Entities.Articles;
using Crawlers.Domains.Entities.ObjectValues.Urls;
using Crawlers.Domains.Interfaces.Services.WebCrawlerServices;
using Crawlers.Infra.WebScrapperServices.Interfaces.InnerServices;
using System.Text;

namespace Crawlers.Infra.WebScrapperServices.Services
{
    public class FolhaWebCrawlerService : WebCrawlerService<FolhaArticle>, IFolhaWebCrawlerService
    {
        public FolhaWebCrawlerService(IWebNavigator webNavigator) : base(webNavigator)
        {
        }

        public override string GetContent(Page url)
        {
            var doc = GetDocument(url);
            var divs = doc.DocumentNode.SelectNodes("//div");
            if (divs == null)
                return String.Empty;

            var nodes = divs.Where(d => d.HasClass("c-news__body")).ToList();
            var result = new StringBuilder();
            
            foreach(var node in nodes)
            {
                result.Append(node.InnerText);  
            }

            return Decode(result.ToString());
        }

        public DateTime? GetPublishDate(Page url)
        {
            var strDate = GetMeta(url, "article:published_time");
            if (strDate == null)
                return null;
            return DateTime.Parse(strDate);
        }

        public override FolhaArticle GetEntity(Page url)
        {
            return new FolhaArticle(GetTitle(url), GetContent(url), url, GetPublishDate(url), GetReferredPages(url));
        }
    }
}
