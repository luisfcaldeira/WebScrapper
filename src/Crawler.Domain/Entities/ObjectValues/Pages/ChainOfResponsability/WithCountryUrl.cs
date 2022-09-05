using Crawlers.Domains.Entities.ObjectValues.Pages.Builders;
using Crawlers.Domains.Interfaces.Entities.ObjectValues.Pages;
using System.Text.RegularExpressions;

namespace Crawlers.Domains.Entities.ObjectValues.Pages.ChainOfResponsability
{
    internal class WithCountryUrl : AbstractUrlHandler, IPageHandler
    {
        protected override string Pattern { get; } = @"^(?:(?<protocol>[htps]{4,5}))?\:?\/{0,2}(?<subdomain>[^\.\s]*)?\.?(?<domain_name>[\S]*)\.(?<toplevel>[^\.\s]{2,3})\.(?<country>[^\.\s]{2})\/{0,1}(?<directory>[\S]+)?$";

        protected override Page CreateUrl(string request, GroupCollection groups)
        {
            var protocol = groups["protocol"].Value;
            var subdomain = groups["subdomain"].Value;
            var domainName = groups["domain_name"].Value;
            var toplevel = groups["toplevel"].Value;
            var country = groups["country"].Value;
            var directory = groups["directory"].Value;

            return PageBuilder.With(domainName, toplevel)
                .WithUrl(request)
                .WithProtocol(protocol)
                .WithSubdomain(subdomain)
                .WithCountry(country)
                .WithDirectory(directory)
                .Create();
        }
    }
}
