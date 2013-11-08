using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Andersc.AnyRun
{
    public class Cmd
    {
        public string CmdPath { get; set; }
        public string CmdName { get { return Path.GetFileName(CmdPath); } }
        public string Alias { get; set; }
    }
}