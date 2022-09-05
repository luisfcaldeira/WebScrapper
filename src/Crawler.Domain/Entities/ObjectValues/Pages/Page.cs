using Crawlers.Domains.Entities.Articles;

namespace Crawlers.Domains.Entities.ObjectValues.Pages
{
    public class Page
    {
        public int Id { get; set; }
        public Domain Domain { get; private set; }
        public string Url { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Visited { get; private set; }
        public IEnumerable<BaseArticle> Articles{ get; set; }

        public bool IsVisited 
        { 
            get
            {
                return Visited != null;
            } 
        }

        protected Page()
        {
            Domain = new Domain();  
        }

        public Page(Domain domain, string url) : this()
        {
            Domain = domain;
            Url = url;
        }

        public bool IsValid(Domain otherDomain)
        {
            return otherDomain.Equals(Domain);
        }

        public override string? ToString()
        {
            return Domain.ToString();
        }

        public void Visit()
        {
            Visited = DateTime.Now;
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

        public void Update(Page page)
        {
            Created = page.Created;
            Visited = page.Visited;
        }
    }
}
