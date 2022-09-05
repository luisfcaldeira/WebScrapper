using Crawlers.Domains.Interfaces.Entities.ObjectValues.Pages;
using System.Text.RegularExpressions;

namespace Crawlers.Domains.Entities.ObjectValues.Pages.ChainOfResponsability
{
    internal abstract class AbstractUrlHandler
    {
        protected abstract string Pattern { get; }
        private IPageHandler _nextHandler;

        public IPageHandler SetNext(IPageHandler handler)
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
