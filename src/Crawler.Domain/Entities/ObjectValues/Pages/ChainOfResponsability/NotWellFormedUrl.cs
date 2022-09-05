using Crawlers.Domains.Exceptions.Urls;
using Crawlers.Domains.Interfaces.Entities.ObjectValues.Pages;
using System.Text.RegularExpressions;

namespace Crawlers.Domains.Entities.ObjectValues.Pages.ChainOfResponsability
{
    internal class NotWellFormedUrl : AbstractUrlHandler, IPageHandler
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
