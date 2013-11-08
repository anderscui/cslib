using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Andersc.CodeLib.Common;

namespace Andersc.AnyRun.Parsers
{
    public abstract class AbstractParser : IParser
    {
        public virtual bool CanParse(string input)
        {
            return true;
        }

        public abstract List<IParseResult> Parse(string input);

        public virtual void Refresh()
        {
        }
    }
}
