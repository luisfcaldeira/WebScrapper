using Crawlers.Domains.Interfaces.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Crawlers.Infra.Databases.DAL.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private bool disposed = false;
        public DbContext DbContext { get; }
        public DbSet<T> DbSet { get; }

        public RepositoryBase(DbContext dbContext)
        {
            DbContext = dbContext;
            
            DbSet = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            DbContext.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            Task.Run(() =>
            {
                foreach (T entity in entities)
                {
                    Add(entity);
                }
            });
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.ToListAsync().Result;
        }

        public T? GetById(object id)
        {
            return DbSet.FindAsync(id).Result;
        }

        public void Delete(object id)
        {
            var entityToDelete = DbSet.FindAsync(id).Result;
            if(entityToDelete != null)
                Delete(entityToDelete);
        }

        public void Delete(T entityToDelete)
        {
            DbSet.Remove(entityToDelete);
        }

        public void Update(T entity)
        {
            try
            {

                DbSet.Attach(entity);
            } catch(InvalidOperationException ex)
            {
                if (!ex.Message.Contains("is already being tracked"))
                    throw;
            }

            DbContext.Entry(entity).State = EntityState.Modified;
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
