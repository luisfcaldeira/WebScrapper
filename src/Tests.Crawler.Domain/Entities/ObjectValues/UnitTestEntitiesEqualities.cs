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
        public void TestIfCreateValidArticle()
        {
            var page1 = PageCreator.Create("http://domain.com");
            var article1 = new Article(
                    "Lorem Ipsum", "Content", page1, DateTime.Today
                );

            Assert.IsTrue(article1.IsValid);
        }

        [TestMethod]
        public void TestIfCreateInvalidArticle()
        {
            var page1 = PageCreator.Create("http://domain.com");
            var article1 = new Article(
                    string.Empty, string.Empty, page1, DateTime.Today
                );

            var article2 = new Article(
                    "Title", string.Empty, page1, DateTime.Today
                );

            var article3 = new Article(
                    string.Empty, "Content", page1, DateTime.Today
                );
            Assert.IsFalse(article1.IsValid);
            Assert.IsFalse(article2.IsValid);
            Assert.IsFalse(article3.IsValid);
        }

        [TestMethod]
        public void TestIfFolhaArticleIsEqualsAnother()
        {
            var page1 = PageCreator.Create("http://domain.com");
            var page2 = PageCreator.Create("http://domain.com");
            var page3 = PageCreator.Create("http://domain1.com.br");

            page1.Id = 1;
            page2.Id = 2;
            page3.Id = 2;

            var article = new Article(
                    "Lorem Ipsum", "Content", page1, DateTime.Today
                );

            var article2 = new Article(
                    "Lorem Ipsum", "Content", page2, DateTime.Today
                );

            var article3 = new Article(
                    "Lorem Ipsum 1", "Content 123" , page3, DateTime.Today
                );

            article.Id = 1;
            article2.Id = 2;
            article3.Id = 3;

            Assert.AreEqual(article, article2);
            Assert.AreNotEqual(article, article3);
        }
    }
}
