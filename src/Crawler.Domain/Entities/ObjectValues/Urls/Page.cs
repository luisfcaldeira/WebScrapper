﻿namespace Crawlers.Domains.Entities.ObjectValues.Urls
{
    public class Page
    {
        public int Id { get; private set; }
        public Domain Domain { get; private set; }
        public string Url { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Visited { get; private set; }

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
    }
}
