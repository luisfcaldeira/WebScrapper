using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Domains.Interfaces.DAL;
using Crawlers.Domains.Interfaces.DAL.Repositories;
using Crawlers.Infra.Databases.Context;
using Crawlers.Infra.Databases.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
                WaitTransaction();

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

        private void WaitTransaction()
        {
            while (DbContext.Database.CurrentTransaction != null) ;
        }

        public void DisableTracking()
        {
            this.DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void EnableTracking()
        {
            this.DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
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
