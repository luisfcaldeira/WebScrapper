using Crawlers.Infra.Databases.DAL.Repositories;
using Crawlers.Domains.Interfaces.DAL;
using Crawlers.Domains.Interfaces.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Infra.Databases.Context;
using System.Data;

namespace Crawlers.Infra.Databases.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private IPageRepository _pageRepository;
        private IFolhaArticleRepository _folhaArticleRepository;

        public IPageRepository PageRepository 
        { 
            get
            {
                if (_pageRepository == null)
                    _pageRepository = new PageRepository(DbContext);

                return _pageRepository;
            }
        }

        public IFolhaArticleRepository FolhaArticleRepository
        {
            get
            {
                if(_folhaArticleRepository == null)
                    _folhaArticleRepository = new FolhaArticleRepository(DbContext);

                return _folhaArticleRepository;
            }
        }

        public CrawlerDbContext DbContext { get; }

        public UnitOfWork(CrawlerDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Save()
        {
            try
            {
                using (var trans = DbContext.Database.BeginTransaction())
                {
                    DbContext.SaveChanges();
                    trans.Commit();
                }
            } 
            catch(DbUpdateConcurrencyException e)
            {
                foreach (var entry in e.Entries)
                {
                    if (entry.Entity is Page)
                    {
                        var databaseValues = entry.GetDatabaseValues();

                        entry.CurrentValues.SetValues(databaseValues);
                    }
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
