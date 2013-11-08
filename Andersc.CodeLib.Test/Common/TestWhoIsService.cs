/*
 * Created by: Anders Cui
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;

using NUnit.Framework;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestWhoIsService
    {
        private string Search(string domain)
        {
            // assume input domain is google.com
            string topLevelDomain = WhoIsHelper.GetTopLevelDomain(domain); // topLevelDomain is com.

            string response = string.Empty;
            if (string.IsNullOrEmpty(topLevelDomain.Trim()))
            {
                //this.Search(domain);
            }
            else
            {
                response = Query(topLevelDomain, "whois.iana.org").ToLower();
                string whoisServer = string.Empty;
                string errorMessage = string.Empty;

                if (!WhoIsHelper.ParseIanaResponseForTldWhoisServer(topLevelDomain, response, out whoisServer, out errorMessage))
                {
                    Console.WriteLine("Invalid Whois Server: {0}", topLevelDomain);
                }
                else
                {
                    response = Query(domain, whoisServer).ToLower();
                    if (WhoIsHelper.IsDomainAvailable(domain, response))
                    {
                        Console.WriteLine("server '{0}' is available.", whoisServer);
                    }
                    else
                    {
                        Console.WriteLine("server '{0}' is not available.", whoisServer);
                    }
                }
            }

            return response;
        }

        private string Query(string query, string whoisServer)
        {
            string str;
            TcpClient client = new TcpClient(whoisServer, 0x2b);
            using (NetworkStream stream = client.GetStream())
            {
                byte[] bytes = Encoding.ASCII.GetBytes(query + "\r\n");
                stream.Write(bytes, 0, bytes.Length);
                using (StreamReader reader = new StreamReader(stream, Encoding.ASCII))
                {
                    str = reader.ReadToEnd() + "\r\n";
                }
            }

            return str;

        }

        [Test]
        public void TestWhoIs()
        {
            string dn = "51.net"; // google.com.hk
            //string server = "whois.verisign-grs.com";
            string server = "whois.iana.org";
            //string server = "whois.internic.net";
            //string server = "whois.cnnic.net.cn";
            //string server = "whois.pir.org";

            try
            {
                //Console.WriteLine(Query(dn, server));
                Console.WriteLine(Search(dn));
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Error: {0}", ex.Message));
            }
        }

        private string GetHostNameByIp(string ip)
        {
            ip = ip.Trim();
            if (ip == string.Empty)
                return string.Empty;
            try
            {
                // 是否 Ping 的通
                if (this.PassPing(ip))
                {
                    IPHostEntry host = Dns.GetHostEntry(ip);
                    return host.HostName;
                }
                else
                    return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private string GetIpByHostName(string hostName)
        {
            hostName = hostName.Trim();
            if (hostName == string.Empty)
                return string.Empty;
            try
            {
                IPHostEntry host = Dns.GetHostEntry(hostName);
                return host.AddressList.GetValue(0).ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private bool PassPing(string ip)
        {
            Ping p = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            string data = "Test Data!";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 1000;
            PingReply reply = p.Send(ip, timeout, buffer, options);
            if (reply.Status == IPStatus.Success)
                return true;
            else
                return false;
        }

        [Test]
        public void TestPassPing()
        {
            string ip = "66.249.89.104"; // www.google.com
            Assert.That(PassPing(ip), Is.True);

            ip = "1.1.1.1";
            Assert.That(PassPing(ip), Is.False);
        }

        [Test]
        public void TestGetIpByHostName()
        {
            string hostName = "www.google.com";
            Assert.That(GetIpByHostName(hostName), Is.EqualTo("66.249.89.104"));

            hostName = "www.anderscuisomething.com";
            Assert.That(GetIpByHostName(hostName), Is.EqualTo(string.Empty));
        }

        [Test]
        public void TestGetHostNameByIp()
        {
            string ip = "66.249.89.99"; // www.google.com.hk
            Assert.That(GetHostNameByIp(ip), Is.EqualTo("nrt04s01-in-f99.1e100.net"));

            ip = "114.80.143.197";
            Assert.That(GetHostNameByIp(ip), Is.EqualTo("114.80.143.197"));

            ip = "www.anderscuisomething.com";
            Assert.That(GetHostNameByIp(ip), Is.EqualTo(string.Empty));

            var localAddress =
                (from ni in NetworkInterface.GetAllNetworkInterfaces()
                 where ni.NetworkInterfaceType != NetworkInterfaceType.Loopback
                 let props = ni.GetIPProperties()
                 from ipAddress in props.UnicastAddresses
                 select ipAddress).FirstOrDefault();

            Console.WriteLine(localAddress.Address);

        }

        [Test]
        public void TestGetDNById()
        {
            string IpAddressString = "206.72.125.204"; //eggheadcafe

            Console.WriteLine(Dns.GetHostByAddress(IpAddressString).HostName);

            //try
            //{
            //    IPAddress hostIPAddress = IPAddress.Parse(IpAddressString);
            //    IPHostEntry hostInfo = Dns.GetHostByAddress(hostIPAddress);
            //    // Get the IP address list that resolves to the host names contained in 
            //    // the Alias property.
            //    IPAddress[] address = hostInfo.AddressList;
            //    // Get the alias names of the addresses in the IP address list.
            //    String[] alias = hostInfo.Aliases;

            //    Console.WriteLine("Host name : " + hostInfo.HostName);
            //    Console.WriteLine("\nAliases :");
            //    for (int index = 0; index < alias.Length; index++)
            //    {
            //        Console.WriteLine(alias[index]);
            //    }
            //    Console.WriteLine("\nIP address list : ");
            //    for (int index = 0; index < address.Length; index++)
            //    {
            //        Console.WriteLine(address[index]);
            //    }
            //}
            //catch (SocketException e)
            //{
            //    Console.WriteLine("SocketException caught!!!");
            //    Console.WriteLine("Source : " + e.Source);
            //    Console.WriteLine("Message : " + e.Message);
            //}
            //catch (FormatException e)
            //{
            //    Console.WriteLine("FormatException caught!!!");
            //    Console.WriteLine("Source : " + e.Source);
            //    Console.WriteLine("Message : " + e.Message);
            //}
            //catch (ArgumentNullException e)
            //{
            //    Console.WriteLine("ArgumentNullException caught!!!");
            //    Console.WriteLine("Source : " + e.Source);
            //    Console.WriteLine("Message : " + e.Message);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Exception caught!!!");
            //    Console.WriteLine("Source : " + e.Source);
            //    Console.WriteLine("Message : " + e.Message);
            //}
        }
    }
}
