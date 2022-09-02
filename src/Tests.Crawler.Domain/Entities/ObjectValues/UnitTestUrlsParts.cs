using Crawlers.Domains.Entities.ObjectValues.Urls;
using Crawlers.Domains.Exceptions.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Crawler.Domains.Entities.ObjectValues
{
    [TestClass]
    public class UnitTestUrlsParts
    {

        [TestMethod]
        public void TestFolhasUrl()
        {
            var urlStr = "https://www1.folha.uol.com.br/poder/2022/08/lula-informa-ao-tse-ter-criado-redes-sociais-direcionadas-a-evangelicos.shtml";
            var url = PageCreator.Create(urlStr);

            Assert.AreEqual("folha.uol", url.Domain.Name);
            Assert.AreEqual("www1", url.Domain.Subdomain.Value);
        }


        [TestMethod]
        public void TestGloboG1Url()
        {
            var urlStr = "https://g1.globo.com/pop-arte/noticia/2022/08/27/finalista-do-miss-inglaterra-se-torna-primeira-a-disputar-concurso-sem-maquiagem.ghtml";
            var url = PageCreator.Create(urlStr);

            Assert.AreEqual("globo", url.Domain.Name);
            Assert.AreEqual("g1", url.Domain.Subdomain.Value);
            Assert.AreEqual("com", url.Domain.TopLevel);
        }


        [TestMethod]
        public void TestDirectory()
        {
            var urlStr = "https://g1.globo.com/pop-arte/noticia/2022/08/27/finalista-do-miss-inglaterra-se-torna-primeira-a-disputar-concurso-sem-maquiagem.ghtml";
            var url = PageCreator.Create(urlStr);

            Assert.AreEqual("pop-arte/noticia/2022/08/27/finalista-do-miss-inglaterra-se-torna-primeira-a-disputar-concurso-sem-maquiagem.ghtml", url.Domain.Directory.Value);
        }

        [TestMethod]
        public void TestIfUrlDomainIsWellFormed()
        {
            var url = PageCreator.Create("domain.com");
            Assert.AreEqual("domain", url.Domain.Name);

            var url2 = PageCreator.Create("http://www.domain.com.br");
            Assert.AreEqual("domain", url2.Domain.Name);            
            
            var url3 = PageCreator.Create("http://www.domain.com.br/index.html?variable1=999&variable2=abcd");
            Assert.AreEqual("domain", url3.Domain.Name);

            Assert.ThrowsException<NotWellFormedUrlException>(() => {
                var url3 = PageCreator.Create("www notwellformed.url");
            });
        }

        [TestMethod]
        public void TestIfUrlProtocolIsWellFormed()
        {
            var expectedResult = "http";

            var protocol1 = new Protocol("http://www.domain.com.br/index.html?variable1=999&variable2=abcd");
            
            Assert.AreEqual(expectedResult, protocol1.Value);

            var protocol2 = new Protocol("https://www.domain.com.br/index.html?variable1=999&variable2=abcd");

            Assert.AreEqual("https", protocol2.Value);

            var protocol3 = new Protocol("https");

            Assert.AreEqual("https", protocol3.Value);

            var protocol4 = new Protocol("https://");

            Assert.AreEqual("https", protocol4.Value);
        }

        [TestMethod]
        public void TestIfUrlDomainIsEqualsOthers()
        {
            var domain1 = new Domain("domain", "com") 
            {
                Country = new Country("br"),
                Protocol = new Protocol("http://")
            };

            var domain2 = new Domain("domain", "com")
            {
                Country = new Country("br"),
                Protocol = new Protocol("https://")
            };

            Assert.AreEqual(domain1, domain2);
        }

        [TestMethod]
        public void TestIfCountryIsRight()
        {
            var country = new Country("xyz");

            Assert.AreEqual("xyz", country.Value);
        }

        [TestMethod]
        public void TestIfExcludSlash()
        {
            var url1 = PageCreator.Create("https://www.domain.com.br/");
            var url3 = PageCreator.Create("//www.domain.com.br/index.html?variable1=999&variable2=abcd");
            Assert.AreEqual("domain", url1.Domain.Name);
            Assert.AreEqual("www", url1.Domain.Subdomain.Value);
            Assert.AreEqual("www", url3.Domain.Subdomain.Value);
        }
    }
}