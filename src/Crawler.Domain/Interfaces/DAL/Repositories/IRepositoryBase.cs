namespace Crawlers.Domain.Interfaces.DAL.Repositories
{
    public interface IRepositoryBase<T> : IDisposable
    {
        public T GetById(object id);
        public IEnumerable<T> GetAll();
        public void Add(T entity);
        public void Update(T entity);
    }
}
