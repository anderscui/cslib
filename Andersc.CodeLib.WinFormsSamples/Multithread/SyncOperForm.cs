using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Andersc.CodeLib.WinFormsSamples.Multithread
{
    public partial class SyncOperForm : DialogForm
    {
        private static readonly int InitValue = 0;
        private static readonly int SleepTime = 300;
        private static readonly int Loops = 10;

        public SyncOperForm()
        {
            InitializeComponent();
        }

        private void DoSomethingTakingALongTime()
        {
            pbResult.Maximum = Loops;
            ShowProgress(InitValue);

            for (int i = 1; i <= Loops; i++)
            {
                Thread.Sleep(SleepTime);
                ShowProgress(i);
            }
        }

        private void ShowProgress(int valueSoFar)
        {
            pbResult.Value = valueSoFar;
        }

        private void btnDo_Click(object sender, EventArgs e)
        {
            DoSomethingTakingALongTime();
        }
    }
}
