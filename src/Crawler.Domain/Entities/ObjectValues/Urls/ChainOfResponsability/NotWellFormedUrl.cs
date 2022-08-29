using Crawlers.Domains.Entities.Interfaces.ObjectValues.Urls;
using Crawlers.Domains.Exceptions.Urls;
using System.Text.RegularExpressions;

namespace Crawlers.Domains.Entities.ObjectValues.Urls.ChainOfResponsability
{
    internal class NotWellFormedUrl : AbstractUrlHandler, IUrlHandler
    {
        protected override string Pattern { get; } = "";

        public override Page Handle(string request)
        {
            throw new NotWellFormedUrlException(request);
        }

        protected override Page CreateUrl(string request, GroupCollection groups)
        {
            throw new NotWellFormedUrlException(request);
        }
    }
}
