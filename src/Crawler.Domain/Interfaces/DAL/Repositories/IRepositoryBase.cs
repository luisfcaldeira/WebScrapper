namespace Crawlers.Domains.Interfaces.DAL.Repositories
{
    public interface IRepositoryBase<T> : IDisposable
    {
        IEnumerable<T> GetAll();
        T? GetById(object id);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
    }
}
