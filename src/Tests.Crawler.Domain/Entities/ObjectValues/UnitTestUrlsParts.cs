using Crawlers.Domain.Entities.ObjectValues.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void TEstIfUrlProtocolIsWellFormed()
        {
            var expectedResult = "http";

            var protocol1 = new UrlProtocol("http://www.domain.com.br/index.html?variable1=999&variable2=abcd");
            
            Assert.AreEqual(expectedResult, protocol1.Value);

            var protocol2 = new UrlProtocol("https://www.domain.com.br/index.html?variable1=999&variable2=abcd");

            Assert.AreEqual("https", protocol2.Value);
        }
    }
}