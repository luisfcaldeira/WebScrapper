using Crawlers.Domain.Entities.ObjectValues.Urls;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crawler.Infra.Databases.Configs.Urls
{
    internal class UrlProtocolConfig : IEntityTypeConfiguration<UrlProtocol>
    {
        public void Configure(EntityTypeBuilder<UrlProtocol> builder)
        {
        }
    }
}
