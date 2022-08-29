using Crawlers.Domains.Entities.ObjectValues.Urls;
using Crawlers.Domains.Interfaces.Services.WebCrawlerServices;
using HtmlAgilityPack;
using System.Text;

namespace Crawlers.Infra.WebScrapperServices.Services
{
    public abstract class WebCrawlerService<T> : IWebCrawlerService<T> where T : class
    {

        public WebCrawlerService(HtmlWeb web)
        {
            Web = web;
        }

        public HtmlWeb Web { get; }

        public IList<Page> GetAnchors(Page url)
        {
            HtmlDocument doc = GetDocument(url);
            var anchors = doc.DocumentNode.SelectNodes("//a");
            var result = new List<Page>();
            foreach (var anchor in anchors)
            {
                try
                {
                    result.Add(PageCreator.Create(anchor.GetAttributeValue("href", "")));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"I couldn't record an anchor: [{ex.Message}]");
                }
            }

            return result;
        }

        public string GetTitle(Page url)
        {
            var doc = GetDocument(url);
            var title = doc.DocumentNode.SelectNodes("//head/title").First();
            // https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding.getencodings?view=net-6.0#system-text-encoding-getencodings
            var titleStr = title.InnerText;
            return Decode(titleStr);
        }

        protected static string Decode(string titleStr)
        {
            byte[] bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(titleStr);
            return Encoding.UTF8.GetString(bytes);
        }

        public string? GetMeta(Page url, string metaName)
        {
            var doc = GetDocument(url);
            var metasNodes = doc.DocumentNode.SelectNodes("//head/meta");
            return metasNodes.Where(meta => meta.Name == metaName).Select(meta => meta.InnerHtml).FirstOrDefault();

        }

        protected HtmlDocument GetDocument(Page url)
        {
            return Web.Load(url.Url);
        }

        public abstract string GetContent(Page url);

        public abstract T GetEntity(Page url);
    }
}
