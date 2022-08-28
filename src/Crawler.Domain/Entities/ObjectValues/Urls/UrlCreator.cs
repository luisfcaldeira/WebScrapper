using Crawlers.Domains.Entities.ObjectValues.Urls.ChainOfResponsability;

namespace Crawlers.Domains.Entities.ObjectValues.Urls
{
    public static class UrlCreator
    {
        public static Url Create(string url)
        {
            var twoElements = new TwoElementsUrl();
            var withSubdomain = new WithSubdomainUrl();
            var withCountry = new WithCountryUrl();
            var withError = new NotWellFormedUrl();

            twoElements
                .SetNext(withSubdomain)
                .SetNext(withCountry)
                .SetNext(withError);

            var client = new UrlClient(twoElements);

            return client.Handle(url);
        }
    }
}
