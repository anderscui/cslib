using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common.Network;

namespace Andersc.CodeLib.Tester.Network
{
    [TestFixture]
    public class TestIpInfoDb
    {
        private static readonly string ApiFormat =
            "http://api.ipinfodb.com/v3/ip-city/?key=4d11ad3f488d8d2a8f497069afd940a5a7be0d7fc940cbdb64b4e396fe4b29c2&ip={0}&format={1}";

        private string userName = "andersc";
        private string pwd = "201314";
        private string fmt = "xml";

        [Test]
        public void TestDomainInfo()
        {
            string url = string.Format(ApiFormat, "210.13.118.58", fmt);
            string response = HttpRequestHelper.GetResponseText(url);
            Console.WriteLine(response);
        }
    }
}
