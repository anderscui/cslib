using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Andersc.AnyRun
{
    public partial class MatchBox : Form
    {
        internal string Input { get; set; }

        internal string[] Options
        {
            set
            {
                ltbSuggests.Items.Clear();
                ltbSuggests.Items.AddRange(value);
            }
        }

        public MatchBox()
        {
            InitializeComponent();
        }
    }
}
