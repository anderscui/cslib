using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Andersc.CodeLib.Common.Extension
{
    public static class LinqXmlExtension
    {
        private static readonly char XPathSeparator = '/';

        public static IEnumerable<XElement> ElementsOfPath(this XDocument doc, string xnamePath)
        {
            string[] names = xnamePath.Split(XPathSeparator);

            IEnumerable<XElement> result = doc.Elements(names[0]);
            for (int i = 1; i < names.Length; i++)
            {
                result = result.Elements(names[i]);
            }

            return result;
        }

    }
}
