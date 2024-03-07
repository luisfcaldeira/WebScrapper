using System.Threading;

namespace Crawlers.Application.Interfaces.Services.Async
{
    public interface IWebCrawlerAppAsyncService
    {
        void Scrap(CancellationToken cancellationToken);
    }
}
