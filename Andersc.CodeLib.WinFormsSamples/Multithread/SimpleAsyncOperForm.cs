using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Andersc.CodeLib.WinFormsSamples.Multithread
{
    public partial class SimpleAsyncOperForm : Form
    {
        private static readonly int SleepTime = 300;

        public SimpleAsyncOperForm()
        {
            InitializeComponent();
        }

        private int Loops
        {
            get { return Convert.ToInt32(nudLoops.Value); }
        }

        private void btnDo_Click(object sender, EventArgs e)
        {
            DoSomethingTakingALongTime();
        }

        private void DoSomethingTakingALongTime()
        {
            pbResult.Maximum = Loops;

            ShowProgress(0);
            Action<int> action = CalcSth;
            action.BeginInvoke(Loops, null, null);
            // TODO: Add EndInvoke()?
        }

        private void CalcSth(int loops)
        {
            for (int i = 1; i <= loops; i++)
            {
                Thread.Sleep(SleepTime);
                ShowProgress(i);
            }
        }

        private void ShowProgress(int valueSoFar)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<int>(ShowProgress), valueSoFar);
            }
            else
            {
                pbResult.Value = valueSoFar;
            }
        }
    }
}
