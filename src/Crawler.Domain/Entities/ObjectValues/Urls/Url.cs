namespace Crawlers.Domains.Entities.ObjectValues.Urls
{
    public class Url
    {
        public int Id { get; private set; }
        public Domain Domain { get; private set; }
        public string Value { get; set; } = string.Empty;
        public bool IsVisited { get; set; } = false;

        protected Url()
        {
            Domain = new Domain();  
        }

        public Url(Domain domain) : this()
        {
            Domain = domain;
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
