using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Andersc.CodeLib.Lifeasier.Sys
{
    public partial class Processes : Form
    {
        public Processes()
        {
            InitializeComponent();
        }

        private void Processes_Load(object sender, EventArgs e)
        {
            ShowProcessInfo();
        }

        // TODO: Refactor to ListView ctrl.
        private void ShowProcessInfo()
        {
            var ps = Process.GetProcesses().OrderBy(p => p.ProcessName);
            StringBuilder sb = new StringBuilder();
            foreach (var p in ps)
            {
                sb.AppendLine(p.ProcessName);
                sb.AppendLine("\tPID" + p.Id);
                sb.AppendLine("\tWin Title" + p.MainWindowTitle);
                //sb.AppendLine("\tPriorityClass" + p.PriorityClass);
                sb.AppendLine("\tResponding" + p.Responding);
                sb.AppendLine("\tFileName" + p.StartInfo.FileName);
                sb.AppendLine("\tThreads Count" + p.Threads.Count);
                sb.AppendLine("\tVirtualMemorySize" + p.VirtualMemorySize64);
                sb.AppendLine();
            }

            lblProcessCount.Text = ps.Count().ToString();
            txtInfo.Text = sb.ToString();
        }
    }
}
