using Crawlers.Domains.Entities.Articles;
using Crawlers.Infra.Databases.Configs.Pages;
using Microsoft.EntityFrameworkCore;

namespace Crawlers.Infra.Databases.Context
{
    public class CrawlerDbContext : DbContext
    {

        public CrawlerDbContext(DbContextOptions<CrawlerDbContext> options) : base(options)
        {
            Database.EnsureCreated();
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.HasDefaultSchema(GetType().FullName.Split('.')[0]);

            modelBuilder.Entity<FolhaArticle>();

            modelBuilder.ApplyConfiguration(new PageConfig());
            modelBuilder.ApplyConfiguration(new ArticleConfig());
            modelBuilder.ApplyConfiguration(new Configs.Collections.ObjectValues.PageCollectionConfig());
        }
    }
}
