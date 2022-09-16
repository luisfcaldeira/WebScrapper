using Core.Infra.Services.Observers.Entities.Messages;
using Core.Infra.Services.Observers.Interfaces;
using Crawlers.Application.InnerServices.Observers.Listeners;
using Crawlers.Application.Interfaces.Services;
using Crawlers.Domains.Entities.Articles;
using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Domains.Interfaces.DAL;
using Crawlers.Domains.Interfaces.Services.WebCrawlerServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Crawlers.Application.Services
{
    public class WebCrawlerFolhaAppService : IWebCrawlerFolhaAppService
    {
        public WebCrawlerFolhaAppService(IUnitOfWork unitOfWork, IFolhaWebCrawlerService folhaWebCrawlerService, IEventManager eventManager)
        {
            UnitOfWork = unitOfWork;
            FolhaWebCrawlerService = folhaWebCrawlerService;
            this.eventManager = eventManager;
        }

        private IUnitOfWork UnitOfWork;
        private IFolhaWebCrawlerService FolhaWebCrawlerService;
        private readonly IEventManager eventManager;

        public async Task Scrap(CancellationToken cancellationToken)
        {
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    eventManager.Notify(new Message(Tag.LogMessage, "stopped"));
                    return;
                }
                ScrapOneIfExists();
            }
        }

        private void ScrapOneIfExists()
        {
            var page = UnitOfWork.PageRepository.GetOneNotVisited();
            if (page != null)
            {
                CreateAndSaveArticle(page);
            }
        }

        private void CreateAndSaveArticle(Page page)
        {
            try
            {
                eventManager.Notify(new Message(Tag.LogMessage, $"Accessing: '{page.RawUrl}'"));

                var newPages = FolhaWebCrawlerService.GetReferredPages(page);
                SaveNewPages(newPages);
                
                var folha = FolhaWebCrawlerService.GetEntity(page);

                if(folha.IsValid)
                    SaveArticle(folha);

            } catch(Exception ex)
            {
                page.InformError(ex);
                Debug.WriteLine(ex.Message);
            }

            page.Visit();

            UnitOfWork.Save();
        }

        private void SaveArticle(FolhaArticle folha)
        {
            if (!UnitOfWork.FolhaArticleRepository.Exists(folha))
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

        private void SaveNewPages(IEnumerable<Page> pages)
        {
            foreach(var page in pages)
            {
                if(!UnitOfWork.PageRepository.Exists(page))
                {
                    UnitOfWork.PageRepository.Add(page);
                }
            }
        }
    }
}