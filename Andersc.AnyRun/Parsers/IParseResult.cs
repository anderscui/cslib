using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.AnyRun.Parsers
{
    public interface IParseResult
    {
        bool Executable { get; set; }
        string DisplayInfo { get; set; }

        void Do();
    }
}
