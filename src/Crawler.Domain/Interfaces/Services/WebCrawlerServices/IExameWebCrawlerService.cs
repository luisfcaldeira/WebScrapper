using Crawlers.Domains.Entities.Articles;
using Crawlers.Domains.Entities.ObjectValues.Pages;

namespace Crawlers.Domains.Interfaces.Services.WebCrawlerServices
{
    public interface IExameWebCrawlerService : IWebCrawlerService
    {
        DateTime? GetPublishDate(Page url);
    }
}
