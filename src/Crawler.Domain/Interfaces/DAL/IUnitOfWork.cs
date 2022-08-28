using Crawlers.Domains.Interfaces.DAL.Repositories;

namespace Crawlers.Domains.Interfaces.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IUrlRepository UrlRepository { get; }
        void Save();
    }
}
