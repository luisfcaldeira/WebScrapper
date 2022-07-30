using Crawlers.Domain.Entities.ObjectValues.Urls;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crawler.Infra.Databases.Configs.Urls
{
    internal class UrlDomainConfig : IEntityTypeConfiguration<UrlDomain>
    {
        public void Configure(EntityTypeBuilder<UrlDomain> builder)
        {
        }
    }
}
