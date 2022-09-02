using Crawlers.Application.Interfaces.Services;
using Crawlers.Domains.Entities.ObjectValues.Urls;
using Crawlers.Domains.Interfaces.DAL;
using Crawlers.Domains.Interfaces.Services.WebCrawlerServices;
using System.Collections.Generic;

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
            var urls = UnitOfWork.PageRepository.GetAllNotVisited();
            foreach (var url in urls)
            {
                var currentDomain = url.Domain;
                var folha = FolhaWebCrawlerService.GetEntity(url);
                var newUrls = folha.GetValidUrls(currentDomain);
                SaveNewUrls(newUrls);
            }
        }

        private void SaveNewUrls(IEnumerable<Page> pages)
        {
            foreach(var page in pages)
            {
                UnitOfWork.PageRepository.Add(page);
            }

            UnitOfWork.Save();
        }
    }
}