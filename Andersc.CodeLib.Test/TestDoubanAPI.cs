using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

using NUnit.Framework;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Tester
{
    [TestFixture]
    public class TestDoubanAPI
    {
        private string publicKey = "0ce90f73010630c626bb2e5903129252";
        private string privateKey = "278ab10e364a9098";

        private string albumUrl = "http://www.douban.com/photos/album/";
        private string albumUrlFormat = "http://www.douban.com/photos/album/{0}/?start={1}";
        private string photoUrlFormat = "http://{0}.douban.com/view/photo/photo/public/p{1}.{2}";
        //private string thumbRegex = "<img src=\"http://(?<domain>\\w+).douban.com/view/photo/thumb/public/p(?<id>\\d+)\\.(?<ext>\\w+)\" />";
        //private string thumbRegex = "<div class=\"photo_wrap\">((.|\\n)+?)<img src=\"http://(?<domain>\\w+).douban.com/view/photo/thumb/public/p(?<id>\\d+)\\.(?<ext>\\w+)\" />";
        private string thumbRegex = "<a(.+?)title=\"(?<title>(.|\\n)*?)\">((.|\\n)+?)<img src=\"http://(?<domain>\\w+).douban.com/view/photo/thumb/public/p(?<id>\\d+)\\.(?<ext>\\w+)\" />";
        private string albumNameRegex = "<h(\\d+)>\\s*(?<albumName>(.|\\n)+?)\\s*</h\\1>";
        private string albumDescRegex = "<div.*style=\".*bottom.*\">(?<albumDesc>.*?)</div>";

        // MIMEType: application/x-www-form-urlencoded

        [Test]
        public void TestSendFieldValue()
        {
            string url = "";
        }

        #region User Account

        //GET http://api.douban.com/people/{userID}
        private string URL_FORMAT_FIND_USER = "http://api.douban.com/people/{0}";

        [Test]
        public void TestFindUser()
        {
            string url = string.Format(URL_FORMAT_FIND_USER, "andersc");

            WebClient wc = new WebClient();
            Console.WriteLine("Starting..");
            byte[] res = wc.DownloadData(url);
            string resString = Encoding.UTF8.GetString(res);
            Console.WriteLine("Result: ");
            Console.WriteLine(resString);

            XDocument doc = XDocument.Parse(resString);
            //string intID = doc.Element("entry").Element("id").Value;
            //Console.WriteLine(intID);

            XNamespace ns = "http://www.w3.org/2005/Atom";

            XElement entry = doc.Element(ns + "entry");
            Console.WriteLine(entry.Element(ns + "id").Value);
        }

        #endregion

        [Test]
        public void TestDownloadImage()
        {
            string url = "http://img2.douban.com/view/photo/photo/public/p451334629.jpg";
            string fileName = Path.GetFileName(url);
            WebClient wc = new WebClient();
            Console.WriteLine("Starting..");
            byte[] res = wc.DownloadData(url);
            File.WriteAllBytes(Path.Combine(@"d:\", fileName), res);
        }

        [Test]
        public void TestDoanloadAlbum()
        {
            string rootDir = @"D:\Downloads\图片\Douban\album\";
            int id = 25458784;

            string localAlbumDir = rootDir + id.ToString();
            if (Directory.Exists(localAlbumDir).IsFalse())
            {
                Directory.CreateDirectory(localAlbumDir);
            }

            string infoFilePath = Path.Combine(localAlbumDir, "info.txt");
            using (StreamWriter sw = new StreamWriter(infoFilePath, false))
            {
                // TODO: Read from page content.
                int pageCount = 18;
                int pageIndex = 0;
                int startIndex = 0;
                bool isFirstPage = true;
                do
                {
                    startIndex = pageIndex * pageCount;

                    string pageContent = GetResponseString(albumUrlFormat.FormatWith(id, startIndex));
                    if (pageContent.IsNotNull())
                    {
                        #region Invalid xml format in this page.

                        // Invalid xml format.
                        //XDocument doc = XDocument.Parse(pageContent);
                        //XNamespace ns = "http://www.w3.org/1999/xhtml";

                        //IEnumerable<XElement> address =
                        //    from el in doc.Elements(ns + "div")
                        //    where (string)el.Attribute(ns + "class") == "paginator"
                        //    select el;
                        //Console.WriteLine(address.First()); 

                        #endregion

                        if (isFirstPage)
                        {
                            Match albumNameMatch = Regex.Match(pageContent, albumNameRegex);
                            if (albumNameMatch.IsNotNull())
                            {
                                sw.WriteLine("album: " + albumNameMatch.Groups["albumName"]);
                            }
                            
                            sw.WriteLine("url: " + albumUrl + id.ToString());

                            Match albumDescMatch = Regex.Match(pageContent, albumDescRegex);
                            if (albumDescMatch.IsNotNull())
                            {
                                sw.WriteLine("description: " + albumDescMatch.Groups["albumDesc"]);
                                
                            }
                            sw.WriteLine();

                            isFirstPage = false;
                        }

                        // Use reg ex.
                        MatchCollection thumbs = Regex.Matches(pageContent, thumbRegex);
                        //Console.WriteLine(thumbs.Count);
                        //Assert.IsTrue(thumbs.Count > 0);
                        if (thumbs.Count == 0 || pageIndex >= 10)
                        {
                            break;
                        }

                        foreach (Match thumb in thumbs)
                        {
                            string title = thumb.Groups["title"].Value;
                            string domain = thumb.Groups["domain"].Value;
                            string photoID = thumb.Groups["id"].Value;
                            string extension = thumb.Groups["ext"].Value;
                            string fileName = "p" + photoID + "." + extension;

                            sw.WriteLine(photoID + ":");
                            sw.WriteLine(title);
                            sw.WriteLine(new string('*', 50));
                            sw.WriteLine();

                            DoanloadFile(photoUrlFormat.FormatWith(domain, photoID, extension), Path.Combine(localAlbumDir, fileName));
                        }
                    }

                    pageIndex++;
                } while (true);
            }
        }

        private void DoanloadFile(string url, string localFileName)
        {
            WebClient wc = new WebClient();
            byte[] res = wc.DownloadData(url);
            File.WriteAllBytes(localFileName, res);
        }

        private string GetResponseString(string url)
        {
            WebClient wc = new WebClient();
            byte[] res = wc.DownloadData(url);
            return Encoding.UTF8.GetString(res);
        }
    }
}
