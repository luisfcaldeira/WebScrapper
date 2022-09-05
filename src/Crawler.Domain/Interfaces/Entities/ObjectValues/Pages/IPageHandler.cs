using Crawlers.Domains.Entities.ObjectValues.Pages;

namespace Crawlers.Domains.Interfaces.Entities.ObjectValues.Pages
{
    public interface IPageHandler
    {
        IPageHandler SetNext(IPageHandler handler);

        Page Handle(string request);
    }
}
