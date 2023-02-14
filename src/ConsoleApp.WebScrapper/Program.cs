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
            var iocMapper = new IocMapper();

            var unitOfWork = iocMapper.Get<IUnitOfWork>();
            var eventManager = iocMapper.Get<IEventManager>();
            eventManager.Attach(new LogMessageEventListener());
            eventManager.Attach(new LogErrorEventListener());

            var strUrl = "https://www1.folha.uol.com.br/poder/2022/08/lula-informa-ao-tse-ter-criado-redes-sociais-direcionadas-a-evangelicos.shtml";

            if (unitOfWork.PageRepository.GetPage(strUrl) == null)
            {
                unitOfWork.PageRepository.Add(PageCreator.Create(strUrl));
                unitOfWork.Save();
            }

            var crawler = iocMapper.Get<IWebCrawlerFolhaAppAsyncService>();
            var configsManager = iocMapper.Get<IConfigsManager>();

            configsManager.Add(new FormatUrlConfiguration(@"^[htps]{4,5}.+\/20[1-2][0-9]\/\d{2}[\/0-9\-a-z]+\.shtml$"));

            var cancellation = new CancellationTokenSource();

            AddListenerToCancel(cancellation);

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
