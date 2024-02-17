using Core.Infra.Services.Observers;
using Core.Infra.Services.Observers.Entities.Messages;
using Core.Infra.Services.Observers.Interfaces;
using Crawlers.Application.Interfaces.Services;
using Crawlers.Application.Interfaces.Services.Async;
using Crawlers.Application.Services.Async.Supporters;
using Crawlers.Domains.Entities.ObjectValues.Pages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
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

            Console.WriteLine($"App started");
            TaskFactory taskFactory = new TaskFactory();
            Task[] tasks = new Task[_threadCount];

            for(int i = 0; i < _threadCount; i++)
            {
                Console.WriteLine($"Starting task [{i}]");

                tasks[i] = taskFactory.StartNew(() => {
                    var counter = new ThreadCounter();
                    var service = _provider.GetService<IWebCrawlerFolhaAppService>();
                    while(true)
                    {
                        service.Scrap(counter.Counter);
                        cancellationToken.ThrowIfCancellationRequested();
                        
                    }
                });
                //Thread.Sleep(60000);
            }

            Task.WaitAll(tasks);
        }
    }
}
