using Crawlers.Domain.Entities.Articles;
using Crawlers.Domain.Entities.ObjectValues.Urls;
using Crawlers.Domain.Interfaces.Services.WebCrawlerServices;
using HtmlAgilityPack;
using System.Text;

namespace Crawlers.Infra.WebScrapperServices.Services
{
    public class FolhaWebCrawlerService : WebCrawlerService<FolhaArticle>, IFolhaWebCrawlerService
    {
        public FolhaWebCrawlerService(HtmlWeb web) : base(web)
        {
        }

        public override string GetContent(Url url)
        {
            var doc = GetDocument(url);
            var divs = doc.DocumentNode.SelectNodes("//div");

            var nodes = divs.Where(d => d.HasClass("c-news__body")).ToList();
            var result = new StringBuilder();
            
            foreach(var node in nodes)
            {
                result.Append(node.InnerText);  
            }

            return Decode(result.ToString());
        }

        public DateTime? GetPublishDate(Url url)
        {
            var strDate = GetMeta(url, "article:published_time");
            if (strDate == null)
                return null;
            return DateTime.Parse(strDate);
        }

        public override FolhaArticle GetEntity(Url url)
        {
            return new FolhaArticle(GetTitle(url), GetContent(url), url, GetPublishDate(url), GetAnchors(url));
        }
    }
}
