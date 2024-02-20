using Core.Infra.IoC;
using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Domains.Interfaces.Services.WebCrawlerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mockers.Contexts.Crawlers.Infra.Services;
using System.Linq;
using System.Reflection;

namespace Tests.Crawler.Services.WebCrawlers
{
    [TestClass]
    public class UnitTestWebCrawlerService
    {
        private Page Page = PageCreator.Create("http://www.domain.com/");
        private IFolhaWebCrawlerService Service = CreateAndConfigureService();


        [TestMethod]
        public void TestIfCatchAllReferredPages()
        {

            var referralPages = Service.GetReferralsPages(Page);

            Assert.AreEqual(1, referralPages.Count);
            var firstPage = referralPages.First();

            Assert.AreEqual("domain", firstPage.Domain.Name);
        }

        [TestMethod]
        public void TestIfGatchTitleCorrectly()
        {
            var title = Service.GetTitle(Page);
            Assert.AreEqual("Lorem Ipsum", title);
        }

        [TestMethod]
        public void TestIfCatchMetaCorrectly()
        {
            var meta = Service.GetMeta(Page, "test");
            Assert.AreEqual("Lorem Ipsum", meta);
        }

        private static IFolhaWebCrawlerService CreateAndConfigureService()
        {
            var iocMapper = new IocMapper();

            var service = iocMapper.Get<IFolhaWebCrawlerService>();

            var property = service.GetType().BaseType.GetProperty("WebNavigator", BindingFlags.NonPublic | BindingFlags.Instance);
            var webNavigator = (WebNavigatorMock)property.GetValue(service);
            webNavigator.Html = "" +
                "<html>" +
                "<head>" +
                "<meta name=\"test\" content='Lorem Ipsum'>" +
                "<title>Lorem Ipsum</title>" +
                "</head>" +
                "<body>" +
                "<div><a href='https://domain.com'></a></div>" +
                "</body>" +
                "</html>";
            return service;
        }
    }
}
