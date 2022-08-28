using Crawlers.Domains.Entities.Interfaces.ObjectValues.Urls;
using Crawlers.Domains.Entities.ObjectValues.Urls.Builders;
using System.Text.RegularExpressions;

namespace Crawlers.Domains.Entities.ObjectValues.Urls.ChainOfResponsability
{
    internal class WithSubdomainUrl : AbstractUrlHandler, IUrlHandler
    {
        protected override string Pattern { get; } = @"^(?:(?<protocol>[htps]{4,5})\:\/\/)?(?<subdomain>[^\.\s]*)\.(?<domain_name>[^\.\s]*)\.(?<toplevel>[^\.\s]{2,3})(?<directory>\/{1}\S+)?$";

        protected override Url CreateUrl(string request, GroupCollection groups)
        {
            var protocol = groups["protocol"].Value;
            var subDomain = groups["subdomain"].Value;
            var name = groups["domain_name"].Value;
            var topLevel = groups["toplevel"].Value;
            var directory = groups["directory"].Value;

            return UrlBuilder.With(name, topLevel)
                .WithUrl(request)
                .WithProtocol(protocol)
                .WithSubdomain(subDomain)
                .WithDirectory(directory)
                .Create();
        }
    }
}
