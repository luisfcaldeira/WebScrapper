using System;

namespace Crawlers.Application.Interfaces.Services
{
    public interface IWebCrawlerAppService : IDisposable
    {
        void Scrap(int taskCode);
    }
}
