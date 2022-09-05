using Crawlers.Domains.Entities.Articles;
using Crawlers.Domains.Entities.ObjectValues.Pages;

namespace Crawlers.Domains.Interfaces.Services.WebCrawlerServices
{
    public interface IFolhaWebCrawlerService : IWebCrawlerService<FolhaArticle>
    {
        DateTime? GetPublishDate(Page url);
    }
}
