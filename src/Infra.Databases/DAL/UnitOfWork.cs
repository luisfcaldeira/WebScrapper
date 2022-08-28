using Crawler.Infra.Databases.DAL.Repositories;
using Crawlers.Domains.Interfaces.DAL;
using Crawlers.Domains.Interfaces.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Crawler.Infra.Databases.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private IUrlRepository _urlRepository;
        public IUrlRepository UrlRepository 
        { 
            get
            {
                if (_urlRepository == null)
                    _urlRepository = new UrlRepository(DbContext);
                return _urlRepository;
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
