using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Andersc.CodeLib.Common;

namespace Andersc.AnyRun.Parsers
{
    public class WebParserResult : AbstractParseResult<Webby>
    {
        public override bool Executable
        {
            get { return true; }
            set { throw new InvalidOperationException(); }
        }

        protected override void Action()
        {
            if (Data.IsNull()) { return; }

            var url = Data.UrlFormat;
            for (int i = 0; i < Data.Params.Length; i++)
            {
                url = url.Replace("%" + (i + 1), WebHelper.UrlEncode(Data.Params[i]));
            }
            //var url = Data.UrlFormat.FormatWith(Data.Params);
            Process.Start(url);
        }
    }
}
