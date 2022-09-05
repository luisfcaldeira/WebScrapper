namespace Crawlers.Domains.Entities.ObjectValues.Pages
{
    public class Subdomain
    {
        public string Value { get; private set; }

        public Subdomain()
        {
            Value = string.Empty;
        }

        public Subdomain(string value) : this()
        {
            Value = value;
        }

        public override string? ToString()
        {
            return string.IsNullOrEmpty(Value) ? null : $"{Value}.";
        }
    }
}
