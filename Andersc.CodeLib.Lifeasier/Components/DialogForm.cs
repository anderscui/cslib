using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Andersc.CodeLib.Lifeasier.Components
{
    public class DialogForm : Form
    {
        public DialogForm()
        {
            KeyDown += DialogForm_KeyDown;
        }

        private void DialogForm_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterParent;
        }

        void DialogForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DialogForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.KeyPreview = true;
            this.Name = "DialogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.DialogForm_Load);
            this.ResumeLayout(false);
        }
    }
}
