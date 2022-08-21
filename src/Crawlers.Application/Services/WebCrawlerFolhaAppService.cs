using Crawlers.Application.Interfaces.Services;
using Crawlers.Domain.Interfaces.DAL;
using Crawlers.Domain.Interfaces.Services.WebCrawlerServices;
using System;
using System.Diagnostics;

namespace Crawlers.Application.Services
{
    public class WebCrawlerFolhaAppService : IWebCrawlerFolhaAppService
    {
        public WebCrawlerFolhaAppService(IUnitOfWork unitOfWork, IFolhaWebCrawlerService folhaWebCrawlerService)
        {
            UnitOfWork = unitOfWork;
            FolhaWebCrawlerService = folhaWebCrawlerService;
        }

        private IUnitOfWork UnitOfWork;
        private IFolhaWebCrawlerService FolhaWebCrawlerService;

        public void Scrap()
        {
            var urls = UnitOfWork.UrlRepository.GetAllNotVisited();
            foreach (var url in urls)
            {
                var folha = FolhaWebCrawlerService.GetEntity(url);
                Console.WriteLine(folha.Title);
                Console.WriteLine(folha.Content);
            }
        }
    }
}
