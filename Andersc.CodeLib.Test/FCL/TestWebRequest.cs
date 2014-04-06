using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using Andersc.CodeLib.Common.Network;
using NUnit.Framework;

namespace Andersc.CodeLib.Tester.FCL
{
    [TestFixture]
    public class TestWebRequest
    {
        private string GetResponseText()
        {
            //var url = "http://www.douban.com/photos/album/102853708";
            var url = "http://www.codeproject.com/Questions/204778/Get-HTML-code-from-a-website-C";
            WebClient wc = new WebClient();
            byte[] res = wc.DownloadData(url);
            return Encoding.UTF8.GetString(res);
        }

        private string GetResponseText2()
        {
            var url = "http://www.douban.com/photos/album/102853708";

            var req = (HttpWebRequest) WebRequest.Create(url);
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:28.0) Gecko/20100101 Firefox/28.0";

            var resp = (HttpWebResponse) req.GetResponse();

            StreamReader readStream = null;
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                var receivedStream = resp.GetResponseStream();
                
                if (resp.CharacterSet == null)
                {
                    readStream = new StreamReader(receivedStream);
                }
                else
                {
                    readStream = new StreamReader(receivedStream, Encoding.GetEncoding(resp.CharacterSet));
                }
            }

            var text = readStream.ReadToEnd();
            resp.Close();
            readStream.Close();

            return text;
        }

        [Test]
        public void GetHtmlByWebClient()
        {
            Console.WriteLine(GetResponseText());
        }

        [Test]
        public void GetHtmlByWebClient2()
        {
            var url = "http://www.douban.com/photos/album/102853708";
            Console.WriteLine(HttpRequestHelper.GetResponseText(url));
        }

        [Test]
        public void GetHtmlByWebRequest()
        {
            Console.WriteLine(GetResponseText2());
        }

        [Test]
        public void GetHtmlByWebRequestViaFirefox()
        {
            var url = "http://www.douban.com/photos/album/102853708/?start=0";
            Console.WriteLine(HttpRequestHelper.GetResponseText(url, Browser.Firefox28));
        }

        [Test]
        public void GetHtmlByWebRequestViaChrome()
        {
            var url = "http://www.douban.com/photos/album/102853708/?start=0";
            Console.WriteLine(HttpRequestHelper.GetResponseText(url, Browser.Chrome26));
        }
    }
}
