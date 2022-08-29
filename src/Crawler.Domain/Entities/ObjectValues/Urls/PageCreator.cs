using Crawlers.Domains.Entities.ObjectValues.Urls.ChainOfResponsability;

namespace Crawlers.Domains.Entities.ObjectValues.Urls
{
    public static class PageCreator
    {
        public static Page Create(string url)
        {
            var twoElements = new TwoElementsUrl();
            var withSubdomain = new WithSubdomainUrl();
            var withCountry = new WithCountryUrl();
            var withError = new NotWellFormedUrl();

            withCountry
                .SetNext(withSubdomain)
                .SetNext(twoElements)
                .SetNext(withError);

            var client = new UrlClient(withCountry);

            return client.Handle(url.Trim());
        }
    }
}
