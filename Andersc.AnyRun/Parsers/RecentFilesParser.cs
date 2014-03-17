using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Andersc.CodeLib.Common;

namespace Andersc.AnyRun.Parsers
{
    public class RecentFilesParser : AbstractParser
    {
        private static List<Cmd> Cmds;

        static RecentFilesParser()
        {
            Cmds = Dao.GetRecentShortcuts();
        }

        public override List<IParseResult> Parse(string input)
        {
            var results = new List<IParseResult>();

            Cmds.Where(cmd => cmd.Alias.Contains(input, StringComparison.OrdinalIgnoreCase)
                           || cmd.CmdPath.Contains(input, StringComparison.OrdinalIgnoreCase))
                .ForEach(cmd => results.Add(new ShortcutParseResult()
            {
                Data = cmd.CmdPath,
                DisplayInfo = "recent: {0} | {1}".FormatWith(cmd.Alias, cmd.CmdPath),
            }));

            return results;
        }
    }
}
