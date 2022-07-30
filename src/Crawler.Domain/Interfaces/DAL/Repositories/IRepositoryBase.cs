namespace Crawlers.Domain.Interfaces.DAL.Repositories
{
    public interface IRepositoryBase<T>
    {
        public T GetById(object id);
        public IEnumerable<T> GetAll();
        public void Add(T entity);
        public void Update(T entity);
    }
}
