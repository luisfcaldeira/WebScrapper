using Crawlers.Domains.Entities.ObjectValues.Pages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crawlers.Infra.Databases.Configs.Pages
{
    internal class PageConfig : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.HasMany(page => page.Articles);
            builder.OwnsOne(page => page.Domain);
            builder.OwnsOne(page => page.Domain, domain =>
            {
                domain.OwnsOne(d => d.Protocol);
                domain.OwnsOne(d => d.Subdomain);
                domain.OwnsOne(d => d.Directory);
                domain.OwnsOne(d => d.Country);
            });

            builder.Ignore(d => d.Url);
            builder.HasMany(d => d.PagesCollections);
        }
    }
}
