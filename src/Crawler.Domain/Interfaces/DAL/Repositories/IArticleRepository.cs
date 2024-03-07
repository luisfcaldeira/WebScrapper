using Crawlers.Domains.Entities.Articles;
using Crawlers.Domains.Entities.ObjectValues.Pages;

namespace Crawlers.Domains.Interfaces.DAL.Repositories
{
    public interface IArticleRepository : IRepositoryBase<Article>
    {
        bool Exists(Article article);
        Article GetArticle(Page page);
    }
}
