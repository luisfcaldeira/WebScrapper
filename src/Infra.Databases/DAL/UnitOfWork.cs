using Crawler.Infra.Databases.DAL.Repositories;
using Crawlers.Domains.Interfaces.DAL;
using Crawlers.Domains.Interfaces.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Crawler.Infra.Databases.DAL
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

        public DbContext DbContext { get; }

        public UnitOfWork(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Save()
        {
            DbContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
