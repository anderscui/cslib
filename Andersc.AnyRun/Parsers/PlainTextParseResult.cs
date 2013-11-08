using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Andersc.AnyRun.Parsers
{
    public class PlainTextParseResult : AbstractParseResult<string>
    {
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
