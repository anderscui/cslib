using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.AnyRun.Parsers
{
    public class ParserBuilder
    {
        public static List<IParser> GetParsers()
        {
            var parsers = new List<IParser>();
            parsers.Add(new CommonParser());
            parsers.Add(new CalcParser());
            parsers.Add(new CmdParser());
            parsers.Add(new SpecialFolderParser());
            parsers.Add(new WebParser());
            parsers.Add(new DefaultAppParser());
            parsers.Add(new ShortcutParser());
            parsers.Add(new RecentFilesParser());

            return parsers;
        }
    }
}
