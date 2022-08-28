using Crawlers.Domains.Entities.Interfaces.ObjectValues.Urls;
using Crawlers.Domains.Exceptions.Urls;
using System.Text.RegularExpressions;

namespace Crawlers.Domains.Entities.ObjectValues.Urls.ChainOfResponsability
{
    internal class NotWellFormedUrl : AbstractUrlHandler, IUrlHandler
    {
        protected override string Pattern { get; } = "";

        public override Url Handle(string request)
        {
            throw new NotWellFormedUrlException(request);
        }

        protected override Url CreateUrl(string request, GroupCollection groups)
        {
            throw new NotWellFormedUrlException(request);
        }
    }
}
