using Crawlers.Domains.Entities.ObjectValues.Urls;

namespace Crawlers.Domains.Entities.Interfaces.ObjectValues.Urls.Builders
{
    public interface IOptions
    {
        IOptions WithProtocol(string protocol);
        IOptions WithSubdomain(string subdomain);
        IOptions WithCountry(string country);
        IOptions WithDirectory(string directory);
        Page Create();
    }
}
