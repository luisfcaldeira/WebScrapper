using Crawlers.Domains.Entities.ObjectValues.Urls;

namespace Crawlers.Domains.Entities.Articles
{
    public class FolhaArticle : BaseArticle
    {
        public FolhaArticle(string title, string content, Url url, DateTime? published, IList<Url> urls) : base(title, content, url, published, urls)
        {
        }
    }
}
