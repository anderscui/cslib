using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Andersc.CodeLib.WinFormsSamples.CustomControls
{
    public partial class FileBrowseTextBox : UserControl
    {
        public FileBrowseTextBox()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (dlgOpenFile.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = dlgOpenFile.FileName;
            }
        }
    }
}
