namespace Crawlers.Domain.Entities.ObjectValues.Urls
{
    public class Url
    {
        public int Id { get; set; }
        public UrlProtocol Protocol { get; private set; }
        public UrlDomain Domain { get; private set; }
        public string Value { get; private set; }
        public bool IsVisited { get; set; } = false;

        protected Url()
        {

        }

        public Url(string url)
        {
            Domain = new UrlDomain(url);
            Protocol = new UrlProtocol(url);
            Value = url;
        }
    }
}
