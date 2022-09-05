using Crawlers.Domains.Entities.Articles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Infra.Databases.Configs.Pages
{
    internal class ArticleConfig : IEntityTypeConfiguration<BaseArticle>
    {
        public void Configure(EntityTypeBuilder<BaseArticle> builder)
        {

            builder.HasMany(a => a.ReferredPages);
        }
    }
}
