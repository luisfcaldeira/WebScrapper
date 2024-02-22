using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Domains.Interfaces.DAL.Repositories;
using Crawlers.Infra.Databases.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Crawlers.Infra.Databases.DAL.Repositories
{
    public class PageRepository : RepositoryBase<Page>, IPageRepository
    {
        public PageRepository(CrawlerDbContext dbContext) : base(dbContext)
        {
        }

        public Page? GetPage(string url)
        {
            return GetAll().Where(u => u.RawUrl == url).FirstOrDefault();    
        }

        public IEnumerable<Page> GetAllNotVisited()
        {
            return GetAll().Where(url => !url.IsVisited && url.TaskCode == -1);
        }

        public Page? GetOneNotVisited()
        {
            return GetNonVisited(1).FirstOrDefault();
        }

        public bool Exists(Page page)
        {
            return GetPage(page.Url) != null;
        }

        public IEnumerable<Page> GetNonVisited(int quantity)
        {
            return DbSet.FromSqlRaw(@$"SELECT TOP ({quantity}) [Id]
                              ,[Domain_Protocol_Value]
                              ,[Domain_Subdomain_Value]
                              ,[Domain_Name]
                              ,[Domain_TopLevel]
                              ,[Domain_Country_Value]
                              ,[Domain_Directory_Value]
                              ,[Created]
                              ,[Visited]
                              ,[MessageErro]
                              ,[RawUrl]
                              ,[ConcurrencyToken]
                              ,[TaskCode]
                          FROM [Crawlers].[Page]
                          ORDER BY NEWID()");

        }

        public void Insert(Page page)
        {
            var query = "" +
                @$"INSERT INTO CRAWLERS.PAGE (
                    [Domain_Protocol_Value]
                  , [Domain_Subdomain_Value]
                  , [Domain_Name]
                  , [Domain_TopLevel]
                  , [Domain_Country_Value]
                  , [Domain_Directory_Value]
                  , [Created]
                  , [Visited]
                  , [MessageErro]
                  , [RawUrl]
                  , [TaskCode]) VALUES (
                    '{page.Domain.Protocol.Value}',
                    '{page.Domain.Subdomain.Value}',
                    '{page.Domain.Name}',
                    '{page.Domain.TopLevel}',
                    '{page.Domain.Country.Value}',
                    '{page.Domain.Directory.Value}',
                    '{page.Created}',
                    NULL,
                    NULL,
                    '{page.RawUrl}'
                    ,-1)"
                    ;
            try
            {
                DbContext.Database.ExecuteSqlRaw(query);

            } catch(Exception e)
            {

            }

        }

        public void RemoveDuplicity()
        {
            using (var trans = DbContext.Database.BeginTransaction())
            {
                var query = @"DELETE T
                            FROM
                            (
                            SELECT *
                            , DupRank = ROW_NUMBER() OVER (
                                            PARTITION BY RawUrl
                                            ORDER BY (SELECT NULL)
                                        )
                            FROM [Crawlers].[Page]
                            ) AS T
                            WHERE DupRank > 1 
                        ";

                DbContext.Database.ExecuteSqlRaw(query);

                trans.Commit();
            }

        }
    }
}
