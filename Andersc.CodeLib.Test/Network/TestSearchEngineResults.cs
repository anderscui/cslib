using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

using NUnit.Framework;

namespace Andersc.CodeLib.Tester.Network
{
    [TestFixture]
    public class TestSearchEngineResults
    {
        [Test]
        public void TestSearchGoogleRanking()
        {
            // Parameters: hl=language; q=keywords; btnG=action?; start=startIndexFromZero; num=searchResultPageSize.
            string url = "http://www.google.com/search?hl=en-US&q=offshore outsourcing china&btnG=Google+Search&start=0&num=20";

            string response = GetResponseText(url);

            Console.WriteLine("Parse Response Text:");
            //Regex re = new Regex("<li class=g>(.|\\n)+?</div>");
            Regex re = new Regex("<h3 class=\"r\"><a href=\"(.+?)\"");
            MatchCollection searchResults = re.Matches(response);
            foreach (Match m in searchResults)
            {
                string murl = m.Groups[1].Value;
                Console.WriteLine(murl);
            }
        }

        [Test]
        public void TestSearchBingRanking()
        {
            //int num = 20;
            // Parameters: q=keywords; first=startIndexFromOne; count=searchResultPageSize.
            string url = "http://www.bing.com/search?q=offshore+outsourcing+china&first=1&count=20";
            string response = GetResponseText(url);

            Regex re = new Regex("<div class=\"sa_cc\">.+?<a href=\"(.+?)\".+?</div>");
            MatchCollection searchResults = re.Matches(response);
            foreach (Match m in searchResults)
            {
                string murl = m.Groups[1].Value;
                Console.WriteLine(murl);
            }
        }

        [Test]
        public void TestSearchYahooRanking()
        {
            //int num = 20;
            // Parameters: q=keywords; first=startIndexFromOne; count=searchResultPageSize.
            //string url = "http://search.yahoo.com/search;_ylt=AvWHnG0gdICrX.nQaLe.muqbvZx4?vc=&fp_ip=cn&p=china&toggle=1&cop=mss&ei=UTF-8&fr=yfp-t-311";
            string url = "http://search.yahoo.com/search;_ylt=AvWHnG0gdICrX.nQaLe.muqbvZx4?vc=&fp_ip=cn&p=china&pstart=1&b=11&toggle=1&cop=mss&ei=UTF-8&fr=yfp-t-311";
            // When paging, _ylt, xargs, pstart, b, xa change;
            string response = GetResponseText(url);
            //Console.WriteLine(GetResponseText(url));

            Regex re = new Regex("<li><div.+?<a.+?href=\"(.+?)\".+?div></li>");
            MatchCollection searchResults = re.Matches(response);
            foreach (Match m in searchResults)
            {
                string murl = m.Groups[1].Value;
                Console.WriteLine(murl);
            }
        }

        [Test]
        public void TestSearchBaiduRanking()
        {
            // Parameters: wd=keywords; pn=startIndexFromZero; rn=searchResultPageSize.
            // But baidu's machenism is different to those of Google & Bing.
            // The actual start index is (pn / rn) * rn, let rn = 20, if pn = 10, startIndex = 0; if pn = 20, startIndex = 20.
            string url = "http://www.baidu.com/s?wd=offshore+outsourcing+china&pn=0&rn=20";
            string response = GetResponseText(url);
            //Console.WriteLine(response);

            Regex re = new Regex("<table.+?class=\"result\".+?<a.+?href=\"(.+?)\".+?</table>");
            MatchCollection searchResults = re.Matches(response);
            foreach (Match m in searchResults)
            {
                string murl = m.Groups[1].Value;
                Console.WriteLine(murl);
            }
        }

        private static string GetResponseText(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.MaximumAutomaticRedirections = 3;
            request.Timeout = 5000;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string result = reader.ReadToEnd();

            reader.Close();

            return result;
        }
    }
}
