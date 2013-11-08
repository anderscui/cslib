using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using Andersc.AnyRun.Parsers;
using Andersc.CodeLib.Common;

namespace Andersc.AnyRun
{
    public partial class ParserForm : Form
    {
        private List<IParser> parsers;
        public ParserForm()
        {
            InitializeComponent();

            parsers = ParserBuilder.GetParsers();
            ltbResults.DisplayMember = "DisplayInfo";
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            ltbResults.Items.Clear();

            var input = txtInput.Text;
            if (input.IsBlank())
            {
                return;
            }

            var parseResults = new List<IParseResult>();
            foreach (var parser in parsers)
            {
                if (parser.CanParse(input))
                {
                    parseResults.AddRange(parser.Parse(input));
                }
            }

            foreach (var result in parseResults)
            {
                ltbResults.Items.Add(result);
            }
        }

        private void ltbResults_DoubleClick(object sender, EventArgs e)
        {
            var res = (ltbResults.SelectedItem as IParseResult);
            if (res.IsNotNull())
            {
                res.Do();
            }
        }
    }
}
