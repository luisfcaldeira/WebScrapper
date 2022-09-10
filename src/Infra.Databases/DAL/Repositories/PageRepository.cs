using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Domains.Interfaces.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Crawlers.Infra.Databases.DAL.Repositories
{
    public class PageRepository : RepositoryBase<Page>, IPageRepository
    {
        public PageRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public Page? GetPage(string url)
        {
            return GetAll().Where(u => u.Url == url).FirstOrDefault();    
        }

        public IEnumerable<Page> GetAllNotVisited()
        {
            return GetAll().Where(url => !url.IsVisited);
        }

        public Page? GetOneNotVisited()
        {
            var all = GetAll();
            return all.Where(u => !u.IsVisited).FirstOrDefault();
        }

        public bool Exists(Page page)
        {
            return GetPage(page.Url) != null;
        }
    }
}
