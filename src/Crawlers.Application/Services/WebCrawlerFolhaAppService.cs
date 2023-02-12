using Core.Infra.Services.Observers.Entities.Messages;
using Core.Infra.Services.Observers.Interfaces;
using Crawlers.Application.Interfaces.Services;
using Crawlers.Domains.Entities.Articles;
using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Domains.Interfaces.DAL;
using Crawlers.Domains.Interfaces.Services.WebCrawlerServices;
using System;
using System.Collections.Generic;

namespace Crawlers.Application.Services
{
    public class WebCrawlerFolhaAppService : IWebCrawlerFolhaAppService
    {
        public WebCrawlerFolhaAppService(IUnitOfWork unitOfWork, IFolhaWebCrawlerService folhaWebCrawlerService, IEventManager eventManager)
        {
            UnitOfWork = unitOfWork;
            FolhaWebCrawlerService = folhaWebCrawlerService;
            _eventManager = eventManager;
        }

        private IUnitOfWork UnitOfWork;
        private IFolhaWebCrawlerService FolhaWebCrawlerService;
        private readonly IEventManager _eventManager;

        public void Scrap(int taskCode)
        {
            ScrapOneIfExists(taskCode);
        }

        private void ScrapOneIfExists(int taskCode)
        {
            var page = UnitOfWork.PageRepository.GetOneNotVisited();
            if (page != null)
            {
                _eventManager.Notify(new Message(Tag.Evolution, "Página encontrada... "));
                TakeItToMe(page, taskCode);

                if(page.TaskCode == taskCode)
                {
                    CreateAndSaveArticle(page, taskCode);
                }
            }
        }

        private void TakeItToMe(Page page, int taskCode)
        {
            page.TaskCode = taskCode;
            UnitOfWork.Save();
        }

        private void CreateAndSaveArticle(Page page, int taskCode)
        {
            try
            {
                _eventManager.Notify(new Message(Tag.LogMessage, $"[{taskCode}] - Accessing #{page.Id} url: '{page.RawUrl}' "));

                var newPages = FolhaWebCrawlerService.GetReferredPages(page);
                SaveNewPages(newPages);
                
                var folha = FolhaWebCrawlerService.GetEntity(page);

                if(folha.IsValid)
                    SaveArticle(folha);

            } catch(Exception ex)
            {
                page.InformError(ex);
                _eventManager.Notify(new Message(Tag.Error, ex.Message));
            }

            page.Visit();

            _eventManager.Notify(new Message(Tag.LogMessage, $"[{taskCode}] - Saving in db..."));
            UnitOfWork.Save();
        }

        private void SaveNewPages(IEnumerable<Page> pages)
        {
            foreach (var page in pages)
            {
                if (!UnitOfWork.PageRepository.Exists(page))
                {
                    UnitOfWork.PageRepository.Add(page);
                }
            }
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

        public void Dispose()
        {
            UnitOfWork.Dispose();
            FolhaWebCrawlerService.Dispose();
            _eventManager.Dispose();
        }
    }
}