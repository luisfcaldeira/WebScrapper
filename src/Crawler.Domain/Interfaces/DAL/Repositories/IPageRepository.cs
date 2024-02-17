using Crawlers.Domains.Entities.ObjectValues.Pages;

namespace Crawlers.Domains.Interfaces.DAL.Repositories
{
    public interface IPageRepository : IRepositoryBase<Page>
    {
        Page? GetPage(string url);
        IEnumerable<Page> GetAllNotVisited();
        Page? GetOneNotVisited();
        IEnumerable<Page> GetNonVisited(int quantity);
        bool Exists(Page page);
    }
}
