using System.Threading;

namespace Crawlers.Application.Interfaces.Services.Async
{
    public interface IWebCrawlerFolhaAppAsyncService
    {
        void Scrap(CancellationToken cancellationToken);
    }
}
