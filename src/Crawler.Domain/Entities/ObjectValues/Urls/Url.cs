namespace Crawlers.Domain.Entities.ObjectValues.Urls
{
    public class Url
    {
        public int Id { get; private set; }
        public UrlProtocol Protocol { get; private set; }
        public UrlDomain Domain { get; private set; }
        public UrlDirectory Directory { get; private set; }

        public Url(string urlString)
        {
            Protocol = new UrlProtocol(urlString);
            Domain = new UrlDomain(urlString);
            Directory = new UrlDirectory(urlString);
        }

        public bool IsValid(UrlDomain otherDomain)
        {
            return otherDomain.Equals(Domain);
        }
    }
}
