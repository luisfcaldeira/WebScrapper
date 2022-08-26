using Crawlers.Domain.Entities.Interfaces.ObjectValues.Urls;
using System.Text.RegularExpressions;

namespace Crawlers.Domain.Entities.ObjectValues.Urls.ChainOfResponsability
{
    public abstract class AbstractUrlHandler
    {
        protected abstract string Pattern { get; }
        private IUrlHandler _nextHandler;

        public IUrlHandler SetNext(IUrlHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual Url Handle(string request)
        {
            var regex = new Regex(Pattern);

            if (regex.IsMatch(request.ToString()))
            {
                return new Url(request);
            } 
            return _nextHandler.Handle(request);
        }
    }
}
