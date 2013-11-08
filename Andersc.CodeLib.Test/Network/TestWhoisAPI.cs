using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common.Network;

namespace Andersc.CodeLib.Tester.Network
{
    [TestFixture]
    public class TestWhoisAPI
    {
        private static readonly string ApiFormat = 
            "http://www.whoisxmlapi.com/whoisserver/WhoisService?domainName={0}&username={1}&password={2}";

        private string userName = "probleum";
        private string pwd = "201314";

        [Test]
        public void TestDomainInfo()
        {
            string url = string.Format(ApiFormat, "210.13.118.58", userName, pwd);
            string response = HttpRequestHelper.GetResponseText(url);
            Console.WriteLine(response);
        }
    }
}
