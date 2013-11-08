using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Andersc.CodeLib.Common;

namespace Andersc.AnyRun.Parsers
{
    public class CmdParseResult : AbstractParseResult<Cmd>
    {
        public override bool Executable
        {
            get { return true; }
            set { throw new InvalidOperationException(); }
        }

        protected override void Action()
        {
            if (Data.IsNull()) { return; }

            var cmd = Data.CmdPath;
            var proc = new Process
            {
                StartInfo = {
                    FileName = cmd, 
                    WindowStyle = ProcessWindowStyle.Normal 
                }
            };
            
            //proc.StartInfo.Arguments = @"D:\temp.txt";
            proc.Start();
            //proc.WaitForExit();
        }
    }
}
