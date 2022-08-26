using Crawlers.Domain.Entities.ObjectValues.Urls;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crawler.Infra.Databases.Configs.Urls
{
    internal class UrlConfig : IEntityTypeConfiguration<Url>
    {
        public void Configure(EntityTypeBuilder<Url> builder)
        {
            builder.OwnsOne(url => url.Directory);
            builder.OwnsOne(url => url.Domain, u =>
            {
                u.Ignore(i => i.Parts);
            });
            builder.OwnsOne(url => url.Protocol);
        }
    }
}
