using Crawlers.Domain.Entities.Interfaces.ObjectValues.Urls;
using Crawlers.Domain.Exceptions.Urls;

namespace Crawlers.Domain.Entities.ObjectValues.Urls.ChainOfResponsability
{
    public class NotWellFormedUrl : AbstractUrlHandler, IUrlHandler
    {
        protected override string Pattern { get; } = "";

        public override Url Handle(string request)
        {
            throw new NotWellFormedUrlException(request);
        }
    }
}
