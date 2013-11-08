using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Andersc.CodeLib.Common;

namespace Andersc.AnyRun.Parsers
{
    public class SpecialFolderParserResult : AbstractParseResult<KeyValuePair<string, string>>
    {
        public override bool Executable
        {
            get { return true; }
            set { throw new InvalidOperationException(); }
        }

        protected override void Action()
        {
            if (Data.IsNull()) { return; }

            Process.Start("explorer.exe", Data.Value);
        }
    }
}
