using Crawlers.Application.Interfaces.Services;
using Crawlers.Application.Interfaces.Services.Async;
using Crawlers.Application.Services.Async.Supporters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Crawlers.Application.Services.Async
{
    public class WebCrawlerFolhaAppAsyncService : IWebCrawlerFolhaAppAsyncService
    {

        private readonly IServiceProvider _provider;
        private readonly int _threadCount;

        public WebCrawlerFolhaAppAsyncService(IServiceProvider provider, int threadCount)
        {
            _provider = provider;
            _threadCount = threadCount;
        }

        public void Scrap(CancellationToken cancellationToken)
        {

            TaskFactory taskFactory = new TaskFactory();

            Task[] tasks = new Task[_threadCount];

            for(int i = 0; i < _threadCount; i++)
            {
                tasks[i] = taskFactory.StartNew(() => {
                    var counter = new ThreadCounter();
                    var service = _provider.GetService<IWebCrawlerFolhaAppService>();
                    while(true)
                    {
                        service.Scrap(counter.Counter);
                        cancellationToken.ThrowIfCancellationRequested();
                    }
                });
            }

            Task.WaitAll(tasks);
        }
    }
}
