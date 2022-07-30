using Crawlers.Domain.Entities.ObjectValues.Urls;
using Microsoft.EntityFrameworkCore;

namespace Crawler.Infra.Databases.DAL.Repositories
{
    public class UrlRepository : RepositoryBase<Url>
    {
        public UrlRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
