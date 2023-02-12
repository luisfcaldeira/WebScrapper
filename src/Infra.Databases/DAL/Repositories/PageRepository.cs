using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Domains.Interfaces.DAL.Repositories;
using Crawlers.Infra.Databases.Context;

namespace Crawlers.Infra.Databases.DAL.Repositories
{
    public class PageRepository : RepositoryBase<Page>, IPageRepository
    {
        public PageRepository(CrawlerDbContext dbContext) : base(dbContext)
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
            return all.Where(u => !u.IsVisited).OrderBy(r => Guid.NewGuid()).FirstOrDefault();
        }

        public bool Exists(Page page)
        {
            return GetPage(page.Url) != null;
        }
    }
}
