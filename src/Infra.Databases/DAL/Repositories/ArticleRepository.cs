using Crawlers.Domains.Entities.Articles;
using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Domains.Interfaces.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Crawlers.Infra.Databases.DAL.Repositories
{
    internal class ArticleRepository : RepositoryBase<Article>, IArticleRepository
    {
        public ArticleRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public bool Exists(Article article)
        {
            return GetAll()
                .Where(f => f.Equals(article))
                .Any();
        }

        public Article GetArticle(Page page)
        {
            return GetAll()
                .Where(f => f.Url.Equals(page.RawUrl))
                .FirstOrDefault();
        }
    }
}
