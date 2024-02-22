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
        public WebCrawlerFolhaAppService(IUnitOfWork unitOfWork, IFolhaWebCrawlerService folhaWebCrawlerService, IEventManager eventManager, int totalPackage)
        {
            UnitOfWork = unitOfWork;
            FolhaWebCrawlerService = folhaWebCrawlerService;
            _eventManager = eventManager;
            _totalPackage = totalPackage;
        }

        private readonly IUnitOfWork UnitOfWork;
        private readonly IFolhaWebCrawlerService FolhaWebCrawlerService;
        private readonly IEventManager _eventManager;
        private readonly int _totalPackage;

        public void Scrap(int taskCode)
        {
            scrap(taskCode);
        }

        private void scrap(int taskCode)
        {
            var pages = UnitOfWork.PageRepository.GetNonVisited(_totalPackage).ToList();
            TakeThem(pages, taskCode);

            foreach(var page in pages.Where(p => p.TaskCode == taskCode))
            {
                //_eventManager.Notify(new Message(Tag.Evolution, $"Working on the page: '{page}'..."));

                if (page.TaskCode == taskCode)
                {
                    CreateAndSaveArticle(page, taskCode);
                }
            }

            if(pages.Any())
            {
                _eventManager.Notify(new Message(Tag.LogMessage, $"[{taskCode}] - Saving in db..."));
                UnitOfWork.Save();
            }
        }

        private void TakeThem(List<Page> pages, int taskCode)
        {
            foreach (var page in pages)
            {
                page.TaskCode = taskCode;
                UnitOfWork.PageRepository.Update(page);
                UnitOfWork.Save();
                UnitOfWork.PageRepository.Detach(page);
            }
            if(pages.Any())
            {
                //_eventManager.Notify(new Message(Tag.LogMessage, $"[{taskCode}] - It was took {pages.Count} record(s) from DB."));
            }
        }

        private void CreateAndSaveArticle(Page page, int taskCode)
        {
            try
            {
                _eventManager.Notify(new Message(Tag.LogMessage, $"[{taskCode}] - Accessing #{page.Id} url: '{page.RawUrl}' and getting referrals"));

                var newPages = FolhaWebCrawlerService.GetReferralsPages(page);
                InsertNewPages(taskCode, newPages);

                //_eventManager.Notify(new Message(Tag.LogMessage, $"[{taskCode}] - Trying to convert into an article document "));
                var folha = FolhaWebCrawlerService.GetEntity(page);

                UnitOfWork.FolhaArticleRepository.Add(folha);
                UnitOfWork.Save();
                UnitOfWork.FolhaArticleRepository.Detach(folha);

            }
            catch (Exception ex)
            {
                page.InformError(ex);
                _eventManager.Notify(new Message(Tag.Error, ex.Message));
            }

            page.Visit();
            UnitOfWork.PageRepository.Update(page);
            UnitOfWork.Save();
            UnitOfWork.PageRepository.Detach(page);
        }

        private void InsertNewPages(int taskCode, IList<Page> newPages)
        {
            //_eventManager.Notify(new Message(Tag.LogMessage, $"[{taskCode}] - Saving {newPages.Count} new pages "));

            foreach (var p in newPages)
            {
                UnitOfWork.PageRepository.Insert(p);
                UnitOfWork.Save();
                UnitOfWork.PageRepository.Detach(p);
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