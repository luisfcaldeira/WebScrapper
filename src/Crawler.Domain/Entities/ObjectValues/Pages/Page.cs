using Crawlers.Domains.Collections.ObjectValues.Pages;
using Crawlers.Domains.Entities.Articles;

namespace Crawlers.Domains.Entities.ObjectValues.Pages
{
    public class Page
    {
        public int Id { get; set; }
        public Domain Domain { get; private set; } = new Domain();
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Visited { get; private set; } = null;
        public ICollection<BaseArticle> Articles{ get; set; }
        public string? MessageErro { get; private set; }
        public string? RawUrl { get; internal set; }
        public ICollection<PageCollection> PagesCollections { get; set; }

        public byte[]? ConcurrencyToken { get; set; }
        public int TaskCode { get; set; } = -1;

        public string Url 
        { 
            get
            {
                return Domain.FullUrl();
            }
        }

        public bool IsVisited 
        { 
            get
            {
                return Visited != null;
            } 
        }


        protected Page()
        {
            Articles = new List<BaseArticle>();
            PagesCollections = new List<PageCollection>();
        }

        public Page(Domain domain) : this()
        {
            Domain = domain;
        }

        public bool IsValid(Domain otherDomain)
        {
            return otherDomain.Equals(Domain);
        }

        public void Visit()
        {
            Visited = DateTime.Now;
        }

        public void Update(Page page)
        {
            Created = page.Created;
            Visited = page.Visited;
        }

        public void InformError(Exception ex)
        {
            MessageErro = ex.Message;
        }

        public bool HasErro()
        {
            return MessageErro != null && !MessageErro.Equals(string.Empty);
        }

        public override string? ToString()
        {
            return Domain.ToString();
        }

        public override bool Equals(object? obj)
        {
            if(obj is Page page)
            {
                return Id == page.Id || Url == page.Url;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Url);
        }
    }
}
