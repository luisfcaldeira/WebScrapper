using Crawlers.Domain.Interfaces.DAL.Repositories;

namespace Crawlers.Domain.Interfaces.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IUrlRepository UrlRepository { get; }
        void Save();
    }
}
