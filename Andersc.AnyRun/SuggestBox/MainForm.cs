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

using PopupControl;

using Andersc.CodeLib.Common;
using Andersc.CodeLib.Common.WinForm;
using Andersc.AnyRun.Parsers;
using Andersc.AnyRun.UserControls;

namespace Andersc.AnyRun
{
    public partial class MainForm : Form
    {
        private Keys[] upDownArrows = new Keys[] { Keys.Up, Keys.Down };
        private Keys[] arrowKeys = new Keys[] { Keys.Left, Keys.Right, Keys.Up, Keys.Down };

        private int hkid;
        private List<IParser> parsers;

        private Popup complex;
        private Suggests sug;

        public MainForm()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            var backColor = Color.FromArgb(50, 101, 101);

            BackColor = backColor;
            lblAlias.BackColor = backColor;
            txtInput.BackColor = backColor;

            lblAlias.Text = string.Empty;

            sug = new Suggests();
            complex = new Popup(sug);
            //complex.Resizable = true;

            parsers = ParserBuilder.GetParsers();
            //ltbSuggests.DisplayMember = "DisplayInfo";
            sug.SuggestBox.DisplayMember = "DisplayInfo";

            sug.SuggestBox.SelectedIndexChanged += SuggestBox_SelectedIndexChanged;
            sug.SuggestBox.KeyDown += SuggestBox_KeyDown;
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
                        ShowConsole();
                    }
                    break;
            }
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

        public void MainForm_KeyPress(object sender, KeyPressEventArgs e)
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

            var focusedCtrl = FindFocusedControl(this);
            if (focusedCtrl != txtInput)
            {
                txtInput_KeyPress(sender, e);
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            var focusedCtrl = FindFocusedControl(this);
            if (focusedCtrl != sug.SuggestBox
                && (e.KeyCode.In(upDownArrows)))
            {
                e.Handled = true;
                SuggestBox_KeyDown(sender, e);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            API.UnregisterHotKey(Handle, hkid);
        }

        #endregion

        #region Notify Icon Handlers

        private void notifyExit_Click(object sender, EventArgs e)
        {
            ExitApp();
        }

        private void notifyShowConsole_Click(object sender, EventArgs e)
        {
            ShowConsole();
        }

        private void notifyCxtMenu_DoubleClick(object sender, EventArgs e)
        {
            ShowConsole();
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

        private void HideConsole()
        {
            //WindowState = FormWindowState.Minimized;
            Hide();
        }

        private void ShowConsole()
        {
            //WindowState = FormWindowState.Normal;
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
            sug.SuggestBox.Items.Clear();

            var input = txtInput.Text;
            if (input.IsBlank())
            {
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
                sug.SuggestBox.Items.Add(result);
            }

            if (sug.SuggestBox.Items.Count > 0)
            {
                complex.Show(sender as TextBox);
                sug.SuggestBox.SelectedIndex = 0;
            }
            else
            {
                complex.Hide();
            }
        }

        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender != txtInput)
            {
                txtInput.Focus();
                txtInput.Text += e.KeyChar.ToString();

                txtInput.SelectionStart = txtInput.Text.Length;
                txtInput.SelectionLength = 0;
            }
        }

        #endregion

        #region Suggests

        private IParseResult CurResult { get; set; }

        private void ltbSuggests_SelectedIndexChanged(object sender, EventArgs e)
        {
            var result = ltbSuggests.SelectedItem as IParseResult;
            CurResult = result;
            SetCmdInfo();
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

        private void SuggestBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var result = sug.SuggestBox.SelectedItem as IParseResult;
            CurResult = result;
            SetCmdInfo();
        }

        private void SuggestBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender != sug.SuggestBox)
            {
                sug.SuggestBox.Focus();
            }

            if (sug.SuggestBox.Items.Count > 0)
            {
                if (e.KeyCode == Keys.Down)
                {
                    sug.SuggestBox.SelectedIndex = (sug.SuggestBox.SelectedIndex + 1) % sug.SuggestBox.Items.Count;
                }
                else if (e.KeyCode == Keys.Up)
                {
                    sug.SuggestBox.SelectedIndex = (sug.SuggestBox.SelectedIndex - 1 + sug.SuggestBox.Items.Count) % sug.SuggestBox.Items.Count;
                }

                e.Handled = true;
            }
        }
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
