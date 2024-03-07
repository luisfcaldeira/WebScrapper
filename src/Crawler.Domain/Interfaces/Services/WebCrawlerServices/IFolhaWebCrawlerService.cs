using Crawlers.Domains.Entities.ObjectValues.Pages;

namespace Crawlers.Domains.Interfaces.Services.WebCrawlerServices
{
    public interface IFolhaWebCrawlerService : IWebCrawlerService
    {
        DateTime? GetPublishDate(Page url);
    }
}
