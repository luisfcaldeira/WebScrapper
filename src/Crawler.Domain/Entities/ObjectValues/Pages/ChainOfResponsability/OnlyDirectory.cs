using Crawlers.Domains.Entities.ObjectValues.Pages.Builders;
using Crawlers.Domains.Interfaces.Entities.ObjectValues.Pages;
using System.Text.RegularExpressions;

namespace Crawlers.Domains.Entities.ObjectValues.Pages.ChainOfResponsability
{
    internal class OnlyDirectory : AbstractUrlHandler, IPageHandler
    {
        protected override string Pattern { get; } = @".+\/?";

        protected override Page CreateUrl(string request, GroupCollection groups)
        {

            return PageBuilder
                .With("", "")
                .WithUrl(request)
                .WithProtocol("")
                .WithDirectory(request)
                .Create();
        }
    }
}
