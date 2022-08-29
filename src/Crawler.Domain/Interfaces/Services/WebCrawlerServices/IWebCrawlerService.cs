using Crawlers.Domains.Entities.ObjectValues.Urls;

namespace Crawlers.Domains.Interfaces.Services.WebCrawlerServices
{
    public interface IWebCrawlerService<T> where T : class
    {
        IList<Page> GetAnchors(Page url);
        string GetTitle(Page url);
        string GetContent(Page url);
        T GetEntity(Page url);
        string? GetMeta(Page url, string metaName);
    }
}
