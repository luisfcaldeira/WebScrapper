using Crawlers.Domains.Entities.Interfaces.ObjectValues.Urls;
using Crawlers.Domains.Entities.ObjectValues.Urls.Builders;
using System.Text.RegularExpressions;

namespace Crawlers.Domains.Entities.ObjectValues.Urls.ChainOfResponsability
{
    internal class TwoElementsUrl : AbstractUrlHandler, IUrlHandler
    {
        protected override string Pattern { get; } = @"^(?:(?<protocol>[htps]{4,5})\:\/\/)?(?<domain_name>[^\.\s]*)\.(?<top_level>[^\.\s]{2,3})\/{0,1}(?<directory>[\S]+)?$";

        protected override Page CreateUrl(string request, GroupCollection groups)
        {
            var protocol = groups["protocol"].Value;
            var domainName = groups["domain_name"].Value;
            var topLevel = groups["top_level"].Value;
            var directory = groups["directory"].Value;

            return PageBuilder
                .With(domainName, topLevel)
                .WithUrl(request)
                .WithProtocol(protocol)
                .WithDirectory(directory)
                .Create();
        }
    }
}
