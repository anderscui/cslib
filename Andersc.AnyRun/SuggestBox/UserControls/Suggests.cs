using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Andersc.AnyRun.Parsers;
using Andersc.CodeLib.Common.WinForm;

namespace Andersc.AnyRun.UserControls
{
    public partial class Suggests : UserControl
    {
        public ListBox SuggestBox
        {
            get { return ltbSuggests; }
        }

        internal IParseResult[] Options
        {
            set
            {
                ltbSuggests.Items.Clear();
                ltbSuggests.Items.AddRange(value);
            }
        }

        public Suggests()
        {
            InitializeComponent();
        }

        private void ltbSuggests_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Keys.Enter.Char())
            {
                if (ltbSuggests.SelectedIndex >= 0)
                {
                    var result = ltbSuggests.SelectedItem as IParseResult;
                    result.Do();
                }
                e.Handled = true;
                return;
            }

            //var form = ParentForm as MainForm;
            //form.MainForm_KeyPress(sender, e);
        }
    }
}
