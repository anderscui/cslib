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
    public partial class EllipseLabelForm : Form
    {
        public EllipseLabelForm()
        {
            InitializeComponent();

            InitControls();
        }

        private void InitControls()
        {
            elblSample.PrefixChanged += new EventHandler<Components.ValueChangedEventArgs<string>>(elblSample_PrefixChanged);
        }

        private void elblSample_PrefixChanged(object sender, Components.ValueChangedEventArgs<string> e)
        {
            MessageBox.Show(string.Format("Prefix was changed to '{0}' from '{1}'", e.NewValue, e.OldValue));
        }

        private void btnChangeText_Click(object sender, EventArgs e)
        {
            elblSample.Text = DateTime.Now.ToString();
        }

        private void btnChangePrefix_Click(object sender, EventArgs e)
        {
            elblSample.Prefix = DateTime.Now.Second.ToString() + ": ";
        }
    }
}
