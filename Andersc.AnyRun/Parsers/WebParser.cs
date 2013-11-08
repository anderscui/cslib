using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Andersc.CodeLib.Common;

namespace Andersc.AnyRun.Parsers
{
    /// <summary>
    /// Regarding web stuff, such as search engine, weather, douban...
    /// </summary>
    public class WebParser : AbstractParser
    {
        private static readonly string regexWeb = "(?<name>\\w+)\\s+(?<q>.*)";

        private static List<Webby> Webbies;

        static WebParser()
        {
            Webbies = Dao.GetAllWebbies();
        }

        public override bool CanParse(string input)
        {
            return Regex.IsMatch(input, regexWeb);
        }

        public override List<IParseResult> Parse(string input)
        {
            var results = new List<IParseResult>();

            var match = Regex.Match(input, regexWeb, RegexOptions.IgnoreCase);
            var name = match.Groups["name"].Value;
            var q = match.Groups["q"].Value;

            foreach (var webby in Webbies)
            {
                if (webby.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                {
                    webby.Params = new string[] { q };
                    results.Add(new WebParserResult()
                    {
                        Data = webby,
                        DisplayInfo = "webby: {0}, query: {1}".FormatWith(webby.Name, q),
                    });
                }
            }

            return results;
        }
    }
}
