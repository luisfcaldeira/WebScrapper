using System;

namespace Crawlers.Application.Interfaces.Services
{
    public interface IWebCrawlerFolhaAppService : IDisposable
    {
        void Scrap(int taskCode);
    }
}
