using Crawlers.Application.Interfaces.Services;
using Crawlers.Domain.Interfaces.DAL;
using Crawlers.Domain.Interfaces.Services.WebCrawlerServices;

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
                var currentDomain = url.Domain;
                var folha = FolhaWebCrawlerService.GetEntity(url);
                // TODO não está puxando as urls para o domínio atual. 
                // TODO precisa criar uma regra para validar o domínio da url e ver se casa com boa parte do domínio atual
                var newUrls = folha.GetValidUrls(currentDomain);
                SaveNewUrls(newUrls);
            }
        }

        private void SaveNewUrls(System.Collections.Generic.IEnumerable<Domain.Entities.ObjectValues.Urls.Url> newUrls)
        {
            foreach(var url in newUrls)
            {
                UnitOfWork.UrlRepository.Add(url);
            }

            UnitOfWork.Save();
        }
    }
}