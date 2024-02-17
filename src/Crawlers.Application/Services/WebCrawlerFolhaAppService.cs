using Core.Infra.Services.Observers.Entities.Messages;
using Core.Infra.Services.Observers.Interfaces;
using Crawlers.Application.Interfaces.Services;
using Crawlers.Domains.Entities.Articles;
using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Domains.Interfaces.DAL;
using Crawlers.Domains.Interfaces.Services.WebCrawlerServices;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var pages = UnitOfWork.PageRepository.GetNonVisited(10).ToList();
            if (!pages.Any())
                return;

            TakeThem(pages, taskCode);

            pages.ForEach(page =>
            {
                _eventManager.Notify(new Message(Tag.Evolution, $"Working on the page: '{page}'..."));

                if (page.TaskCode == taskCode)
                {
                    CreateAndSaveArticle(page, taskCode);
                }
            });

            _eventManager.Notify(new Message(Tag.LogMessage, $"[{taskCode}] - Saving in db..."));
            UnitOfWork.Save();
        }

        private void TakeThem(List<Page> pages, int taskCode)
        {
            foreach (var page in pages)
            {
                page.TaskCode = taskCode;
            }

            _eventManager.Notify(new Message(Tag.LogMessage, $"[{taskCode}] - Taking {pages.Count} record(s) from DB."));
            UnitOfWork.Save();
        }

        private void CreateAndSaveArticle(Page page, int taskCode)
        {
            try
            {
                _eventManager.Notify(new Message(Tag.LogMessage, $"[{taskCode}] - Accessing #{page.Id} url: '{page.RawUrl}' "));

                var newPages = FolhaWebCrawlerService.GetReferredPages(page);
                _eventManager.Notify(new Message(Tag.LogMessage, $"[{taskCode}] - Saving {newPages.Count} new pages "));
                SaveNewPages(newPages);
                
                _eventManager.Notify(new Message(Tag.LogMessage, $"[{taskCode}] - Trying to convert into a article document "));
                var folha = FolhaWebCrawlerService.GetEntity(page);

                if (folha.IsValid)
                {
                    _eventManager.Notify(new Message(Tag.LogMessage, $"[{taskCode}] - Document is valid, converting... "));
                    SaveArticle(folha);
                }

            } catch(Exception ex)
            {
                page.InformError(ex);
                _eventManager.Notify(new Message(Tag.Error, ex.Message));
            }

            page.Visit();

            
        }

        private void SaveNewPages(IEnumerable<Page> pages)
        {
            foreach (var page in pages)
            {
                UnitOfWork.PageRepository.Add(page);
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