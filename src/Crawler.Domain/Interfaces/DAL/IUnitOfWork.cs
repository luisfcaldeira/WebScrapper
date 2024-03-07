using Crawlers.Domains.Interfaces.DAL.Repositories;

namespace Crawlers.Domains.Interfaces.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IPageRepository PageRepository { get; }
        IArticleRepository FolhaArticleRepository { get; }
        void Save();
        public void DisableTracking();
        public void EnableTracking();
    }
}
