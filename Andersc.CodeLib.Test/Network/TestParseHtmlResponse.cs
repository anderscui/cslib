using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

using NUnit.Framework;
using System.Xml;

namespace Andersc.CodeLib.Tester.Network
{
    public class RssFeed
    {
        public DateTime PubDate { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Link { get; set; }
    }

    [TestFixture]
    public class TestParseHtmlResponse
    {
        private static readonly string DefaultTimeZone = "+0800";

        public static string GetResponseText(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";

            request.MaximumAutomaticRedirections = 5;
            request.Timeout = 1000 * 6;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string result = reader.ReadToEnd();

            reader.Close();

            return result;
        }

        private List<string> GetPressReleaseLists()
        {
            List<string> pressReleaseLists = new List<string>();
            pressReleaseLists.Add("http://www.bleum.com/news-research/press-release/press-release-2009.shtml");
            pressReleaseLists.Add("http://www.bleum.com/news-research/press-release/press-release-2010.shtml");
            pressReleaseLists.Add("http://www.bleum.com/news-research/press-release/press-release.shtml");

            return pressReleaseLists;
        }

        public static string RemoveHtmlTags(string html)
        {
            string result = html;
            Regex regex = new Regex(@"<[^>]+>|</[^>]+>");

            return regex.Replace(result, string.Empty);
        }

        [Test]
        public void TestPressRelease()
        {
            List<string> newsUrls = new List<string>();
            foreach (string pressReleaseListUrl in GetPressReleaseLists())
            {
                //Console.WriteLine(pressReleaseListUrl);
                string response = GetResponseText(pressReleaseListUrl);

                string listPattern = "<ul class=\"press-release-list\">((.|\\n)+?)</ul>";
                if (Regex.IsMatch(response, listPattern))
                {
                    //Console.WriteLine("List: ");
                    //Console.WriteLine(Regex.Match(response, listPattern).Groups[1].Value);

                    string linkList = Regex.Match(response, listPattern).Groups[1].Value;
                    string linkPattern = "<a href=\"(?<url>.*?)\"";
                    foreach (Match m in Regex.Matches(linkList, linkPattern))
                    {
                        //Console.WriteLine(m.Groups["url"].Value);
                        string url = m.Groups["url"].Value;
                        if (url[0] == '/')
                        {
                            newsUrls.Add("http://www.bleum.com" + url);
                        }
                        else
                        {
                            newsUrls.Add("http://www.bleum.com/" + url);
                        }
                    }
                }
            }

            Console.WriteLine("Found {0} Links.", newsUrls.Count);

            List<RssFeed> feeds = new List<RssFeed>();

            string newsPattern = "<div class=\"press-release-date\"><b>Date:\\s*</b>(?<pubDate>.+?)</div>\\s*"
                    + "<div class=\"press-release-title\">(?<title>.+?)</div>\\s*"
                    + "<p>(?<abstract>(.|\\n)+?)</p>";
            foreach (string newsUrl in newsUrls)
            {
                string newsHtml = GetResponseText(newsUrl);
                if (Regex.IsMatch(newsHtml, newsPattern))
                {
                    Match m = Regex.Match(newsHtml, newsPattern);
                    RssFeed reed = new RssFeed()
                    {
                        Link = newsUrl,
                        PubDate = Convert.ToDateTime(m.Groups["pubDate"].Value.Trim()),
                        Title = RemoveHtmlTags(m.Groups["title"].Value).Trim(),
                        Abstract = RemoveHtmlTags(m.Groups["abstract"].Value).Trim()
                    };
                    feeds.Add(reed);
                }
                else
                {
                    Console.WriteLine("Not match: {0}", newsUrl);
                }
            }

            Console.WriteLine("Got {0} Feeds.", feeds.Count);
            //foreach (RssFeed reed in feeds)
            //{
            //    Console.WriteLine("pubDate: {0}", reed.PubDate);
            //    Console.WriteLine("title: {0}", reed.Title);
            //    Console.WriteLine("abstract: {0}", reed.Abstract);
            //}

            Comparison<RssFeed> comp = delegate(RssFeed first, RssFeed second)
            { 
                return -Comparer<DateTime>.Default.Compare(first.PubDate, second.PubDate);
            };
            feeds.Sort(comp);
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", "yes"));
            XmlElement root = doc.CreateElement("rss");
            XmlAttribute rootVer = doc.CreateAttribute("version");
            rootVer.Value = "2.0";
            root.Attributes.Append(rootVer);
            XmlAttribute rootNs = doc.CreateAttribute("xmlns:trackback");
            rootNs.Value = "http://madskills.com/public/xml/rss/module/trackback/";
            root.Attributes.Append(rootNs);

            XmlElement channel = doc.CreateElement("channel");
            root.AppendChild(channel);

            XmlElement channelTitle = doc.CreateElement("title");
            channelTitle.InnerText = "Software Outsourcing, Outsourcing china, Offshore Development, Offshore Software Development Center, Offshore Outsourcing in Shanghai China - Bleum";
            channel.AppendChild(channelTitle);
            XmlElement channelLink = doc.CreateElement("link");
            channelLink.InnerText = "http://www.bleum.com";
            channel.AppendChild(channelLink);
            XmlElement channelDescription = doc.CreateElement("description");
            channelDescription.InnerText = "Bleum is one of China's leading IT and software outsourcing providers in a variety of sectors including high-tech, financial services, telecommunications, and retail. We create global development centers and provide services such as application development, support and maintenance, testing, and legacy system modernization.";
            channel.AppendChild(channelDescription);

            foreach (RssFeed feed in feeds)
            {
                XmlElement item = doc.CreateElement("item");

                XmlElement title = doc.CreateElement("title");
                title.InnerText = feed.Title;
                item.AppendChild(title);
                XmlElement link = doc.CreateElement("link");
                link.InnerText = feed.Link;
                item.AppendChild(link);
                XmlElement description = doc.CreateElement("description");
                description.InnerText = feed.Abstract;
                item.AppendChild(description);
                XmlElement pubDate = doc.CreateElement("pubDate");
                pubDate.InnerText = feed.PubDate.ToString("ddd, dd MMM yyyy HH:mm:ss ") + DefaultTimeZone;
                item.AppendChild(pubDate);

                channel.AppendChild(item);
            }
            doc.AppendChild(root);
            doc.Save(String.Format("rss-{0}.xml", DateTime.Now.ToFileTime()));
        }
    }
}
