using Crawlers.Application.Interfaces.Services;
using Crawlers.Application.Interfaces.Services.Async;
using Crawlers.Application.Services.Async.Supporters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Crawlers.Application.Services.Async
{
    public class WebCrawlerAppAsyncService : IWebCrawlerAppAsyncService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly int _threadCount;
       

        public WebCrawlerAppAsyncService(IServiceProvider serviceProvider, int threadCount)
        {
            
            this.serviceProvider = serviceProvider;
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

                    Console.WriteLine($"Loading WebCrawlerAppService #{counter.Counter}");
                    var service = serviceProvider.GetService<IWebCrawlerAppService>();
                    
                    while(true)
                    {
                        service.Scrap(counter.Counter);
                        cancellationToken.ThrowIfCancellationRequested();
                    }
                });
            }

            Task.WaitAll(tasks, cancellationToken);
        }
    }
}
