using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Andersc.CodeLib.Common;

namespace Andersc.AnyRun.Parsers
{
    public class ShortcutParseResult : AbstractParseResult<string>
    {
        public override bool Executable
        {
            get { return true; }
            set { throw new InvalidOperationException(); }
        }

        protected override void Action()
        {
            if (Data.IsBlank()) { return; }
            if (File.Exists(Data).IsFalse()) { return; }

            Process.Start(Data);
        }
    }
}
