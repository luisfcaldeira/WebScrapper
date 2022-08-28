using System.Text.RegularExpressions;

namespace Crawlers.Domains.Entities.ObjectValues.Urls
{
    public class Country
    {
        private const string Pattern = @"(?<country>[\S]{1,3})";
        public string Value { get; set; } = string.Empty;

        public Country()
        {
            Value = string.Empty;
        }
         
        public Country(string value) : this()
        {
            Value = Extract(value);
        }

        private string Extract(string value)
        {
            var regex = new Regex(Pattern);
            if (regex.IsMatch(value))
            {
                return regex.Match(value).Groups["country"].Value;
            }

            return "";
        }

        public override string? ToString()
        {
            return string.IsNullOrEmpty(Value) ? null : "." + Value;
        }

        public override bool Equals(object? obj)
        {
            return obj is Country country &&
                   Value == country.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }
}
