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
    public partial class CoolBackgroundWorker : Form
    {
        class CalcUserState
        {
            public string Message { get; set; }
            public int ValueSoFar { get; set; }
        }

        private static readonly int SleepTime = 300;

        private int Loops
        {
            get { return Convert.ToInt32(nudLoops.Value); }
        }

        public CoolBackgroundWorker()
        {
            InitializeComponent();

            pbResult.Maximum = Loops;
            lblStatus.Text = "Idle";
            pbResult.Visible = false;
        }

        private void btnDo_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.CancellationPending)
            {
                return;
            }

            if (backgroundWorker.IsBusy)
            {
                btnDo.Enabled = false;
                backgroundWorker.CancelAsync();
                return;
            }

            btnDo.Text = "Cancel";
            lblStatus.Text = "Caculating";
            pbResult.Value = 0;
            pbResult.Visible = true;
            backgroundWorker.RunWorkerAsync(Loops);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime start = DateTime.Now;

            int loops = Convert.ToInt32(e.Argument);
            CalcSth(loops);
            if (backgroundWorker.CancellationPending) { e.Cancel = true; }

            TimeSpan elapsed = DateTime.Now - start;
            e.Result = elapsed;
        }

        private void CalcSth(int loops)
        {
            for (int i = 1; i <= loops; i++)
            {
                // Mock exception
                //if (i == 3) { throw new InvalidOperationException(); }
                if (backgroundWorker.CancellationPending) { return; }

                // Do real works.
                Thread.Sleep(SleepTime);
                backgroundWorker.ReportProgress(0,
                    new CalcUserState() { Message = "Caculating", ValueSoFar = i });
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CalcUserState state = e.UserState as CalcUserState;
            ShowProgress(state.Message, state.ValueSoFar);
        }

        private void ShowProgress(string msg, int valueSoFar)
        {
            Debug.Assert(this.InvokeRequired == false);
            if (InvokeRequired) { throw new Exception("Doh!"); }

            lblStatus.Text = msg;
            pbResult.Value = valueSoFar;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                lblStatus.Text = "Error: " + e.Error.Message;
                pbResult.Visible = false;
                return;
            }

            if (e.Cancelled)
            {
                btnDo.Text = "Do";
                btnDo.Enabled = true;
                lblStatus.Text = "Cancelled";
                pbResult.Visible = false;
                return;
            }

            TimeSpan elapsed = (TimeSpan)e.Result;
            lblStatus.Text = "Ready, time elapsed " + elapsed.ToString();
            pbResult.Visible = false;
        }
    }
}
