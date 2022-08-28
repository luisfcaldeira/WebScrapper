using Crawlers.Domains.Entities.ObjectValues.Urls;

namespace Crawlers.Domains.Interfaces.DAL.Repositories
{
    public interface IUrlRepository : IRepositoryBase<Url>
    {
        Url GetUrl(string url);
        IEnumerable<Url> GetAllNotVisited();
    }
}
