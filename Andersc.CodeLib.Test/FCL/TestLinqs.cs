using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

using NUnit.Framework;

using Andersc.CodeLib.Common;
using Andersc.CodeLib.Tester.Helpers;

namespace Andersc.CodeLib.Tester.FCL
{
    [TestFixture]
    public class TestLinqs
    {
        private string northwindConnString = @"Server=(local)\SQL2008;Database=Northwind;UID=andersc;PWD=sa;";

        [Test]
        public void TestLinqToObject_Basic()
        {
            string[] words = { "Hello", "Wonderful", "Linq", "Beautiful", "World" };

            var groups =
                from w in words
                orderby w ascending
                group w by w.Length into lengthGroups
                orderby lengthGroups.Key descending
                select new { Length = lengthGroups.Key, Words = lengthGroups };

            foreach (var group in groups)
            {
                Console.WriteLine("Words of length " + group.Length);
                foreach (string w in group.Words)
                {
                    Console.WriteLine(w);
                }
            }
        }

        [Test]
        public void TestFromObjectsToXML()
        {
            Book[] books = new Book[]
            {
                new Book{ Title="Ajax in Action", Publisher="Manning", Year= 2005 },
                new Book{ Title="WinForms in Action", Publisher="Manning", Year= 2006 },
                new Book{ Title="RSS and Atom in Action", Publisher="Manning", Year= 2006 }
            };

            // Using XmlDocument
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("books");
            foreach (Book book in books)
            {
                if (book.Year == 2006)
                {
                    XmlElement elem = doc.CreateElement("book");
                    elem.SetAttribute("title", book.Title);

                    XmlElement publisher = doc.CreateElement("publisher");
                    publisher.InnerText = book.Publisher;
                    elem.AppendChild(publisher);

                    root.AppendChild(elem);
                }
            }
            doc.AppendChild(root);
            doc.Save(String.Format("byDOM-{0}.xml", DateTime.Now.ToFileTime()));

            // Using LINQ to XML
            XElement xml = new XElement("books",
                from book in books
                where book.Year == 2006
                select new XElement("book",
                    new XAttribute("title", book.Title),
                    new XElement("publisher", book.Publisher)
                )
            );
            xml.Save(String.Format("byLINQ-{0}.xml", DateTime.Now.ToFileTime()));
        }

        [Test]
        public void TestToComplexXml()
        {

        }

        [Test]
        public void TestSqlToLinq()
        {
            DataContext ctx = new DataContext(northwindConnString);
            ctx.Log = new DebuggerWriter();

            Table<Customer> customers = ctx.GetTable<Customer>();

            var fromLondon =
                from customer in customers
                where customer.City == "London"
                select customer;

            foreach (var c in fromLondon)
            {
                Console.WriteLine(c.ContactName);
            }
        }
    }
}
