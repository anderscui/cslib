using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Network
{
    public class Browser
    {
        public static readonly Browser Firefox28;
        public static readonly Browser Chrome26;

        static Browser()
        {
            Firefox28 = new Browser()
            {
                Name = "Firefox",
                Version = "28.0",
                UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:28.0) Gecko/20100101 Firefox/28.0",

                Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",
                AcceptEncoding = "gzip, deflate",
                AcceptLanguage = "en-us,zh-cn;q=0.8,zh;q=0.5,en;q=0.3"
            };

            Chrome26 = new Browser()
            {
                Name = "Chrome",
                Version = "26.0",
                UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.31 (KHTML, like Gecko) Chrome/26.0.1410.64 Safari/537.31",

                Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",
                AcceptEncoding = "gzip,deflate,sdch",
                AcceptLanguage = "en-US,en;q=0.8,zh-CN;q=0.6,zh;q=0.4",
            };
        }

        public string Name { get; set; }
        public string Version { get; set; }
        public string UserAgent { get; set; }

        public string Accept { get; set; }
        public string AcceptEncoding { get; set; }
        public string AcceptLanguage { get; set; }
    }
}
