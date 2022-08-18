using Crawlers.Domain.Entities.Articles;
using Crawlers.Domain.Entities.ObjectValues.Urls;

namespace Crawlers.Domain.Interfaces.Services.WebCrawlerServices
{
    public interface IFolhaWebCrawlerService : IWebCrawlerService<FolhaArticle>
    {
        DateTime? GetPublishDate(Url url);
    }
}
