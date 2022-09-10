using Crawlers.Domains.Entities.Articles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crawlers.Infra.Databases.Configs.Pages
{
    internal class ArticleConfig : IEntityTypeConfiguration<BaseArticle>
    {
        public void Configure(EntityTypeBuilder<BaseArticle> builder)
        {
            builder.HasOne(a => a.ReferredPages);
        }
    }
}
