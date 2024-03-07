using Crawlers.Domains.Entities.ObjectValues.Pages;

namespace Crawlers.Domains.Interfaces.DAL.Repositories
{
    public interface IPageRepository : IRepositoryBase<Page>
    {
        Page? GetPage(string url);
        IEnumerable<Page> GetAllNotVisited();
        IEnumerable<Page> GetNonVisited(int quantity);
        IEnumerable<Page> GetNonVisitedRegisteredForTask(int taskCode);
        bool Exists(Page page);
        void RemoveDuplicity();
        void Insert(Page page);
    }
}
