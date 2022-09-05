using Crawlers.Application.Interfaces.Services;
using Crawlers.Domains.Entities.Articles;
using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Domains.Interfaces.DAL;
using Crawlers.Domains.Interfaces.Services.WebCrawlerServices;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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

        public async Task Scrap(CancellationToken cancellationToken)
        {
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine("stopped");
                    return;
                }
                ScrapOneIfExists();
            }
        }

        private void ScrapOneIfExists()
        {
            var url = UnitOfWork.PageRepository.GetOneNotVisited();
            if (url != null)
            {
                var currentDomain = url.Domain;
                var folha = FolhaWebCrawlerService.GetEntity(url);
                SaveArticle(folha);
                var newPages = folha.GetValidPages(currentDomain);
                SaveNewPages(newPages);
                url.Visit();
                UnitOfWork.Save();
            }
        }


        private void SaveNewPages(IEnumerable<Page> pages)
        {
            foreach(var page in pages)
            {
                if(!UnitOfWork.PageRepository.Exists(page))
                {
                    UnitOfWork.PageRepository.Add(page);
                } else
                {
                    var pageDb = UnitOfWork.PageRepository.GetPage(page.Url);
                    pageDb.Update(page);
                    UnitOfWork.PageRepository.Update(pageDb);
                }
            }
        }

        private void SaveArticle(FolhaArticle folha)
        {
            if(!UnitOfWork.FolhaArticleRepository.Exists(folha))
            {
                UnitOfWork.FolhaArticleRepository.Add(folha);
            }
            else
            {
                FolhaArticle folhaDb = UnitOfWork.FolhaArticleRepository.GetArticle(folha.Page);
                folhaDb.Update(folha);
                UnitOfWork.FolhaArticleRepository.Update(folhaDb);
            }
        }
    }
}