using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Andersc.CodeLib.Common;

namespace Andersc.AnyRun.Parsers
{
    public class SpecialFolderParser : AbstractParser
    {
        private static List<KeyValuePair<string, string>> SpecialFolders;

        static SpecialFolderParser()
        {
            SpecialFolders = new List<KeyValuePair<string, string>>();
            foreach (var name in Enum.GetNames(typeof(Environment.SpecialFolder)))
            {
                SpecialFolders.Add(
                    new KeyValuePair<string, string>(name,
                        Environment.GetFolderPath((Environment.SpecialFolder)Enum.Parse(typeof(Environment.SpecialFolder), name))));

            }
        }

        public override List<IParseResult> Parse(string input)
        {
            var results = new List<IParseResult>();

            SpecialFolders.ForEach(sf =>
            {
                if (sf.Key.Contains(input, StringComparison.OrdinalIgnoreCase)
                    && sf.Value.IsNotBlank())
                {
                    results.Add(new SpecialFolderParserResult()
                    {
                        Data = sf,
                        DisplayInfo = "folder: {0} | {1}".FormatWith(sf.Key, sf.Value)
                    });
                }
            });

            return results;
        }
    }
}
