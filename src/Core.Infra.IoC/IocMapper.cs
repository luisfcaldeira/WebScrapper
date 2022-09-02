using Crawler.Infra.Databases.Context;
using Crawler.Infra.Databases.DAL;
using Crawler.Infra.Databases.DAL.Repositories;
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

namespace Core.Infra.IoC
{
    public class IocMapper
    {
        private IServiceProvider _services;
        private IServiceProvider _provider;
        private bool mockWebNavigator;

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
                })
                .ConfigureServices((_, services) =>
                {
                    var serviceCollection = services.AddTransient<IPageRepository, PageRepository>()
                    .AddTransient<DbContext>(provider => new CrawlerDbContext(@"Server=(localdb)\mssqllocaldb;Database=WebCrawler"))
                    .AddTransient<IWebCrawlerFolhaAppService, WebCrawlerFolhaAppService>()
                    .AddTransient<IFolhaWebCrawlerService, FolhaWebCrawlerService>()
                    .AddTransient((provider) => new HtmlWeb())
                    .AddTransient<IUnitOfWork, UnitOfWork>();

                    if(mockWebNavigator)
                    {
                        serviceCollection.AddTransient<IWebNavigator, WebNavigatorMock>();
                    } else
                    {
                        serviceCollection.AddTransient<IWebNavigator, WebNavigator>();

                    }
                })
            
            .Build();

            _services = host.Services;

            IServiceScope serviceScope = _services.CreateScope();
            _provider = serviceScope.ServiceProvider;

            host.RunAsync();
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
