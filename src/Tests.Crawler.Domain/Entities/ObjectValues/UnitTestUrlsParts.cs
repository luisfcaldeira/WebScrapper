using Crawlers.Domain.Entities.ObjectValues.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests.Crawler.Domain.Entities.ObjectValues
{
    [TestClass]
    public class UnitTestUrlsParts
    {
        [TestMethod]
        public void TestIfUrlDomainIsWellFormed()
        {
            var domain = new UrlDomain("www.domain.com");
            Assert.AreEqual("domain.com", domain.Value);

            var domain2 = new UrlDomain("http://www.domain.com.br");
            Assert.AreEqual("domain.com.br", domain2.Value);            
            
            var domain3 = new UrlDomain("http://www.domain.com.br/index.html?variable1=999&variable2=abcd");
            Assert.AreEqual("domain.com.br", domain3.Value);
        }

        [TestMethod]
        public void TestIfUrlProtocolIsWellFormed()
        {
            var expectedResult = "http";

            var protocol1 = new UrlProtocol("http://www.domain.com.br/index.html?variable1=999&variable2=abcd");
            
            Assert.AreEqual(expectedResult, protocol1.Value);

            var protocol2 = new UrlProtocol("https://www.domain.com.br/index.html?variable1=999&variable2=abcd");

            Assert.AreEqual("https", protocol2.Value);
        }

        [TestMethod]
        public void TestIfUrlDomainIsEqualsOthers()
        {
            var domain1 = new UrlDomain("http://domain.com");
            var domain2 = new UrlDomain("https://domain.com");
            var domain3 = new UrlDomain("https://www.domain.com");
            var domain4 = new UrlDomain("http://www.domain.com");
            var domain5 = new UrlDomain("http://www.domain.com.br");
            var domain6 = new UrlDomain("http://another.domain.com");
            var domain7 = new UrlDomain("http://uglydomain.com");

            Assert.AreEqual(domain1, domain2);
            Assert.AreEqual(domain1, domain3);
            Assert.AreEqual(domain1, domain4);
            Assert.AreNotEqual(domain1, domain5);
            Assert.AreEqual(domain1, domain6);
            Assert.AreNotEqual(domain1, domain7);
        }

        [TestMethod]
        public void TestIfDomainIsInList()
        {
            var domains = new List<UrlDomain>()
            {
                new UrlDomain("http://domain.com"),
                new UrlDomain("https://domain.com"),
                new UrlDomain("https://www.domain.com"),
                new UrlDomain("http://www.domain.com"),
                new UrlDomain("http://another.domain.com"),
            };

            var url = new Url("https://domain.com/index.html?param1=1&param2=lorem%20ipsum");
            var anotherUrl = new Url("https://another.domain.com/index.html?param1=1&param2=lorem%20ipsum");
            var falseUrl = new Url("https://false.notdomain.com/index.html?param1=1&param2=lorem%20ipsum");

            Assert.IsTrue(domains.Contains(anotherUrl.Domain));
            Assert.IsTrue(domains.Contains(url.Domain));
            Assert.IsFalse(domains.Contains(falseUrl.Domain));
        }

        [TestMethod]
        public void TestIfPartsFromUrlAreCorrect()
        {
            var url = new Url("http://www.domain.com.br");
            Assert.AreEqual(3, url.Domain.Parts.Length);
            Assert.AreEqual("domain", url.Domain.Parts[0]);
            Assert.AreEqual("com", url.Domain.Parts[1]);
            Assert.AreEqual("br", url.Domain.Parts[2]);
        }

    }
}