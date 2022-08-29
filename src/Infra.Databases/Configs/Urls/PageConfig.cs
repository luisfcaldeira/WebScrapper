using Crawlers.Domains.Entities.ObjectValues.Urls;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crawler.Infra.Databases.Configs.Urls
{
    internal class PageConfig : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.OwnsOne(url => url.Domain);
            builder.OwnsOne(url => url.Domain, u =>
            {
                u.OwnsOne(d => d.Protocol);
                u.OwnsOne(d => d.Subdomain);
                u.OwnsOne(d => d.Directory);
                u.OwnsOne(d => d.Country);
            });
        }
    }
}
