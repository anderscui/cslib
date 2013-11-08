namespace Andersc.AnyRun
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblAlias = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyCxtMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.notifyShowConsole = new System.Windows.Forms.ToolStripMenuItem();
            this.notifySepSetting = new System.Windows.Forms.ToolStripSeparator();
            this.notifyReindex = new System.Windows.Forms.ToolStripMenuItem();
            this.notifySetting = new System.Windows.Forms.ToolStripMenuItem();
            this.notifySepHelp = new System.Windows.Forms.ToolStripSeparator();
            this.notifyHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.notifySepExit = new System.Windows.Forms.ToolStripSeparator();
            this.notifyExit = new System.Windows.Forms.ToolStripMenuItem();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.ltbSuggests = new System.Windows.Forms.ListBox();
            this.notifyCxtMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAlias
            // 
            this.lblAlias.AutoSize = true;
            this.lblAlias.Location = new System.Drawing.Point(6, 9);
            this.lblAlias.Name = "lblAlias";
            this.lblAlias.Size = new System.Drawing.Size(28, 13);
            this.lblAlias.TabIndex = 2;
            this.lblAlias.Text = "alias";
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "run rola!";
            this.notifyIcon.ContextMenuStrip = this.notifyCxtMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "AnyRun";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // notifyCxtMenu
            // 
            this.notifyCxtMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.notifyShowConsole,
            this.notifySepSetting,
            this.notifyReindex,
            this.notifySetting,
            this.notifySepHelp,
            this.notifyHelp,
            this.notifyAbout,
            this.notifySepExit,
            this.notifyExit});
            this.notifyCxtMenu.Name = "notifyCxtMenu";
            this.notifyCxtMenu.Size = new System.Drawing.Size(148, 154);
            // 
            // notifyShowConsole
            // 
            this.notifyShowConsole.Name = "notifyShowConsole";
            this.notifyShowConsole.Size = new System.Drawing.Size(147, 22);
            this.notifyShowConsole.Text = "Show console";
            this.notifyShowConsole.Click += new System.EventHandler(this.notifyShowConsole_Click);
            // 
            // notifySepSetting
            // 
            this.notifySepSetting.Name = "notifySepSetting";
            this.notifySepSetting.Size = new System.Drawing.Size(144, 6);
            // 
            // notifyReindex
            // 
            this.notifyReindex.Name = "notifyReindex";
            this.notifyReindex.Size = new System.Drawing.Size(147, 22);
            this.notifyReindex.Text = "Reindex";
            // 
            // notifySetting
            // 
            this.notifySetting.Name = "notifySetting";
            this.notifySetting.Size = new System.Drawing.Size(147, 22);
            this.notifySetting.Text = "Settings";
            // 
            // notifySepHelp
            // 
            this.notifySepHelp.Name = "notifySepHelp";
            this.notifySepHelp.Size = new System.Drawing.Size(144, 6);
            // 
            // notifyHelp
            // 
            this.notifyHelp.Name = "notifyHelp";
            this.notifyHelp.Size = new System.Drawing.Size(147, 22);
            this.notifyHelp.Text = "Help";
            this.notifyHelp.Click += new System.EventHandler(this.notifyHelp_Click);
            // 
            // notifyAbout
            // 
            this.notifyAbout.Name = "notifyAbout";
            this.notifyAbout.Size = new System.Drawing.Size(147, 22);
            this.notifyAbout.Text = "About";
            this.notifyAbout.Click += new System.EventHandler(this.notifyAbout_Click);
            // 
            // notifySepExit
            // 
            this.notifySepExit.Name = "notifySepExit";
            this.notifySepExit.Size = new System.Drawing.Size(144, 6);
            // 
            // notifyExit
            // 
            this.notifyExit.Name = "notifyExit";
            this.notifyExit.Size = new System.Drawing.Size(147, 22);
            this.notifyExit.Text = "Exit";
            this.notifyExit.Click += new System.EventHandler(this.notifyExit_Click);
            // 
            // txtInput
            // 
            this.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInput.HideSelection = false;
            this.txtInput.Location = new System.Drawing.Point(9, 35);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(497, 20);
            this.txtInput.TabIndex = 3;
            this.txtInput.WordWrap = false;
            this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            this.txtInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInput_KeyPress);
            // 
            // ltbSuggests
            // 
            this.ltbSuggests.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ltbSuggests.FormattingEnabled = true;
            this.ltbSuggests.Location = new System.Drawing.Point(9, 66);
            this.ltbSuggests.Name = "ltbSuggests";
            this.ltbSuggests.Size = new System.Drawing.Size(497, 210);
            this.ltbSuggests.TabIndex = 4;
            this.ltbSuggests.SelectedIndexChanged += new System.EventHandler(this.ltbSuggests_SelectedIndexChanged);
            this.ltbSuggests.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ltbSuggests_KeyDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(518, 285);
            this.ControlBox = false;
            this.Controls.Add(this.ltbSuggests);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.lblAlias);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Opacity = 0.8D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AnyRun";
            this.TransparencyKey = System.Drawing.Color.Wheat;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
            this.notifyCxtMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAlias;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip notifyCxtMenu;
        private System.Windows.Forms.ToolStripMenuItem notifyShowConsole;
        private System.Windows.Forms.ToolStripSeparator notifySepSetting;
        private System.Windows.Forms.ToolStripMenuItem notifyReindex;
        private System.Windows.Forms.ToolStripMenuItem notifySetting;
        private System.Windows.Forms.ToolStripSeparator notifySepHelp;
        private System.Windows.Forms.ToolStripMenuItem notifyHelp;
        private System.Windows.Forms.ToolStripMenuItem notifyAbout;
        private System.Windows.Forms.ToolStripSeparator notifySepExit;
        private System.Windows.Forms.ToolStripMenuItem notifyExit;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.ListBox ltbSuggests;
    }
}

