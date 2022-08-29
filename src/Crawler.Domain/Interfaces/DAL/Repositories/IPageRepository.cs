using Crawlers.Domains.Entities.ObjectValues.Urls;

namespace Crawlers.Domains.Interfaces.DAL.Repositories
{
    public interface IPageRepository : IRepositoryBase<Page>
    {
        Page GetPage(string url);
        IEnumerable<Page> GetAllNotVisited();
    }
}
