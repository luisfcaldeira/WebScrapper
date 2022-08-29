namespace Crawlers.Domains.Entities.ObjectValues.Urls
{
    public class Page
    {
        public int Id { get; private set; }
        public Domain Domain { get; private set; }
        public string Url { get; set; } = string.Empty;
        public bool IsVisited { get; set; } = false;

        protected Page()
        {
            Domain = new Domain();  
        }

        public Page(Domain domain, string url) : this()
        {
            Domain = domain;
            Url = url;
        }

        public bool IsValid(Domain otherDomain)
        {
            return otherDomain.Equals(Domain);
        }

        public override string? ToString()
        {
            return Domain.ToString();
        }
    }
}
