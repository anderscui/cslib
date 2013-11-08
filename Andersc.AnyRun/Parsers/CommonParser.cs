using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Andersc.CodeLib.Common;

namespace Andersc.AnyRun.Parsers
{
    public class CommonParser : AbstractParser
    {
        public override List<IParseResult> Parse(string input)
        {
            var results = new List<IParseResult>();

            if ("date".Contains(input, StringComparison.OrdinalIgnoreCase))
            {
                results.Add(new PlainTextParseResult()
                {
                    DisplayInfo = "time now: {0}".FormatWith(DateTime.Now.ToString())
                });
            }

            if ("pi".Contains(input, StringComparison.OrdinalIgnoreCase))
            {
                results.Add(new PlainTextParseResult()
                {
                    DisplayInfo = "PI is: {0}".FormatWith(Math.PI)
                });
            }

            if ("e".Contains(input, StringComparison.OrdinalIgnoreCase))
            {
                results.Add(new PlainTextParseResult()
                {
                    DisplayInfo = "E is: {0}".FormatWith(Math.E)
                });
            }

            return results;
        }
    }
}
