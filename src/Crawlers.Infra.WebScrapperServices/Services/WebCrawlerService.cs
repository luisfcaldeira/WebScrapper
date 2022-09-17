using Core.Infra.Services.Observers.Entities.Messages;
using Core.Infra.Services.Observers.Interfaces;
using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Domains.Interfaces.Services.WebCrawlerServices;
using Crawlers.Infra.WebScrapperServices.Interfaces.InnerServices;
using HtmlAgilityPack;
using System.Text;

namespace Crawlers.Infra.WebScrapperServices.Services
{
    public abstract class WebCrawlerService<T> : IWebCrawlerService<T> where T : class
    {
        protected IEventManager EventManager { get; }

        public WebCrawlerService(IWebNavigator webNavigator, IEventManager eventManager)
        {
            _webNavigator = webNavigator;
            EventManager = eventManager;
        }

        private IWebNavigator _webNavigator { get; }

        public IList<Page> GetReferredPages(Page url)
        {
            HtmlDocument doc = GetDocument(url);
            var anchors = doc.DocumentNode.SelectNodes("//a");
            var result = new List<Page>();
            if(anchors == null)
                return result;
            foreach (var anchor in anchors)
            {
                try
                {
                    result.Add(PageCreator.Create(anchor.GetAttributeValue("href", "")));
                }
                catch (Exception ex)
                {
                    string message = $"I couldn't reconize an anchor: [{ex.Message}]";
                    EventManager.Notify(new Message(Tag.Warning, message));
                }
            }

            return result;
        }

        public string? GetTitle(Page url)
        {
            var doc = GetDocument(url);
            var title = doc.DocumentNode.SelectNodes("//head/title").First();
            if(title == null)
                return null;

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
            if (metasNodes == null)
                return null;

            foreach(var node in metasNodes)
            {
                if (GetAttributeValue(node, "name") == metaName)
                    return GetAttributeValue(node, "content");
            }

            return null;
        }

        private static string? GetAttributeValue(HtmlNode node, string name)
        {
            return GetAttribute(node, name)?.Value;
        }

        private static HtmlAttribute? GetAttribute(HtmlNode node, string name)
        {
            var attributes = node.GetAttributes();
            return attributes.Where(a => a.Name == name).FirstOrDefault();
        }

        protected HtmlDocument GetDocument(Page page)
        {
            return _webNavigator.GetDocument(page);
        }

        public abstract string? GetContent(Page url);

        public abstract T? GetEntity(Page url);
    }
}
