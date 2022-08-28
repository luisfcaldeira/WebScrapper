using Crawlers.Domains.Entities.ObjectValues.Urls;

namespace Crawlers.Domains.Entities.Articles
{
    public abstract class BaseArticle
    {
        public int Id { get; set; }
        public string Title { get; private set; } = string.Empty;
        public string Content { get; private set; } = string.Empty;
        public Url Url { get; private set; }
        public DateTime? Published { get; private set; } 
        public IList<Url> Urls { get; private set; }

        protected BaseArticle()
        {
        }

        protected BaseArticle(string title, string content, Url url, DateTime? published, IList<Url> urls)
        {
            Title = title;
            Content = content;
            Url = url;
            Published = published;
            Urls = urls;
        }

        public IEnumerable<Url>? GetValidUrls(ObjectValues.Urls.Domain domain)
        {
            return Urls?.Where(url => url.IsValid(domain)).ToList();
        }
    }
}
