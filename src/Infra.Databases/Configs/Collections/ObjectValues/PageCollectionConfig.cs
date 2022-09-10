using Crawlers.Domains.Collections.ObjectValues.Pages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crawlers.Infra.Databases.Configs.Collections.ObjectValues
{
    internal class PageCollectionConfig : IEntityTypeConfiguration<PageCollection>
    {
        public void Configure(EntityTypeBuilder<PageCollection> builder)
        {
            builder.HasMany(a => a.Pages);
        }
    }
}
