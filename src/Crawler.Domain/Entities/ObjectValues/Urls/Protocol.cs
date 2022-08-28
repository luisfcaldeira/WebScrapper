using System.Text.RegularExpressions;

namespace Crawlers.Domains.Entities.ObjectValues.Urls
{
    public class Protocol
    {
        public const string RegexPattern = @"([htps]{4,5})\:\/\/";
        public string Value { get; private set; } = string.Empty;

        public Protocol()
        {
            Value = "http";
        }

        public Protocol(string urlOrProtocol) : this()
        {
            if(Regex.IsMatch(urlOrProtocol, RegexPattern))
            {
                var match = Regex.Match(urlOrProtocol, RegexPattern);
                var groups = match.Groups;
                Value = groups[1].Value;
            }
        }

        public override string? ToString()
        {
            return Value + "://";
        }

        public override bool Equals(object? obj)
        {
            return obj is Protocol protocol &&
                   Value == protocol.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }
}
