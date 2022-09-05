using Crawlers.Domains.Entities.ObjectValues.Pages;

namespace Crawlers.Domains.Entities.Articles
{
    public abstract class BaseArticle
    {
        public int Id { get; set; }
        public string Title { get; protected set; } = string.Empty;
        public string Content { get; protected set; } = string.Empty;
        public Page Page { get; private set; }
        public DateTime? Published { get; protected set; } 
        public IList<Page> ReferredPages { get; protected set; }

        protected BaseArticle()
        {
        }

        protected BaseArticle(string title, string content, Page url, DateTime? published, IList<Page> referredPages)
        {
            Title = title;
            Content = content;
            Page = url;
            Published = published;
            ReferredPages = referredPages;
        }

        public IEnumerable<Page>? GetValidPages(Domain domain)
        {
            return ReferredPages?.Where(url => url.IsValid(domain)).ToList();
        }

        public override bool Equals(object? obj)
        {
            if(obj is BaseArticle article)
            {
                var pagesAreEquals = EqualityComparer<Page>.Default.Equals(Page, article.Page);
                return Id == article.Id || pagesAreEquals;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Page);
        }

        public override string? ToString()
        {
            return $"{Title} - {Published} - [{Page.Url}]";
        }
    }
}
