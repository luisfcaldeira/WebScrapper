using Core.Infra.IoC;
using Crawlers.Domains.Entities.ObjectValues.Pages.Builders;
using Crawlers.Domains.Interfaces.Services.WebCrawlerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Tests.Crawler.Services.WebCrawlers
{
    [TestClass]
    public class UnitTestFolhaWebCrawlerService
    {
        [TestMethod]
        public void TestIfItReadBody()
        {
            var iocMapper = new IocMapper();

            var service = iocMapper.Get<IFolhaWebCrawlerService>();

            var page = PageBuilder.With("folha.uol", "com")
                .WithUrl("https://www1.folha.uol.com.br/ilustrada/2022/02/novelas-sao-maniqueistas-diz-bruno-gagliasso-que-vive-um-traficante-no-streaming.shtml")
                .WithSubdomain("www1")
                .WithDirectory("/poder/2024/02/aliados-de-peso-de-bolsonaro-desistem-ou-silenciam-sobre-ir-a-ato-na-paulista-veja-lista.shtml")
                .WithProtocol("https")
                .WithCountry("br")
                .Create();

            Debug.WriteLine(page.Domain.FullUrl());


            var content = service.GetContent(page);

            Debug.Write("Content: ");
            Debug.WriteLine(content);


            Assert.IsTrue(content != "");
        }
    }
}
