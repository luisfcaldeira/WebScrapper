using Crawlers.Domains.Entities.Interfaces.ObjectValues.Urls;
using System.Text.RegularExpressions;

namespace Crawlers.Domains.Entities.ObjectValues.Urls.ChainOfResponsability
{
    internal abstract class AbstractUrlHandler
    {
        protected abstract string Pattern { get; }
        private IUrlHandler _nextHandler;

        public IUrlHandler SetNext(IUrlHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual Page Handle(string request)
        {
            var regex = new Regex(Pattern);

            if (regex.IsMatch(request.ToString()))
            {
                return CreateUrl(request, regex.Match(request).Groups);
            } 
            return _nextHandler.Handle(request);
        }

        protected abstract Page CreateUrl(string request, GroupCollection groups);
    }
}
