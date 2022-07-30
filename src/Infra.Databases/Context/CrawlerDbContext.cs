using Crawler.Infra.Databases.Configs.Urls;
using Crawlers.Domain.Entities.ObjectValues.Urls;
using Microsoft.EntityFrameworkCore;

namespace Crawler.Infra.Databases.Context
{
    public class CrawlerDbContext : DbContext
    {
        private readonly string connectionString;

        public CrawlerDbContext(string connectionString) : base()
        {
            this.connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.HasDefaultSchema(GetType().FullName.Split('.')[0]);

            modelBuilder.ApplyConfiguration<Url>(new UrlConfig());
        }
    }
}
