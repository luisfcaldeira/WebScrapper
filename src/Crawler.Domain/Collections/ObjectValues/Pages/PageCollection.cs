using Crawlers.Domains.Entities.ObjectValues.Pages;

namespace Crawlers.Domains.Collections.ObjectValues.Pages
{
    public class PageCollection
    {
        public int Id { get; set; }
        public ICollection<Page> Pages { get; private set; }

        private PageCollection() { }

        public PageCollection(IList<Page> pages, Domain domain)
        {
            Pages = pages.Where(page => page.Domain.Equals(domain)).ToList();
        }
    }
}
