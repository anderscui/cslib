using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Andersc.CodeLib.Common;

namespace Andersc.AnyRun.Parsers
{
    public class DefaultAppParserResult : AbstractParseResult<string>
    {
        public override bool Executable
        {
            get { return true; }
            set { throw new InvalidOperationException(); }
        }

        protected override void Action()
        {
            Process.Start(Data);
        }
    }
}
