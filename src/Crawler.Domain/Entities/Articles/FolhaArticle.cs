using Crawlers.Domains.Entities.ObjectValues.Pages;

namespace Crawlers.Domains.Entities.Articles
{
    public class FolhaArticle : BaseArticle
    {
        protected FolhaArticle() : base() { }

        public FolhaArticle(string? title, string? content, Page url, DateTime? published) : base(title, content, url, published)
        {
        }

        public void Update(FolhaArticle folha)
        {
            Title = folha.Title;
            Content = folha.Content;
            Published = folha.Published;
        }
    }
}
