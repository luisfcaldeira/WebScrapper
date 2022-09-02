using Crawlers.Domains.Entities.ObjectValues.Urls;

namespace Crawlers.Domains.Entities.Articles
{
    public abstract class BaseArticle
    {
        public int Id { get; set; }
        public string Title { get; private set; } = string.Empty;
        public string Content { get; private set; } = string.Empty;
        public Page Url { get; private set; }
        public DateTime? Published { get; private set; } 
        public IList<Page> ReferredPages { get; private set; }

        protected BaseArticle()
        {
        }

        protected BaseArticle(string title, string content, Page url, DateTime? published, IList<Page> referredPages)
        {
            Title = title;
            Content = content;
            Url = url;
            Published = published;
            ReferredPages = referredPages;
        }

        public IEnumerable<Page>? GetValidUrls(Domain domain)
        {
            return ReferredPages?.Where(url => url.IsValid(domain)).ToList();
        }
    }
}
