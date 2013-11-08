using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace Andersc.CodeLib.WinFormsSamples.Multithread
{
    public partial class AsyncOperForm : Form
    {
        private static readonly int SleepTime = 300;

        private int Loops
        {
            get { return Convert.ToInt32(nudLoops.Value); }
        }

        public AsyncOperForm()
        {
            InitializeComponent();
        }

        private void btnDo_Click(object sender, EventArgs e)
        {
            DoSomethingTakingALongTime();
        }

        private void DoSomethingTakingALongTime()
        {
            pbResult.Maximum = Loops;

            Thread piThread = new Thread(() => CalcSth(Loops));
            piThread.Start();
        }

        private void CalcSth(int loops)
        {
            ShowProgressDelegate spd = (val => pbResult.Value = val);

            this.BeginInvoke(spd, 0);

            for (int i = 1; i <= loops; i++)
            {
                Thread.Sleep(SleepTime);
                this.BeginInvoke(spd, i);
            }
        }

        delegate void ShowProgressDelegate(int valueSoFar);
    }
}
