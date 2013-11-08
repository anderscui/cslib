using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Andersc.CodeLib.WinFormsSamples.Multithread
{
    internal enum CalcState
    {
        Pending, // No calculating running or canceling
        Calculating, // Calculation in process
        Canceled // Calculation canceled in UI but not worker
    }

    public partial class CancelAsyncOperForm : Form
    {
        private static readonly int InitValue = 0;
        private static readonly int SleepTime = 2000;

        private CalcState state = CalcState.Pending;

        public CancelAsyncOperForm()
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
            switch (state)
            {
                case CalcState.Pending:
                    // Start a new calculation
                    state = CalcState.Calculating;
                    btnDo.Text = "Cancel";

                    pbResult.Maximum = Loops;
                    ShowProgress(InitValue);
                    Action<int> action = CalcSth;
                    action.BeginInvoke(Loops, null, null);
                    break;

                case CalcState.Calculating:
                    // Cancel a running calculation
                    state = CalcState.Canceled;
                    btnDo.Enabled = false;
                    break;

                case CalcState.Canceled:
                    Debug.Assert(false);
                    break;

                default:
                    throw new Exception("Invalid state.");
            }
        }

        private void CalcSth(int loops)
        {
            bool shouldCancel = false;

            for (int i = 1; i <= loops; i++)
            {
                Thread.Sleep(SleepTime);
                ShowProgress(i, ref shouldCancel);

                if (shouldCancel)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Used for lock the data.
        /// </summary>
        private object stateLock = new object();
        private void ShowProgress(int valueSoFar, ref bool cancel)
        {
            lock (stateLock)
            {
                if (state == CalcState.Canceled)
                {
                    state = CalcState.Canceled;
                    cancel = true;
                    return;
                }

                cancel = false;
                ShowProgress(valueSoFar);
            }
        }

        private void ShowProgress(int valueSoFar)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<int>(ShowProgress), valueSoFar);
            }
            else
            {
                pbResult.Value = valueSoFar;

                // TODO: Doesnot work here.
                if (state == CalcState.Canceled)
                {
                    btnDo.Text = "Do";
                    btnDo.Enabled = true;
                    pbResult.Value = InitValue;
                }
            }
        }
    }
}
