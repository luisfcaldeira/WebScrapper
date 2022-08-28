using Crawlers.Domains.Entities.Interfaces.ObjectValues.Urls;
using Crawlers.Domains.Entities.ObjectValues.Urls.Builders;
using System.Text.RegularExpressions;

namespace Crawlers.Domains.Entities.ObjectValues.Urls.ChainOfResponsability
{
    internal class TwoElementsUrl : AbstractUrlHandler, IUrlHandler
    {
        protected override string Pattern { get; } = @"^(?:(?<protocol>[htps]{4,5})\:\/\/)?(?<domain_name>[^\.\s]*)\.(?<top_level>[^\.\s]{2,3})$";

        protected override Url CreateUrl(string request, GroupCollection groups)
        {
            var protocol = groups["protocol"].Value;
            var domainName = groups["domain_name"].Value;
            var topLevel = groups["top_level"].Value;

            return UrlBuilder
                .With(domainName, topLevel)
                .WithUrl(request)
                .WithProtocol(protocol)
                .Create();
        }
    }
}
