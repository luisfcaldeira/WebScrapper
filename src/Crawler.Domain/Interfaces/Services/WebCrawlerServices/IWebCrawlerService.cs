using Crawlers.Domain.Entities.ObjectValues.Urls;

namespace Crawlers.Domain.Interfaces.Services.WebCrawlerServices
{
    public interface IWebCrawlerService<T> where T : class
    {
        IList<Url> GetAnchors(Url url);
        string GetTitle(Url url);
        string GetContent(Url url);
        T GetEntity(Url url);
        string? GetMeta(Url url, string metaName);
    }
}
