using Crawlers.Domain.Entities.ObjectValues.Urls;

namespace Crawlers.Domain.Interfaces.DAL.Repositories
{
    public interface IUrlRepository : IRepositoryBase<Url>
    {
        Url GetUrl(string url);
        IEnumerable<Url> GetAllNotVisited();
    }
}
