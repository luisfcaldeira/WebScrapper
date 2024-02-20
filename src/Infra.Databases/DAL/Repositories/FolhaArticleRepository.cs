using Crawlers.Domains.Entities.Articles;
using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Domains.Interfaces.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Crawlers.Infra.Databases.DAL.Repositories
{
    internal class FolhaArticleRepository : RepositoryBase<FolhaArticle>, IFolhaArticleRepository
    {
        public FolhaArticleRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public bool Exists(FolhaArticle folha)
        {
            return GetAll()
                .Where(f => f.Equals(folha))
                .Any();
        }

        public FolhaArticle? GetArticle(Page page)
        {
            return GetAll()
                .Where(f => f.Url.Equals(page.RawUrl))
                .FirstOrDefault();
        }
    }
}
