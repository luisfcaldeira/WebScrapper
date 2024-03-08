using Core.Infra.Services.Observers.Entities.Messages;
using Crawlers.Domains.Entities.ObjectValues.Pages;
using Crawlers.Infra.WebScrapperServices.Interfaces.InnerServices;
using HtmlAgilityPack;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Crawlers.Infra.WebScrapperServices.Services
{
    public class WebNavigator : IWebNavigator
    {
        public HtmlWeb htmlWeb;

        public WebNavigator(HtmlWeb htmlWeb)
        {
            this.htmlWeb = htmlWeb;
        }

        public void Dispose()
        {
        }

        public HtmlDocument GetDocument(Page page)
        {
            htmlWeb.PreRequest = request =>
            {

                //request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7";
                //request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36";
                request.AutomaticDecompression = DecompressionMethods.All;

                //request.CookieContainer = new CookieContainer();
                ////request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br, zstd");
                //request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.9,pt-BR;q=0.8,pt;q=0.7");
                //request.Headers.Add(HttpRequestHeader.CacheControl, "max-age=0");
                //request.Referer = page.Domain.ToString();

                return true;

            };
            htmlWeb.AutoDetectEncoding = true;
            var document = htmlWeb.Load(page.Url);

            return document;
        }
    }
}
