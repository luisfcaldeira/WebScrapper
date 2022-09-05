namespace Crawlers.Domains.Entities.ObjectValues.Pages
{
    public class Domain
    {
        public Protocol Protocol { get; set; } = new Protocol();
        public Subdomain Subdomain { get; set; } = new Subdomain();
        public string Name { get; private set; } = string.Empty;
        public string TopLevel { get; private set; } = string.Empty;
        public Country Country { get; set; } = new Country();
        public UrlDirectory Directory { get; set; } = new UrlDirectory();

        public Domain()
        {
        }

        public Domain(string name, string topLevel) : this()
        {
            Name = name;
            TopLevel = topLevel;
        }

        public override bool Equals(object? obj)
        {
            return obj is Domain domain &&
                   Name == domain.Name &&
                   TopLevel == domain.TopLevel &&
                   Country.Equals(domain.Country);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, TopLevel, Country);
        }

        public override string? ToString()
        {
            return $"{Protocol}{Subdomain}{Name}.{TopLevel}{Country}{Directory}";
        }
    }
}
