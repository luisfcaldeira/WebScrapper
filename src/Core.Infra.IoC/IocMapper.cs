using Crawlers.Infra.Databases.Context;
using Crawlers.Infra.Databases.DAL;
using Crawlers.Infra.Databases.DAL.Repositories;
using Crawlers.Application.Interfaces.Services;
using Crawlers.Application.Services;
using Crawlers.Domains.Interfaces.DAL;
using Crawlers.Domains.Interfaces.DAL.Repositories;
using Crawlers.Domains.Interfaces.Services.WebCrawlerServices;
using Crawlers.Infra.WebScrapperServices.Interfaces.InnerServices;
using Crawlers.Infra.WebScrapperServices.Services;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mockers.Contexts.Crawlers.Infra.Services;
using Core.Infra.Services.Observers.Interfaces;
using Core.Infra.Services.Observers;
using Crawlers.Application.Services.Async;
using Crawlers.Application.Interfaces.Services.Async;
using Microsoft.Extensions.Logging;
using Core.Infra.CrossCutting.Interfaces.Services.Configs.Managers;
using Core.Infra.CrossCutting.Services.Configs.Managers;

namespace Core.Infra.IoC
{
    public class IocMapper
    {
        private IServiceProvider _services;
        private IServiceProvider _provider;
        private bool mockWebNavigator;
        private int totalThreads;
        private int totalPackage;
        private string site;

        public IocMapper()
        {
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostingContext, configuration) =>
                {
                    configuration.Sources.Clear();

                    IHostEnvironment env = hostingContext.HostingEnvironment;

                    configuration
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                    IConfigurationRoot configurationRoot = configuration.Build();
                    mockWebNavigator = configurationRoot.GetValue<bool>("MockWebNavigator");
                    totalThreads = configurationRoot.GetValue<int>("TotalThreads");
                    totalPackage = configurationRoot.GetValue<int>("TotalPackage");
                    site = configurationRoot.GetValue<string>("Site");

                })
                .ConfigureServices((_, services) =>
                {
                    var serviceCollection =

                    services
                    .AddDbContext<CrawlerDbContext>(provider =>
                        {
                            //provider.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=WebCrawler");
                            provider.UseSqlServer(@"Server=DESKTOP-L0UN16O;Database=WebCrawlerExame;User Id=acatc2;Password=acatc2;");
                        }, ServiceLifetime.Transient)

                    .AddTransient<IPageRepository, PageRepository>()
                    
                    .AddTransient<IWebCrawlerAppAsyncService>((serviceProvider) =>
                    {
                        return new WebCrawlerAppAsyncService(serviceProvider, totalThreads);
                    })
                    
                    .AddTransient<HtmlWeb>()
                    .AddTransient<IUnitOfWork, UnitOfWork>()
                    .AddTransient<IEventManager, EventManager>()
                    .AddSingleton<IConfigsManager, ConfigsManager>()
                    .AddTransient<IWebCrawlerAppService, WebCrawlerAppService>((serviceProvider) =>
                    {
                        return new WebCrawlerAppService(serviceProvider.GetService<IUnitOfWork>(), serviceProvider.GetService<IWebCrawlerService>(), serviceProvider.GetService<IEventManager>(), totalPackage);
                    }); 

                    switch(site)
                    {
                        case "Folha":
                            services.AddTransient<IWebCrawlerService, FolhaWebCrawlerService>();
                            break;
                        case "Exame":
                            services.AddTransient<IWebCrawlerService, ExameWebCrawlerService>();
                            break;

                        default:
                            throw new Exception("Site option wasn't implemented");
                    }

                    if(mockWebNavigator)
                    {
                        serviceCollection.AddTransient<IWebNavigator, WebNavigatorMock>();
                    } else
                    {
                        serviceCollection.AddTransient<IWebNavigator, WebNavigator>();

                    }
                })
            .ConfigureLogging((context, logging) => {
                var env = context.HostingEnvironment;
                var config = context.Configuration.GetSection("Logging");
                logging.SetMinimumLevel(LogLevel.None);
                logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.None);
            })
            .Build();
            host.RunAsync();

            _services = host.Services;
            _provider = _services.CreateScope().ServiceProvider;

        }

        public T? Get<T>()
        {
            return _services.GetService<T>();
        }

        public T? GetService<T>()
        {
            return _provider.GetService<T>();
        }
    }
}
