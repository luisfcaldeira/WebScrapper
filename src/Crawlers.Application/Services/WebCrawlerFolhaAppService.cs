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
                // TODO criar um construtor para domain poder incluir as propriedades separadas conforme a regra que capturou a URL
                // TODO precisa criar uma regra para validar o domínio da url e ver se casa com boa parte do domínio atual
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