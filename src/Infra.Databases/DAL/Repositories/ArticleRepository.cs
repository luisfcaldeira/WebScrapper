using Crawlers.Domains.Entities.Articles;
using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Domains.Interfaces.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Crawlers.Infra.Databases.DAL.Repositories
{
    internal class ArticleRepository : RepositoryBase<Article>, IArticleRepository
    {
        public ArticleRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public void Insert(Article article)
        {
            var publishDate = "NULL";
            if (article.Published != null)
            {
                publishDate = $"'{article.Published.Value:yyyy-MM-dd hh:mm}'";
            }

            var sql = @$"
                        INSERT INTO [Crawlers].[Article]
                                   ([Title]
                                   ,[Content]
                                   ,[Url]
                                   ,[Published]
                                   ,[IsValid]
                                   )
                             VALUES
                                   ('{article.Title.Replace("'", "''").Replace("\"", "\"\"")}'
                                   ,'{article.Content.Replace("'", "''").Replace("\"", "\"\"")}'
                                   ,'{article.Url}'
                                   ,{publishDate}
                                   ,'{article.IsValid}'
                                   )
                        ";
            try
            {
                DbContext.Database.ExecuteSqlRaw(sql);

            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public bool Exists(Article article)
        {
            return GetAll()
                .Where(f => f.Equals(article))
                .Any();
        }

        public Article GetArticle(Page page)
        {
            return GetAll()
                .Where(f => f.Url.Equals(page.RawUrl))
                .FirstOrDefault();
        }
    }
}
