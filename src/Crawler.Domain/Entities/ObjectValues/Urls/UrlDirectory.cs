using System.Text.RegularExpressions;

namespace Crawlers.Domains.Entities.ObjectValues.Urls
{
    public class UrlDirectory
    {
        private const string RegexPattern = @"\/{0,1}(?<directory>\S+)";
        public string Value { get; set; }

        public UrlDirectory()
        {
            Value = "";
        }

        public UrlDirectory(string url) : this()
        {
            Extract(url);
        }

        private void Extract(string url)
        {
            var regex = new Regex(RegexPattern);
            if (regex.IsMatch(url))
            {
                var groups = regex.Match(url).Groups;
                Value = groups["directory"].Value;
            }
        }

        public override string? ToString()
        {
            return "/" + Value;
        }
    }
}
