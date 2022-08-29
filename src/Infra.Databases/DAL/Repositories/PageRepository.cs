using Crawlers.Domains.Entities.ObjectValues.Urls;
using Crawlers.Domains.Interfaces.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Crawler.Infra.Databases.DAL.Repositories
{
    public class PageRepository : RepositoryBase<Page>, IPageRepository
    {
        public PageRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public Page GetPage(string url)
        {
            return GetAll().Where(u => u.Url == url).FirstOrDefault();    
        }

        public IEnumerable<Page> GetAllNotVisited()
        {
            return GetAll().Where(url => !url.IsVisited);
        }
    }
}
