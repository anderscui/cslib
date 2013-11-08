using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Andersc.CodeLib.Pomodoro
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer.Interval = 5 * 1000;
            timer.Tick += new EventHandler(timer_Tick);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Dispose();
        }

        private void btnNewPomodoro_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            MessageBox.Show("Pomodoro ends, have a rest:)");
        }

        
    }
}
