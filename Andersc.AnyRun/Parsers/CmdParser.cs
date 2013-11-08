using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Andersc.CodeLib.Common;

namespace Andersc.AnyRun.Parsers
{
    public class CmdParser : AbstractParser
    {
        private static List<Cmd> Cmds;
 
        static CmdParser()
        {
            Cmds = Dao.GetAllCmds();
        }

        public override List<IParseResult> Parse(string input)
        {
            var results = new List<IParseResult>();

            Cmds.Where(cmd => cmd.Alias.Contains(input, StringComparison.OrdinalIgnoreCase)
                           || cmd.CmdPath.Contains(input, StringComparison.OrdinalIgnoreCase))
                .ForEach(cmd => results.Add(new CmdParseResult()
            {
                Data = cmd,
                DisplayInfo = "cmd: {0} | {1}".FormatWith(cmd.Alias, cmd.CmdPath),
            }));

            return results;
        }
    }
}
