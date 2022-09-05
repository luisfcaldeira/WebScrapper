using System.Threading;
using System.Threading.Tasks;

namespace Crawlers.Application.Interfaces.Services
{
    public interface IWebCrawlerFolhaAppService
    {
        Task Scrap(CancellationToken cancellationToken);
    }
}
