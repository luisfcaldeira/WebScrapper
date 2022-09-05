namespace Crawlers.Domains.Interfaces.DAL.Repositories
{
    public interface IRepositoryBase<T> : IDisposable
    {
        public IEnumerable<T> GetAll();
        public T GetById(object id);
        public void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        public void Update(T entity);
    }
}
