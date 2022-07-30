using System.Text.RegularExpressions;

namespace Crawlers.Domain.Entities.ObjectValues.Urls
{
    public class UrlProtocol
    {
        public const string RegexPattern = @"([htps]{4,5})\:\/\/";
        public string Value { get; private set; }

        protected UrlProtocol()
        {

        }

        public UrlProtocol(string url)
        {
            Value = "http";
            if(Regex.IsMatch(url, RegexPattern))
            {
                var match = Regex.Match(url, RegexPattern);
                var groups = match.Groups;
                Value = groups[1].Value;
            }
        }
    }
}
