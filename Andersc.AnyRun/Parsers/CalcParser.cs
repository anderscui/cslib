using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Andersc.CodeLib.Algorithm;
using Andersc.CodeLib.Common;

namespace Andersc.AnyRun.Parsers
{
    public class CalcParser : AbstractParser
    {
        public override bool CanParse(string input)
        {
            // TODO: 
            return input.IsNotBlank();
        }

        public override List<IParseResult> Parse(string input)
        {
            var results = new List<IParseResult>();

            try
            {
                var eva = new Evaluator(input);
                long result = eva.Evaluate();
                results.Add(new PlainTextParseResult()
                {
                    DisplayInfo = input + " => " + result
                });
            }
            catch (Exception)
            {
                Console.WriteLine();
            }

            return results;
        }
    }
}
