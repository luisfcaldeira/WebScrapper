using Crawler.Infra.Databases.Context;
using Crawler.Infra.Databases.DAL;
using Crawler.Infra.Databases.DAL.Repositories;
using Crawlers.Application.Interfaces.Services;
using Crawlers.Application.Services;
using Crawlers.Domains.Entities.ObjectValues.Urls;
using Crawlers.Domains.Interfaces.DAL;
using Crawlers.Domains.Interfaces.DAL.Repositories;
using Crawlers.Domains.Interfaces.Services.WebCrawlerServices;
using Crawlers.Infra.WebScrapperServices.Services;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ConsoleApp.WebScrapper
{
    public static class Program
    {
        public static void Main()
        {

            using IHost host = Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) => 
                services.AddTransient<IUrlRepository, UrlRepository>()
                .AddTransient<DbContext>(provider => new CrawlerDbContext(@"Server=(localdb)\mssqllocaldb;Database=Test"))
                .AddTransient<IWebCrawlerFolhaAppService, WebCrawlerFolhaAppService>()
                .AddTransient<IFolhaWebCrawlerService, FolhaWebCrawlerService>()
                .AddTransient((provider) => new HtmlWeb())
                .AddTransient<IUnitOfWork, UnitOfWork>())
            .Build();

            IServiceProvider services = host.Services;

            var unitOfWork = services.GetRequiredService<IUnitOfWork>();
            var strUrl = "https://www1.folha.uol.com.br/poder/2022/08/lula-informa-ao-tse-ter-criado-redes-sociais-direcionadas-a-evangelicos.shtml";
            
            if(unitOfWork.UrlRepository.GetUrl(strUrl) == null)
            {
                unitOfWork.UrlRepository.Add(UrlCreator.Create(strUrl));
                unitOfWork.Save();
            }

            Execute(services);

            host.RunAsync();

            Console.ReadLine();
        }

        static void Execute(IServiceProvider services)
        {
            using IServiceScope serviceScope = services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            var crawler = provider.GetRequiredService<IWebCrawlerFolhaAppService>();
            crawler.Scrap();

        }
    }
}
