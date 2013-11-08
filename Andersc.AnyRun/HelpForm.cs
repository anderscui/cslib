using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Andersc.CodeLib.Common.WinForm;

namespace Andersc.AnyRun
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
        }

        private void HelpForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Keys.Escape.Char())
            {
                Close();
            }
        }
    }
}
