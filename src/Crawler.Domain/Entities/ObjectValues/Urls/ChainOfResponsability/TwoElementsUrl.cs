using Crawlers.Domain.Entities.Interfaces.ObjectValues.Urls;

namespace Crawlers.Domain.Entities.ObjectValues.Urls.ChainOfResponsability
{
    public class TwoElementsUrl : AbstractUrlHandler, IUrlHandler
    {
        protected override string Pattern { get; } = @"(?:(?<protocol>[htps]{4,5})\:\/\/)?(?<domain_name>[^.]*)\.(?<toplevel>[^.]{2,3})";
    }
}
