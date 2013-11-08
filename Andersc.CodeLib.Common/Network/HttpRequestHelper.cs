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
        public static string GetResponseText(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";

            // TODO: Find suitable autoRedirections & timeout.
            request.MaximumAutomaticRedirections = 5;
            request.Timeout = 1000 * 6;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string result = reader.ReadToEnd();

            reader.Close();

            return result;
        }
    }
}
