using System.Text.RegularExpressions;

namespace Crawlers.Domain.Entities.ObjectValues.Urls
{
    public class UrlDomain
    {
        public const string RegexPattern = @"(?<domain>[^.]*\.[^.]{2,3}(?:\.[^.]{2,3})?$)";
        public string Value { get; private set; } = "";
        public string[] Parts { get; private set; } 

        protected UrlDomain()
        {

        }

        public UrlDomain(string url)
        {
            Extract(url);
        }

        private void Extract(string url)
        {
            var cleanedString = Clean(url);
            var regex = new Regex(RegexPattern);
            if (regex.IsMatch(cleanedString))
            {
                var groups = regex.Match(cleanedString).Groups;
                Value = groups["domain"].Value;
                Parts = Value.Split('.');
            }
        }

        private string Clean(string url)
        {
            url = Regex.Replace(url, @"([htps]{4,5})\:\/\/([w]{3}\.)?", "");
            var urls = url.Split("/");

            return urls[0];
        }

        public override bool Equals(object? obj)
        {
            return obj is UrlDomain domain &&
                   Value == domain.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }
}
