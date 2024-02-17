using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Domains.Interfaces.DAL.Repositories;
using Crawlers.Infra.Databases.Context;

namespace Crawlers.Infra.Databases.DAL.Repositories
{
    public class PageRepository : RepositoryBase<Page>, IPageRepository
    {
        private static bool _semaphore = false;
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
            return GetNonVisited(1).FirstOrDefault();
        }

        public bool Exists(Page page)
        {
            return GetPage(page.Url) != null;
        }

        public IEnumerable<Page> GetNonVisited(int quantity)
        {
            if(_semaphore) 
                return new List<Page>();

            _semaphore = true;
            var result =  GetAll().Where(u => !u.IsVisited && u.TaskCode == -1)
                .OrderBy(r => Guid.NewGuid())
                .Take(quantity);
            _semaphore = false;
            return result;
        }
    }
}
