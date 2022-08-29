using Crawlers.Domains.Entities.ObjectValues.Urls;

namespace Crawlers.Domains.Entities.Articles
{
    public class FolhaArticle : BaseArticle
    {
        public FolhaArticle(string title, string content, Page url, DateTime? published, IList<Page> urls) : base(title, content, url, published, urls)
        {
        }
    }
}
