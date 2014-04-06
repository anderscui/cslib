using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Andersc.CodeLib.Common.Network
{
    public static class HttpRequestHelper
    {
        public static string GetResponseText(string url, Browser browser = null, string httpMethod = "GET")
        {
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = httpMethod;
            if (browser.IsNull())
            {
                browser = Browser.Firefox28;
            }
            request.UserAgent = browser.UserAgent;
            //request.Accept = browser.Accept;
            //request.Headers.Add(HttpRequestHeader.AcceptEncoding, browser.AcceptEncoding);
            //request.Headers.Add(HttpRequestHeader.AcceptLanguage, browser.AcceptLanguage);
            // request Charset.

            // TODO: Find suitable autoRedirections & timeout.
            request.MaximumAutomaticRedirections = 5;
            request.Timeout = 1000 * 6;

            using (var resp = request.GetResponse() as HttpWebResponse)
            {
                var reader = resp.CharacterSet.IsNull()
                        ? new StreamReader(resp.GetResponseStream()) 
                        : new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding(resp.CharacterSet));

                var result = reader.ReadToEnd();
                reader.Close();

                return result;
            }
        }
    }
}
