using Core.Infra.IoC;
using Crawlers.Infra.Databases.Context;
using Crawlers.Infra.Databases.DAL;
using Crawlers.Infra.Databases.DAL.Repositories;
using Crawlers.Application.Interfaces.Services;
using Crawlers.Application.Services;
using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Domains.Interfaces.DAL;
using Crawlers.Domains.Interfaces.DAL.Repositories;
using Crawlers.Domains.Interfaces.Services.WebCrawlerServices;
using Crawlers.Infra.WebScrapperServices.Services;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Infra.Services.Observers.Interfaces;
using ConsoleApp.WebScrapper.InnerServices.Listeners;

namespace ConsoleApp.WebScrapper
{
    public static class Program
    {
        public static void Main()
        {

            var iocMapper = new IocMapper();

            var unitOfWork = iocMapper.GetService<IUnitOfWork>();
            var eventManager = iocMapper.GetService<IEventManager>();
            eventManager.Attach(new LogMessageEventListener());
            eventManager.Attach(new LogErrorEventListener());

            var strUrl = "https://www1.folha.uol.com.br/poder/2022/08/lula-informa-ao-tse-ter-criado-redes-sociais-direcionadas-a-evangelicos.shtml";

            if (unitOfWork.PageRepository.GetPage(strUrl) == null)
            {
                unitOfWork.PageRepository.Add(PageCreator.Create(strUrl));
                unitOfWork.Save();
            }

            var crawler = iocMapper.GetService<IWebCrawlerFolhaAppService>();
            var cancellation = new CancellationTokenSource();
            AddListenerToCancel(cancellation);
            // TODO registrar o EventManager como singleton?
            // TODO criar entidades concretas para implementar os listeners
            crawler.Scrap(cancellation.Token).Wait();

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
