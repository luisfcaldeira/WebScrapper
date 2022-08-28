using Crawlers.Domains.Entities.Interfaces.ObjectValues.Urls;
using Crawlers.Domains.Entities.ObjectValues.Urls.Builders;
using System.Text.RegularExpressions;

namespace Crawlers.Domains.Entities.ObjectValues.Urls.ChainOfResponsability
{
    internal class WithCountryUrl : AbstractUrlHandler, IUrlHandler
    {
        protected override string Pattern { get; } = @"^(?:(?<protocol>[htps]{4,5})\:\/\/)?(?<subdomain>[^\.\s]*)?\.?(?<domain_name>[\S]*)\.(?<toplevel>[^\.\s]{2,3})\.(?<country>[^\.\s]{2})(?<directory>\/{1}[\S]+)?$";

        protected override Url CreateUrl(string request, GroupCollection groups)
        {
            var protocol = groups["protocol"].Value;
            var subdomain = groups["subdomain"].Value;
            var domainName = groups["domain_name"].Value;
            var toplevel = groups["toplevel"].Value;
            var country = groups["country"].Value;
            var directory = groups["directory"].Value;

            return UrlBuilder.With(domainName, toplevel)
                .WithUrl(request)
                .WithProtocol(protocol)
                .WithSubdomain(subdomain)
                .WithCountry(country)
                .WithDirectory(directory)
                .Create();
        }
    }
}
