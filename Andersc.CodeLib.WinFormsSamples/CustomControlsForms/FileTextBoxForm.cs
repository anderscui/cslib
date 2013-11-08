using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Andersc.CodeLib.WinFormsSamples.CustomControlsForms
{
    public partial class FileTextBoxForm : Form
    {
        public FileTextBoxForm()
        {
            InitializeComponent();
        }

        private void txtFileName_TextChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Test");
        }
    }
}
