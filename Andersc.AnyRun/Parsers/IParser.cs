using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.AnyRun.Parsers
{
    public interface IParser
    {
        bool CanParse(string input);
        List<IParseResult> Parse(string input);
        void Refresh();
    }
}
