using System.Text.RegularExpressions;

namespace Crawlers.Domain.Entities.ObjectValues.Urls
{
    public class UrlDomain
    {
        private const string RegexPattern = @"(?:http\:\/\/|https\:\/\/)?((?:[\w\d\-]{2,}\.)(?:[\w\d\-]{2,}\.?)(?:[\w\d\-]{2,}\.?)?(?:[\w\d\-]{2,}\.?)?(?:[\w\d\-]{2,}\.?)?(?:[\w\d\-]{2,}\.?)?(?:[\w\d\-]{2,}\.?)?)\/?[^\s]*";
        public string Value { get; private set; } = "";

        protected UrlDomain()
        {

        }

        public UrlDomain(string url)
        {
            Extract(url);
        }

        private void Extract(string url)
        {
            var regex = new Regex(RegexPattern);
            if (regex.IsMatch(url))
            {
                var groups = regex.Match(url).Groups;
                Value = RemoveTripleW(groups[1].Value);
            }
        }

        private string RemoveTripleW(string value)
        {
            return Regex.Replace(value, @"^[w]{3}\.", "");
        }
    }
}
