using Crawlers.Domain.Entities.ObjectValues.Urls;
using Crawlers.Domain.Interfaces.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Crawler.Infra.Databases.DAL.Repositories
{
    public class UrlRepository : RepositoryBase<Url>, IUrlRepository
    {
        public UrlRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public Url GetUrl(string url)
        {
            return GetAll().Where(u => u.Value == url).FirstOrDefault();    
        }

        public IEnumerable<Url> GetAllNotVisited()
        {
            return GetAll().Where(url => !url.IsVisited);
        }
    }
}
