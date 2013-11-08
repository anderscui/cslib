using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.AnyRun.Parsers
{
    public class MockParser : AbstractParser
    {
        public override List<IParseResult> Parse(string input)
        {
            var results = new List<IParseResult>();

            for (int i = 0; i < 1; i++)
            {
                results.Add(new PlainTextParseResult()
                {
                    DisplayInfo = "MockResult " + i
                });
            }

            return results;
        }
    }
}
