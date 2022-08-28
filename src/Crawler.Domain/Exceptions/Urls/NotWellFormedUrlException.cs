namespace Crawlers.Domains.Exceptions.Urls
{
    public class NotWellFormedUrlException : Exception
    {
        public NotWellFormedUrlException(string url) : base($"It was impossible to create an url with informed value: '{url}'.") { }
    }
}
