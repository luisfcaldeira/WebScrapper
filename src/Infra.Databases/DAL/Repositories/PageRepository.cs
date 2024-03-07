using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Domains.Interfaces.DAL.Repositories;
using Crawlers.Infra.Databases.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;

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
                          WHERE VISITED IS NULL
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

            } catch(Exception)
            {

            }

        }

        public override void Update(Page entity)
        {
            var visited = "NULL";

            if (entity.Visited != null)
            {
                var dateStr = entity.Visited.Value.ToString("yyyy-MM-dd HH:mm:ss");
                visited = $"'{dateStr}'";
            }

            var concurrencyToken = Convert.ToHexString(entity.ConcurrencyToken);
            concurrencyToken = ConvertToSqlVarBinary(concurrencyToken);
            
            var query = @$"  
                          UPDATE [Crawlers].[Page]
                          SET [Domain_Protocol_Value] = '{entity.Domain.Protocol.Value}'
                              ,[Domain_Subdomain_Value] = '{entity.Domain.Subdomain.Value}'
                              ,[Domain_Name] = '{entity.Domain.Name}'
                              ,[Domain_TopLevel] = '{entity.Domain.TopLevel}'
                              ,[Domain_Country_Value] = '{entity.Domain.Country.Value}'
                              ,[Domain_Directory_Value] = '{entity.Domain.Directory.Value}'
                              ,[Created] = '{entity.Created}'
                              ,[Visited] = {visited}
                              ,[MessageErro] = '{entity.MessageErro?.Replace("'", "\"")}'
                              ,[RawUrl] = '{entity.RawUrl}'
                              ,[TaskCode] = '{entity.TaskCode}'
                          WHERE ConcurrencyToken = {concurrencyToken}
                          AND ID = '{entity.Id}'";

            
            var affectedRows = DbContext.Database.ExecuteSqlRaw(query);

            if(affectedRows == 0)
            {
                throw new Exception("This page was taken by other task.");
            }
        }

        private string ConvertToSqlVarBinary(string concurrencyToken)
        {
            var result = "";
            var x = "x";
            foreach(var c in concurrencyToken)
            {
                result += c + x;
                x = "";
            }

            return result;
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

        public IEnumerable<Page> GetNonVisitedRegisteredForTask(int taskCode)
        {
            return GetAll().Where(url => !url.IsVisited && url.TaskCode == taskCode);
        }
    }
}
