using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace Andersc.CodeLib.WinFormsSamples.CustomControls
{
    public class FileTextBox : TextBox
    {
        protected override void OnTextChanged(EventArgs e)
        {
            if (File.Exists(this.Text))
            {
                this.ForeColor = Color.Black;
            }
            else
            {
                this.ForeColor = Color.Red;
            }

            base.OnTextChanged(e);
        }
    }
}
