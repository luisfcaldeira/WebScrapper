using Crawlers.Domains.Entities.ObjectValues.Pages.ChainOfResponsability;

namespace Crawlers.Domains.Entities.ObjectValues.Pages
{
    public static class PageCreator
    {
        public static Page Create(string url)
        {
            var twoElements = new TwoElementsUrl();
            var withSubdomain = new WithSubdomainUrl();
            var withCountry = new WithCountryUrl();
            var withError = new NotWellFormedUrl();
            var onlyDirectory = new OnlyDirectory();

            withCountry
                .SetNext(withSubdomain)
                .SetNext(twoElements)
                .SetNext(onlyDirectory)
                .SetNext(withError);

            var client = new UrlClient(withCountry);

            return client.Handle(url.Trim());
        }
    }
}
