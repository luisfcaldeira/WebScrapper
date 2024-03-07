using Crawlers.Domains.Entities.Articles;
using Crawlers.Domains.Entities.ObjectValues.Pages;

namespace Crawlers.Domains.Interfaces.Services.WebCrawlerServices
{
    public interface IWebCrawlerService : IDisposable 
    {
        IList<Page> GetReferralsPages(Page url);
        string? GetTitle(Page url);
        string? GetContent(Page url);
        Article GetEntity(Page url);
        string? GetMeta(Page url, string metaName);
    }
}
