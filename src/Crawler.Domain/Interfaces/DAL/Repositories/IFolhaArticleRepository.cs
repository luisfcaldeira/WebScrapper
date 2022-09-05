using Crawlers.Domains.Entities.Articles;
using Crawlers.Domains.Entities.ObjectValues.Pages;

namespace Crawlers.Domains.Interfaces.DAL.Repositories
{
    public interface IFolhaArticleRepository : IRepositoryBase<FolhaArticle>
    {
        bool Exists(FolhaArticle folha);
        FolhaArticle? GetArticle(Page page);
    }
}
