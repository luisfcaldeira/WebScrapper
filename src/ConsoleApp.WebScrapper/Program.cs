using ConsoleApp.WebScrapper.InnerServices.Listeners;
using Core.Infra.CrossCutting.Interfaces.Services.Configs.Managers;
using Core.Infra.CrossCutting.Services.Configs;
using Core.Infra.IoC;
using Core.Infra.Services.Observers.Interfaces;
using Crawlers.Application.Interfaces.Services.Async;
using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Domains.Interfaces.DAL;
using System;
using System.Threading;

namespace ConsoleApp.WebScrapper
{
    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello");
            Console.WriteLine("Collecting dependences");

            var iocMapper = new IocMapper();

            Console.WriteLine("Loading dependences");

            var unitOfWork = iocMapper.Get<IUnitOfWork>();
            var eventManager = iocMapper.Get<IEventManager>();
            eventManager.Attach(new LogMessageEventListener());
            eventManager.Attach(new LogErrorEventListener());

            //var strUrl = "https://www.folha.uol.com.br/";
            var strUrl = "https://www.exame.com/";

            Console.WriteLine($"Loading website {strUrl}");

            unitOfWork.PageRepository.Insert(PageCreator.Create(strUrl));

            Console.WriteLine("Loading crawler");
            var crawler = iocMapper.Get<IWebCrawlerAppAsyncService>();
            Console.WriteLine("Loading configurations");
            var configsManager = iocMapper.Get<IConfigsManager>();

            Console.WriteLine("Seting configuration");
            //configsManager.Add(new FormatUrlConfiguration(@"^[htps]{4,5}.+\/20[1-2][0-9]\/\d{2}[\/0-9\-a-z]+\.shtml$"));
            configsManager.Add(new FormatUrlConfiguration(@"^[htps]{4,5}:\/\/[w\.]{0,4}exame\.com.+"));

            var cancellation = new CancellationTokenSource();

            AddListenerToCancel(cancellation);


            Console.WriteLine("Starting app");
            crawler.Scrap(cancellation.Token);

            Console.WriteLine("Finished. Press any key to quit");
            Console.ReadKey();
        }

        private static void AddListenerToCancel(CancellationTokenSource cancellation)
        {
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                cancellation.Cancel();
                eventArgs.Cancel = true;
            };
        }
    }
}
