using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Andersc.AnyRun.Parsers;
using Andersc.CodeLib.Common;
using Andersc.CodeLib.Common.WinForm;

namespace Andersc.AnyRun
{
    public partial class MainForm : Form
    {
        private Keys[] upDownArrows = new Keys[] { Keys.Up, Keys.Down };
        private Keys[] arrowKeys = new Keys[] { Keys.Left, Keys.Right, Keys.Up, Keys.Down };

        private int hkid;
        private List<IParser> parsers;

        public MainForm()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            //var backColor = Color.FromArgb(50, 101, 101);
            var backColor = Color.FromArgb(108, 102, 96);

            BackColor = backColor;
            //lblAlias.BackColor = backColor;
            //txtInput.BackColor = backColor;

            lblAlias.Text = string.Empty;

            parsers = ParserBuilder.GetParsers();
            ltbSuggests.DisplayMember = "DisplayInfo";
        }

        private void RunCommand()
        {
            if (CurResult.IsNull())
            {
                return;
            }

            CurResult.Do();
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    if (hkid == (int)m.WParam)
                    {
                        ToggleConsole();
                    }
                    break;
            }

            //if (m.Msg == Program.WM_SHOWME)
            //{
            //    //ShowConsole();
            //    MessageBox.Show(":)");
            //}
            base.WndProc(ref m);
        }

        #region Main Form Handlers

        private void MainForm_Load(object sender, EventArgs e)
        {
            // hot key registry;
            hkid = Thread.CurrentThread.GetHashCode();
            API.RegisterHotKey(Handle, hkid, (int)KeyModifiers.Windows, (int)Keys.W);

            txtInput.Focus();
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Keys.Escape.Char())
            {
                HideConsole();
                e.Handled = true;
                return;
            }

            if (e.KeyChar == Keys.Enter.Char())
            {
                RunCommand();
                e.Handled = true;
                return;
            }

            //API.SendMessage(txtInput.Handle, API.WM_CHAR, e.KeyChar, 0);
            var focusedCtrl = FindFocusedControl(this);
            if (focusedCtrl != txtInput)
            {
                txtInput_KeyPress(sender, e);
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            var focusedCtrl = FindFocusedControl(this);
            if (focusedCtrl != ltbSuggests
                && (e.KeyCode.In(upDownArrows)))
            {
                e.Handled = true;
                ltbSuggests_KeyDown(sender, e);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            API.UnregisterHotKey(Handle, hkid);
        }

        #endregion

        #region Notify Icon Handlers

        private void notifyAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hope you enjoy this tool", "AnyRun - Run anything on your computer");
        }

        private void notifyHelp_Click(object sender, EventArgs e)
        {
            var helpForm = new HelpForm();
            helpForm.ShowDialog(this);
        }

        private void notifyExit_Click(object sender, EventArgs e)
        {
            ExitApp();
        }

        private void notifyShowConsole_Click(object sender, EventArgs e)
        {
            ShowConsole();
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ShowConsole();
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ShowConsole();
            }
        }

        #endregion

        #region Private Helpers

        private static Control FindFocusedControl(Control control)
        {
            var container = control as ContainerControl;
            while (container != null)
            {
                control = container.ActiveControl;
                container = control as ContainerControl;
            }
            return control;
        }

        private void ToggleConsole()
        {
            if (Visible && Focused)
            {
                HideConsole();
            }
            else
            {
                ShowConsole();
            }
        }

        private void HideConsole()
        {
            Hide();
        }

        private void ShowConsole()
        {
            Show();
            Activate();
        }

        private void ExitApp()
        {
            notifyIcon.Dispose();
            Close();
            Application.Exit();
        }

        #endregion

        #region Input Handlers

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            ltbSuggests.Items.Clear();

            var input = txtInput.Text;
            if (input.IsBlank())
            {
                ClearCurResult();
                return;
            }

            var parseResults = new List<IParseResult>();
            foreach (var parser in parsers)
            {
                if (parser.CanParse(input))
                {
                    parseResults.AddRange(parser.Parse(input));
                }
            }

            foreach (var result in parseResults)
            {
                ltbSuggests.Items.Add(result);
            }

            if (ltbSuggests.Items.Count > 0)
            {
                ltbSuggests.SelectedIndex = 0;
            }
            else
            {
                ClearCurResult();
            }
        }

        private char _lastInput = '\0';
        private DateTime _lastInputTime = DateTime.Now;
        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            var timeElapsed = (DateTime.Now - _lastInputTime).TotalMilliseconds;
            if ((e.KeyChar == _lastInput) && (_lastInput == Keys.Back.Char()) && (timeElapsed <= 300))
            {
                txtInput.Focus();
                txtInput.Text = string.Empty;
                return;
            }

            if (sender != txtInput)
            {
                txtInput.Focus();
                if (e.KeyChar == Keys.Back.Char() && txtInput.Text.Length > 0)
                {
                    txtInput.Text = txtInput.Text.Substring(0, txtInput.Text.Length - 1);
                }
                else
                {
                    txtInput.Text += e.KeyChar.ToString();
                }

                txtInput.SelectionStart = txtInput.Text.Length;
                txtInput.SelectionLength = 0;
            }

            _lastInput = e.KeyChar;
            _lastInputTime = DateTime.Now;
        }

        #endregion

        #region Suggests

        private IParseResult _curResult = null;
        private IParseResult CurResult
        {
            get { return _curResult; }
            set
            {
                _curResult = value;
                SetCmdInfo();
            }
        }

        private void ClearCurResult()
        {
            CurResult = EmptyParseResult.Instance;
        }

        private void ltbSuggests_SelectedIndexChanged(object sender, EventArgs e)
        {
            var result = ltbSuggests.SelectedItem as IParseResult;
            CurResult = result;
        }

        private void SetCmdInfo()
        {
            lblAlias.Text = CurResult.IsNotNull()
                ? CurResult.DisplayInfo
                : string.Empty;
        }

        private void ltbSuggests_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender != ltbSuggests)
            {
                ltbSuggests.Focus();
            }

            if (ltbSuggests.Items.Count > 0)
            {
                if (e.KeyCode == Keys.Down)
                {
                    ltbSuggests.SelectedIndex = (ltbSuggests.SelectedIndex + 1) % ltbSuggests.Items.Count;
                }
                else if (e.KeyCode == Keys.Up)
                {
                    ltbSuggests.SelectedIndex = (ltbSuggests.SelectedIndex - 1 + ltbSuggests.Items.Count) % ltbSuggests.Items.Count;
                }

                e.Handled = true;
            }
        }

        #endregion
    }

    public enum KeyModifiers
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        Windows = 8
    }
}
