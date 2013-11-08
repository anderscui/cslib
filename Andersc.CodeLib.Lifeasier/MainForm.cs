using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Andersc.CodeLib.Lifeasier.Dev;
using Andersc.CodeLib.Lifeasier.Life;
using Andersc.CodeLib.Lifeasier.Tools;
using Andersc.CodeLib.Lifeasier.Sys;

namespace Andersc.CodeLib.Lifeasier
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void menuPathExtractor_Click(object sender, EventArgs e)
        {
            SmartCopier pe = new SmartCopier();
            pe.ShowDialog(this);
        }

        private void mnuFolderExtractor_Click(object sender, EventArgs e)
        {
            FolderExtractor fe = new FolderExtractor();
            fe.ShowDialog(this);
        }

        private void vSSHelperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VssHelper vh = new VssHelper();
            vh.ShowDialog(this);
        }

        private void mnuPathFinder_Click(object sender, EventArgs e)
        {
            PathFinder pf = new PathFinder();
            pf.ShowDialog();
        }

        private void mnuVssRemover_Click(object sender, EventArgs e)
        {
            VssRemover remover = new VssRemover();
            remover.ShowDialog(this);
        }

        private void mnuSystemDriveInfo_Click(object sender, EventArgs e)
        {
            SystemDriveInfo driveInfo = new SystemDriveInfo();
            driveInfo.ShowDialog(this);
        }

        private void mnuToolsPop3Mail_Click(object sender, EventArgs e)
        {
            Pop3Mail pm = new Pop3Mail();
            pm.ShowDialog(this);
        }

        private void mnuProcesses_Click(object sender, EventArgs e)
        {
            (new Processes()).ShowDialog(this);
        }

        private void mnuDevTestDbConn_Click(object sender, EventArgs e)
        {
            var form = new TestDatabaseConnectivity();
            form.ShowDialog(this);
        }

        private void menuLifeOxford_Click(object sender, EventArgs e)
        {
            (new OxfordExtractor()).ShowDialog(this);
        }
    }
}
