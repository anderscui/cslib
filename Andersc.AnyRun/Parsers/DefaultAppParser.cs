using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Andersc.CodeLib.Common;

namespace Andersc.AnyRun.Parsers
{
    /// <summary>
    /// Regarding web stuff, such as search engine, weather, douban...
    /// </summary>
    public class DefaultAppParser : AbstractParser
    {
        private static readonly string regexWeb = ".\\w+";

        public override bool CanParse(string input)
        {
            return Regex.IsMatch(input, regexWeb);
        }

        public override List<IParseResult> Parse(string input)
        {
            var results = new List<IParseResult>();

            var result = API.GetAssociation(input);
            if (result.IsNotBlank() && File.Exists(result))
            {
                results.Add(new DefaultAppParserResult()
                {
                    Data = result,
                    DisplayInfo = "app: {0}".FormatWith(result),
                });
            }

            return results;
        }
    }
}
