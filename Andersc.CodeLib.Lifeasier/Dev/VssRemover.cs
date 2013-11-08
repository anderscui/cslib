using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Lifeasier.Dev
{
    public partial class VssRemover : Form
    {
        public VssRemover()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = dlgFolder.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtDir.Text = dlgFolder.SelectedPath;
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (txtDir.Text.IsBlank())
            {
                MessageBox.Show("Please select a dir.");
                return;
            }

            try
            {
                txtMsg.Clear();
                AppendMsg("I'm removing vss info for you.");
                AppendMsg("the folder is " + txtDir.Text + Environment.NewLine);

                RemoveFilesByExtension("*.vssscc");
                RemoveFilesByExtension("*.scc");
                RemoveFilesByExtension("*.vspscc");

                AppendMsg(Environment.NewLine + "Job done.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error occured: {0}", ex.Message));
            }
        }

        private void RemoveFilesByExtension(string extension)
        {
            FileInfo fi = null;
            string[] files = Directory.GetFiles(txtDir.Text, extension, SearchOption.AllDirectories);

            AppendMsg("Removing the " + extension + " files");
            foreach (string file in files)
            {
                fi = new FileInfo(file);

                if (fi.IsReadOnly()) { fi.MakeEditable(); }
                fi.Delete();
            }
            AppendMsg("OK");
        }

        private void AppendMsg(string msg)
        {
            txtMsg.Text += msg + Environment.NewLine;
        }
    }
}
