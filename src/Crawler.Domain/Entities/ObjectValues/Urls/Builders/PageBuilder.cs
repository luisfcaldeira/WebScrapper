using Crawlers.Domains.Entities.Interfaces.ObjectValues.Urls.Builders;

namespace Crawlers.Domains.Entities.ObjectValues.Urls.Builders
{
    public class PageBuilder : IOptions, IFirstOption
    {
        private static Domain _domain;
        private string _url;

        private PageBuilder () {}

        public static IFirstOption With(string domainName, string topLevel)
        {
            _domain = new Domain(domainName, topLevel);
            return new PageBuilder();
        }

        public IOptions WithUrl(string url)
        {
            _url = url;
            return this;
        }

        public IOptions WithProtocol(string protocol)
        {
            _domain.Protocol = new Protocol(protocol);
            return this;
        }

        public IOptions WithSubdomain(string subdomain)
        {
            _domain.Subdomain = new Subdomain(subdomain);
            return this;
        }

        public IOptions WithCountry(string country)
        {
            _domain.Country = new Country(country);
            return this;
        }

        public IOptions WithDirectory(string directory)
        {
            _domain.Directory = new UrlDirectory(directory);
            return this;
        }

        public Page Create()
        {
            return new Page(_domain, _url);
        }
    }
}
