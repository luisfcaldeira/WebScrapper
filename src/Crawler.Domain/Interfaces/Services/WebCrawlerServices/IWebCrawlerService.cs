using Crawlers.Domains.Entities.ObjectValues.Pages;

namespace Crawlers.Domains.Interfaces.Services.WebCrawlerServices
{
    public interface IWebCrawlerService<T> : IDisposable where T : class 
    {
        IList<Page> GetReferralsPages(Page url);
        string? GetTitle(Page url);
        string? GetContent(Page url);
        T? GetEntity(Page url);
        string? GetMeta(Page url, string metaName);
    }
}
