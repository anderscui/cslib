using System;
using System.Collections.Generic;
using System.Linq;

namespace Andersc.AnyRun.Parsers
{
    public class EmptyParseResult : AbstractParseResult<string>
    {
        private static readonly EmptyParseResult _instance = new EmptyParseResult() { DisplayInfo = "No Matches..." };

        public static EmptyParseResult Instance
        {
            get { return _instance; }
        }

        public override bool Executable
        {
            get { return false; }
            set
            {
                // TODO: impl 
            }
        }

        protected override void Action() { }
    }
}
