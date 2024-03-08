using Core.Infra.CrossCutting.Interfaces.Services.Configs.Managers;
using Core.Infra.Services.Observers.Entities.Messages;
using Core.Infra.Services.Observers.Interfaces;
using Crawlers.Domains.Entities.Articles;
using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Domains.Entities.ObjectValues.Pages.Builders;
using Crawlers.Domains.Interfaces.Services.WebCrawlerServices;
using Crawlers.Infra.WebScrapperServices.Interfaces.InnerServices;
using HtmlAgilityPack;
using System.Text;
using System.Text.RegularExpressions;

namespace Crawlers.Infra.WebScrapperServices.Services
{
    public abstract class WebCrawlerService : IWebCrawlerService
    {
        protected IConfigsManager ConfigsManager { get; }

        protected IEventManager EventManager { get; }

        public WebCrawlerService(IWebNavigator webNavigator, IEventManager eventManager, IConfigsManager configsManager)
        {
            _webNavigator = webNavigator;
            EventManager = eventManager;
            ConfigsManager = configsManager;
        }

        private IWebNavigator _webNavigator { get; }

        public virtual IList<Page> GetReferralsPages(Page page)
        {
            HtmlDocument doc = GetDocument(page);
            var anchors = doc.DocumentNode.SelectNodes($"//a");
            var filteredAnchors = ApplyRule(anchors);

            for(var i = 0; i < filteredAnchors.Count; i++)
            {
                var a = filteredAnchors[i];
                if(!a.RawUrl.StartsWith("http"))
                {
                    filteredAnchors[i] = PageBuilder.With(page.Domain.Name, page.Domain.TopLevel)
                        .WithUrl(page.Url + "/" + a.RawUrl)
                        .WithProtocol(page.Domain.Protocol.Value)
                        .WithDirectory(a.RawUrl)
                        .WithSubdomain(page.Domain.Subdomain.Value)
                        .WithCountry(page.Domain.Country.Value)
                        .Create();
                }
            }

            return filteredAnchors;
        }

        protected IList<Page> ApplyRule(HtmlNodeCollection anchors)
        {
            if (anchors == null) 
                return new List<Page>();    

            var filtredAnchors = anchors.ToList();
            var pattern = ConfigsManager.Get("FormatUrl")?.Description;

            if(pattern != null)
            {
                var regex = new Regex(pattern);
                filtredAnchors = anchors.Where(a =>
                {
                    var result = regex.IsMatch(GetAnchorHref(a)) || !GetAnchorHref(a).StartsWith("http");
                    return result;
                }
                ).ToList();
            }

            return ConvertAnchorsIntoPages(filtredAnchors);
        }

        protected IList<Page> ConvertAnchorsIntoPages(IList<HtmlNode> anchors)
        {
            var result = new List<Page>();

            if (anchors == null)
                return result;

            foreach (var anchor in anchors)
            {
                try
                {
                    result.Add(PageCreator.Create(GetAnchorHref(anchor)));
                }
                catch (Exception ex)
                {
                    string message = $"I couldn't reconize an anchor: [{ex.Message}]";
                    EventManager.Notify(new Message(Tag.Warning, message));
                }
            }

            return result;
        }

        protected string GetAnchorHref(HtmlNode anchor)
        {
            return anchor.GetAttributeValue("href", "");
        }

        public string? GetTitle(Page page)
        {
            var doc = GetDocument(page);
            var title = doc.DocumentNode.SelectNodes("//head/title").FirstOrDefault();
            if(title == null)
                return null;

            // https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding.getencodings?view=net-6.0#system-text-encoding-getencodings
            var titleStr = title.InnerText;
            return Decode(titleStr, Encoding.GetEncoding(doc.Encoding.BodyName));
        }

        protected static string Decode(string titleStr, Encoding srcEncoding)
        {
            byte[] bytes = srcEncoding.GetBytes(titleStr);
            return Encoding.UTF8.GetString(bytes);
        }

        public string? GetMeta(Page page, string metaName)
        {
            var doc = GetDocument(page);
            var metasNodes = doc.DocumentNode.SelectNodes("//head/meta");
            if (metasNodes == null)
                return null;

            foreach(var node in metasNodes)
            {
                if (GetAttributeValue(node, "property") == metaName)
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

        public void Dispose()
        {
            _webNavigator.Dispose();
            EventManager.Dispose();
        }

        public abstract string? GetContent(Page page);

        public abstract Article GetEntity(Page page);
    }
}
