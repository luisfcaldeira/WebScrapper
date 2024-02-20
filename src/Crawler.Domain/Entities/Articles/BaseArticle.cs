using Crawlers.Domains.Entities.ObjectValues.Pages;

namespace Crawlers.Domains.Entities.Articles
{
    public abstract class BaseArticle
    {
        public int Id { get; set; }
        public string Title { get; protected set; } = string.Empty;
        public string Content { get; protected set; } = string.Empty;
        public string Url { get; private set; } 
        public DateTime? Published { get; protected set; } 
        public bool IsValid { get; private set; } = false;

        protected BaseArticle()
        {
        }

        protected BaseArticle(string title, string content, Page page, DateTime? published)
        {
            Title = title;
            Content = ClearContent(content);
            Url = page.RawUrl;
            Published = published;

            if (CheckIfContentExist(content))
            {
                IsValid = true;
            }
        }

        private string ClearContent(string content) 
        {
            var newContent = content
                .Replace("  ", "")
                .Replace("\t", "")
                .Replace("\n", "");

            if (newContent.Contains("  ")
                || newContent.Contains("\t")
                || newContent.Contains("\n")
                )
                return ClearContent(newContent);

            return newContent;
        }

        private bool CheckIfContentExist(string? content)
        {
            return content != string.Empty && content != null;
        }

        public override string? ToString()
        {
            return $"{Title} - {Published} - [{Url}]";
        }

        public override bool Equals(object? obj)
        {
            return obj is BaseArticle article &&
                   Url == article.Url;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Url);
        }
    }
}
