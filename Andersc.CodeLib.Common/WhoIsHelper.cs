using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Andersc.CodeLib.Common
{
    public class WhoIsHelper
    {
        public static string GetTopLevelDomain(string domain)
        {
            string str = string.Empty;
            int index = domain.LastIndexOf('.');
            if (index > 0)
            {
                int length = domain.Length;
                str = domain.Substring(index + 1, (length - index) - 1).ToLower();
            }

            return str;
        }

        // TODO: 
        private static string FormatString(string msg, params object[] args)
        {
            string str = msg;
            if (args != null)
            {
                try
                {
                    str = string.Format(Thread.CurrentThread.CurrentCulture, msg, args);
                }
                catch
                {
                }
            }
            return str;
        }

        public static bool IsDomainAvailable(string domain, string response)
        {
            bool flag = false;
            string str = FormatString("no match for \"{0}\"", new object[] { domain }).ToLower(Thread.CurrentThread.CurrentCulture);
            if (!response.Contains(str) && !response.Contains("not found"))
            {
                return flag;
            }

            return true;
        }

        public static bool ParseIanaResponseForTldWhoisServer(string tld, string response, out string whoisServer, out string errorMessage)
        {
            whoisServer = string.Empty;
            errorMessage = string.Empty;
            string str = FormatString("this query returned 0", new object[] { tld }).ToLower();
            if (response.Contains(str))
            {
                errorMessage = "Invalid Top Level Domain.";
                return false;
            }

            if (response.Contains("whois:"))
            {
                int index = response.IndexOf("whois:", StringComparison.CurrentCultureIgnoreCase);
                int num2 = response.IndexOf('\n', index);
                whoisServer = response.Substring(index, (num2 - index) + 1).ToLower();
                whoisServer = whoisServer.Replace("whois:", string.Empty).Trim();
                return true;
            }

            errorMessage = "This top level domain does not publish a Whois Server.";
            return false;
        }

        public static string SanitizeDomain(string domain)
        {
            domain = domain.Replace(" ", string.Empty);
            domain = domain.Replace("https://", string.Empty).Trim();
            domain = domain.Replace("http://", string.Empty).Trim();
            return domain.Replace("www.", string.Empty).Trim();
        }
    }
}
