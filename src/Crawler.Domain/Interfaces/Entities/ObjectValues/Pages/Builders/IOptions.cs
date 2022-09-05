using Crawlers.Domains.Entities.ObjectValues.Pages;

namespace Crawlers.Domains.Interfaces.Entities.ObjectValues.Pages.Builders
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
