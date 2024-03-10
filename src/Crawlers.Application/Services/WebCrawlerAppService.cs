using Core.Infra.Services.Observers;
using Core.Infra.Services.Observers.Entities.Messages;
using Core.Infra.Services.Observers.Interfaces;
using Crawlers.Application.Interfaces.Services;
using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Domains.Interfaces.DAL;
using Crawlers.Domains.Interfaces.Services.WebCrawlerServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crawlers.Application.Services
{
    public class WebCrawlerAppService : IWebCrawlerAppService
    {
        public WebCrawlerAppService(IUnitOfWork unitOfWork, IWebCrawlerService WebCrawlerService, IEventManager eventManager, int totalPackage)
        {
            UnitOfWork = unitOfWork;
            this.WebCrawlerService = WebCrawlerService;
            _eventManager = eventManager;
            _totalPackage = totalPackage;
        }

        private readonly IUnitOfWork UnitOfWork;
        private readonly IWebCrawlerService WebCrawlerService;
        private readonly IEventManager _eventManager;
        private readonly int _totalPackage;

        public void Scrap(int taskCode)
        {
            scrap(taskCode);
        }

        private void scrap(int taskCode)
        {
            _eventManager.Notify(new Message(Tag.LogMessage, $"[{taskCode}] - Getting non-visited websites"));

            var pages = UnitOfWork.PageRepository.GetNonVisited(_totalPackage).ToList();
            TakeThem(pages, taskCode);

            _eventManager.Notify(new Message(Tag.LogMessage, $"[{taskCode}] - Getting websites of #{taskCode} thread"));
            var pagesToVisit = UnitOfWork.PageRepository.GetNonVisitedRegisteredForTask(taskCode);

            _eventManager.Notify(new Message(Tag.LogMessage, $"[{taskCode}] - Iterating websites"));
            foreach (var page in pagesToVisit)
            {
                CreateAndSaveArticle(page, taskCode);
            }

            if(pages.Any())
            {
                _eventManager.Notify(new Message(Tag.LogMessage, $"[{taskCode}] - Saving in db..."));
                UnitOfWork.Save();
            }
        }

        private void TakeThem(List<Page> pages, int taskCode)
        {
            _eventManager.Notify(new Message(Tag.LogMessage, $"[{taskCode}] - Set owner for non-visited websites"));
            foreach (var page in pages)
            {
                try
                {
                    page.TaskCode = taskCode;
                    try
                    {
                        UnitOfWork.PageRepository.Update(page);

                    }
                    catch (Exception ex)
                    {
                        if (!ex.Message.Contains("This page was taken by other task"))
                        {
                            throw;
                        }
                    }
                } catch (Exception) { }
            }
            if(pages.Any())
            {
                _eventManager.Notify(new Message(Tag.LogMessage, $"[{taskCode}] - It was took {pages.Count} record(s) from DB."));
            }
        }

        private void CreateAndSaveArticle(Page page, int taskCode)
        {
            try
            {
                _eventManager.Notify(new Message(Tag.LogMessage, $"[{taskCode}] - Accessing #{page.Id} url: '{page.RawUrl}' and getting referrals"));

                var newPages = WebCrawlerService.GetReferralsPages(page);
                InsertNewPages(newPages);

                //_eventManager.Notify(new Message(Tag.LogMessage, $"[{taskCode}] - Trying to convert into an article document "));
                var article = WebCrawlerService.GetEntity(page);

                UnitOfWork.ArticleRepository.Insert(article);

            }
            catch (Exception ex)
            {
                page.InformError(ex);
                _eventManager.Notify(new Message(Tag.Error, ex.Message));
            }

            page.Visit();

            try
            {
                UnitOfWork.PageRepository.Update(page);

            } catch (Exception ex)
            {
                if(!ex.Message.Contains("This page was taken by other task"))
                {
                    throw;
                }
            }
        }

        private void InsertNewPages(IList<Page> newPages)
        {
            foreach (var p in newPages)
            {
                UnitOfWork.PageRepository.Insert(p);
            }
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
            WebCrawlerService.Dispose();
            _eventManager.Dispose();
        }
    }
}