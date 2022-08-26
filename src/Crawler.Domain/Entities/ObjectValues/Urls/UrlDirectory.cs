using System.Text.RegularExpressions;

namespace Crawlers.Domain.Entities.ObjectValues.Urls
{
    public class UrlDirectory
    {
        private const string RegexPattern = @"\/(?<directory>[^\/]+)";
        public string Value { get; set; }

        protected UrlDirectory()
        {

        }

        public UrlDirectory(string url)
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
    }
}
