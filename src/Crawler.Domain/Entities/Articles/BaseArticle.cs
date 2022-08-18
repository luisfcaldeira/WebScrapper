namespace Crawlers.Domain.Entities.Articles
{
    public abstract class BaseArticle
    {
        public int Id { get; set; }
        public string Title { get; private set; } = string.Empty;
        public string Content { get; private set; } = string.Empty;
        public DateTime? Published { get; private set; } 

        protected BaseArticle()
        {
        }

        public BaseArticle(string title, string content, DateTime? published)
        {
            Title = title;
            Content = content;
            Published = published;
        }
    }
}
