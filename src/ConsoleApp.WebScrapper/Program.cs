using Crawler.Infra.Databases.Context;
using Crawler.Infra.Databases.DAL.Repositories;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.WebScrapper
{
    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello World");

            var repository = new UrlRepository(new CrawlerDbContext(@"Server=(localdb)\mssqllocaldb;Database=Test"));
            var dados = repository.GetAll();

            HtmlWeb web = new HtmlWeb();
            var htmlNewDoc = new HtmlDocument();
            HtmlDocument doc = web.Load("https://www.folha.uol.com.br/");
            
            //var headerNames = doc.DocumentNode.SelectNodes("//span[@class='toctext']");
            var title = doc.DocumentNode.SelectNodes("//head/title").First();
            // https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding.getencodings?view=net-6.0#system-text-encoding-getencodings
            byte[] bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(title.InnerText);
            var nameFixed = Encoding.UTF8.GetString(bytes);

            Console.WriteLine(nameFixed);
            Console.ReadKey();
        }
    }
}
