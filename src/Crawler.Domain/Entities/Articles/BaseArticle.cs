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
        public bool IsValid { get; private set; } = false;

        protected BaseArticle()
        {
        }

        protected BaseArticle(string title, string content, Page page, DateTime? published)
        {
            Title = title;
            Content = content;
            Page = page;
            Published = published;

            if (CheckIfContentOrTitleExist(title, content))
            {
                IsValid = true;
            }
        }

        private bool CheckIfContentOrTitleExist(string? title, string? content)
        {
            return title != string.Empty && title != null && content != string.Empty && content != null;
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
