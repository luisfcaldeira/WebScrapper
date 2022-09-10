using Crawlers.Domains.Collections.ObjectValues.Pages;
using Crawlers.Domains.Entities.ObjectValues.Pages;

namespace Crawlers.Domains.Entities.Articles
{
    public abstract class BaseArticle
    {
        public int Id { get; set; }
        public string Title { get; protected set; } = string.Empty;
        public string Content { get; protected set; } = string.Empty;
        public Page? Page { get; private set; } 
        public DateTime? Published { get; protected set; } 
        public PageCollection? ReferredPages { get; set; }

        protected BaseArticle()
        {
        }

        protected BaseArticle(string title, string content, Page page, DateTime? published)
        {
            if (CheckIfContentOrTitleExist(content))
            {
                throw new ArgumentException($"It was not possible to create an Article from '{page.Url}'.");
            }

            Title = title;
            Content = content;
            Page = page;
            Published = published;
        }

        private bool CheckIfContentOrTitleExist(string? content)
        {
            return (Title == string.Empty || Title == null) && (content == string.Empty || content == null);
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
