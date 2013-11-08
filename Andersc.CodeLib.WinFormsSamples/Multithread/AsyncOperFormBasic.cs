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
    public partial class AsyncOperFormBasic : Form
    {
        private static readonly int SleepTime = 300;

        private int Loops
        {
            get { return Convert.ToInt32(nudLoops.Value); }
        }

        public AsyncOperFormBasic()
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
         
            Thread calcThread = new Thread(CalcStart);
            calcThread.Name = "Calc Sth";
            ShowThreadDebuggingInfo("Start a new thread");
            calcThread.Start(Loops);
        }

        // thread starter wrapper, or entry point.
        private void CalcStart(object loops)
        {
            CalcSth(Convert.ToInt32(loops));
        }

        private void CalcSth(int loops)
        {
            ShowThreadDebuggingInfo("Start to calc");

            ShowProgressDelegate spd = ShowProgress;

            // Init progress
            Invoke(spd, 0);
            //UpdateProgress(0); // Doesn't work.

            for (int i = 1; i <= loops; i++)
            {
                Thread.Sleep(SleepTime);
                Invoke(spd, i);
                //UpdateProgress(i); // Doesn't work.
            }
        }

        delegate void ShowProgressDelegate(int valueSoFar);
        private void ShowProgress(int valueSoFar)
        {
            pbResult.Value = valueSoFar;
        }

        private void UpdateProgress(int valueSoFar)
        {
            ShowThreadDebuggingInfo("Update value to " + valueSoFar);
            pbResult.Value = valueSoFar;
            // causes: Cross-thread operation not valid: Control 'pbResult' accessed from a thread other than the thread it was created on.
        }

        private static void ShowThreadDebuggingInfo(string message)
        {
            Debug.WriteLine(message, "Common");
            Debug.WriteLine(string.Format("Current Thread: {0}", Thread.CurrentThread.ManagedThreadId),
                "Thread");
        }
    }
}
