using Crawlers.Domains.Entities.Articles;
using Crawlers.Domains.Entities.ObjectValues.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Crawler.Domains.Entities.ObjectValues
{
    [TestClass]
    public class UnitTestEntitiesEqualities
    {
        [TestMethod]
        public void TestIfFolhaArticleIsEqualsAnother()
        {
            var page1 = PageCreator.Create("http://domain.com");
            var page2 = PageCreator.Create("http://domain.com");
            var page3 = PageCreator.Create("http://domain1.com.br");

            page1.Id = 1;
            page2.Id = 2;
            page3.Id = 2;

            var folha1 = new FolhaArticle(
                    "Lorem Ipsum", "Content", page1, DateTime.Today
                );

            var folha2 = new FolhaArticle(
                    "Lorem Ipsum", "Content", page2, DateTime.Today
                );

            var folha3 = new FolhaArticle(
                    "Lorem Ipsum 1", "Content 123" , page3, DateTime.Today
                );

            folha1.Id = 1;
            folha2.Id = 2;
            folha3.Id = 3;

            Assert.AreEqual(folha1, folha2);
            Assert.AreNotEqual(folha1, folha3);
        }
    }
}
