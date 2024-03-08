using Crawlers.Domains.Interfaces.DAL.Repositories;

namespace Crawlers.Domains.Interfaces.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IPageRepository PageRepository { get; }
        IArticleRepository ArticleRepository { get; }
        void Save();
        public void DisableTracking();
        public void EnableTracking();
    }
}
