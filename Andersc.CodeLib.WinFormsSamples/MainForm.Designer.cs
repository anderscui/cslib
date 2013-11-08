namespace Andersc.CodeLib.WinFormsSamples
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGraphics = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGraphicsBasic = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStdCtrl = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStdCtrlOwnerDraw = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStdCtrlDragDrop = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCustomCtrl = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCustomCtrlEllipseLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCustomCtrlFtb = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMultithread = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMultithreadSync = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMultithreadAsync = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMultithreadSimpleAsync = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMultithreadCancelAsync = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMisc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMiscManifestRes = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMiscTypedRes = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMiscAllCultures = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMiscInputLanguages = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMiscThrowExp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMiscCustomConfigSection = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuGraphics,
            this.mnuStdCtrl,
            this.mnuCustomCtrl,
            this.mnuMultithread,
            this.mnuMisc,
            this.mnuHelp});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            resources.ApplyResources(this.mnuFile, "mnuFile");
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            resources.ApplyResources(this.mnuFileExit, "mnuFileExit");
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // mnuGraphics
            // 
            this.mnuGraphics.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGraphicsBasic});
            this.mnuGraphics.Name = "mnuGraphics";
            resources.ApplyResources(this.mnuGraphics, "mnuGraphics");
            // 
            // mnuGraphicsBasic
            // 
            this.mnuGraphicsBasic.Name = "mnuGraphicsBasic";
            resources.ApplyResources(this.mnuGraphicsBasic, "mnuGraphicsBasic");
            this.mnuGraphicsBasic.Click += new System.EventHandler(this.mnuGraphicsBasic_Click);
            // 
            // mnuStdCtrl
            // 
            this.mnuStdCtrl.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuStdCtrlOwnerDraw,
            this.mnuStdCtrlDragDrop});
            this.mnuStdCtrl.Name = "mnuStdCtrl";
            resources.ApplyResources(this.mnuStdCtrl, "mnuStdCtrl");
            // 
            // mnuStdCtrlOwnerDraw
            // 
            this.mnuStdCtrlOwnerDraw.Name = "mnuStdCtrlOwnerDraw";
            resources.ApplyResources(this.mnuStdCtrlOwnerDraw, "mnuStdCtrlOwnerDraw");
            this.mnuStdCtrlOwnerDraw.Click += new System.EventHandler(this.mnuStdCtrlOwnerDraw_Click);
            // 
            // mnuStdCtrlDragDrop
            // 
            this.mnuStdCtrlDragDrop.Name = "mnuStdCtrlDragDrop";
            resources.ApplyResources(this.mnuStdCtrlDragDrop, "mnuStdCtrlDragDrop");
            this.mnuStdCtrlDragDrop.Click += new System.EventHandler(this.mnuStdCtrlDragDrop_Click);
            // 
            // mnuCustomCtrl
            // 
            this.mnuCustomCtrl.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCustomCtrlEllipseLabel,
            this.mnuCustomCtrlFtb});
            this.mnuCustomCtrl.Name = "mnuCustomCtrl";
            resources.ApplyResources(this.mnuCustomCtrl, "mnuCustomCtrl");
            // 
            // mnuCustomCtrlEllipseLabel
            // 
            this.mnuCustomCtrlEllipseLabel.Name = "mnuCustomCtrlEllipseLabel";
            resources.ApplyResources(this.mnuCustomCtrlEllipseLabel, "mnuCustomCtrlEllipseLabel");
            this.mnuCustomCtrlEllipseLabel.Click += new System.EventHandler(this.mnuCustomCtrlEllipseLabel_Click);
            // 
            // mnuCustomCtrlFtb
            // 
            this.mnuCustomCtrlFtb.Name = "mnuCustomCtrlFtb";
            resources.ApplyResources(this.mnuCustomCtrlFtb, "mnuCustomCtrlFtb");
            this.mnuCustomCtrlFtb.Click += new System.EventHandler(this.mnuCustomCtrlFtb_Click);
            // 
            // mnuMultithread
            // 
            this.mnuMultithread.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMultithreadSync,
            this.mnuMultithreadAsync,
            this.mnuMultithreadSimpleAsync,
            this.mnuMultithreadCancelAsync});
            this.mnuMultithread.Name = "mnuMultithread";
            resources.ApplyResources(this.mnuMultithread, "mnuMultithread");
            // 
            // mnuMultithreadSync
            // 
            this.mnuMultithreadSync.Name = "mnuMultithreadSync";
            resources.ApplyResources(this.mnuMultithreadSync, "mnuMultithreadSync");
            this.mnuMultithreadSync.Click += new System.EventHandler(this.mnuMultithreadSync_Click);
            // 
            // mnuMultithreadAsync
            // 
            this.mnuMultithreadAsync.Name = "mnuMultithreadAsync";
            resources.ApplyResources(this.mnuMultithreadAsync, "mnuMultithreadAsync");
            this.mnuMultithreadAsync.Click += new System.EventHandler(this.mnuMultithreadAsync_Click);
            // 
            // mnuMultithreadSimpleAsync
            // 
            this.mnuMultithreadSimpleAsync.Name = "mnuMultithreadSimpleAsync";
            resources.ApplyResources(this.mnuMultithreadSimpleAsync, "mnuMultithreadSimpleAsync");
            this.mnuMultithreadSimpleAsync.Click += new System.EventHandler(this.mnuMultithreadSimpleAsync_Click);
            // 
            // mnuMultithreadCancelAsync
            // 
            this.mnuMultithreadCancelAsync.Name = "mnuMultithreadCancelAsync";
            resources.ApplyResources(this.mnuMultithreadCancelAsync, "mnuMultithreadCancelAsync");
            this.mnuMultithreadCancelAsync.Click += new System.EventHandler(this.mnuMultithreadCancelAsync_Click);
            // 
            // mnuMisc
            // 
            this.mnuMisc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMiscManifestRes,
            this.mnuMiscTypedRes,
            this.mnuMiscAllCultures,
            this.mnuMiscInputLanguages,
            this.mnuMiscThrowExp,
            this.mnuMiscCustomConfigSection});
            this.mnuMisc.Name = "mnuMisc";
            resources.ApplyResources(this.mnuMisc, "mnuMisc");
            // 
            // mnuMiscManifestRes
            // 
            this.mnuMiscManifestRes.Name = "mnuMiscManifestRes";
            resources.ApplyResources(this.mnuMiscManifestRes, "mnuMiscManifestRes");
            this.mnuMiscManifestRes.Click += new System.EventHandler(this.mnuMiscManifestRes_Click);
            // 
            // mnuMiscTypedRes
            // 
            this.mnuMiscTypedRes.Name = "mnuMiscTypedRes";
            resources.ApplyResources(this.mnuMiscTypedRes, "mnuMiscTypedRes");
            this.mnuMiscTypedRes.Click += new System.EventHandler(this.mnuMiscTypedRes_Click);
            // 
            // mnuMiscAllCultures
            // 
            this.mnuMiscAllCultures.Name = "mnuMiscAllCultures";
            resources.ApplyResources(this.mnuMiscAllCultures, "mnuMiscAllCultures");
            this.mnuMiscAllCultures.Click += new System.EventHandler(this.mnuMiscAllCultures_Click);
            // 
            // mnuMiscInputLanguages
            // 
            this.mnuMiscInputLanguages.Name = "mnuMiscInputLanguages";
            resources.ApplyResources(this.mnuMiscInputLanguages, "mnuMiscInputLanguages");
            this.mnuMiscInputLanguages.Click += new System.EventHandler(this.mnuMiscInputLanguages_Click);
            // 
            // mnuMiscThrowExp
            // 
            this.mnuMiscThrowExp.Name = "mnuMiscThrowExp";
            resources.ApplyResources(this.mnuMiscThrowExp, "mnuMiscThrowExp");
            this.mnuMiscThrowExp.Click += new System.EventHandler(this.mnuMiscThrowExp_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpAbout});
            this.mnuHelp.Name = "mnuHelp";
            resources.ApplyResources(this.mnuHelp, "mnuHelp");
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            resources.ApplyResources(this.mnuHelpAbout, "mnuHelpAbout");
            this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
            // 
            // mnuMiscCustomConfigSection
            // 
            this.mnuMiscCustomConfigSection.Name = "mnuMiscCustomConfigSection";
            resources.ApplyResources(this.mnuMiscCustomConfigSection, "mnuMiscCustomConfigSection");
            this.mnuMiscCustomConfigSection.Click += new System.EventHandler(this.mnuMiscCustomConfigSection_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolStripMenuItem mnuGraphics;
        private System.Windows.Forms.ToolStripMenuItem mnuGraphicsBasic;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuMultithread;
        private System.Windows.Forms.ToolStripMenuItem mnuMultithreadSync;
        private System.Windows.Forms.ToolStripMenuItem mnuMultithreadAsync;
        private System.Windows.Forms.ToolStripMenuItem mnuMultithreadSimpleAsync;
        private System.Windows.Forms.ToolStripMenuItem mnuMultithreadCancelAsync;
        private System.Windows.Forms.ToolStripMenuItem mnuCustomCtrl;
        private System.Windows.Forms.ToolStripMenuItem mnuCustomCtrlEllipseLabel;
        private System.Windows.Forms.ToolStripMenuItem mnuStdCtrl;
        private System.Windows.Forms.ToolStripMenuItem mnuStdCtrlOwnerDraw;
        private System.Windows.Forms.ToolStripMenuItem mnuCustomCtrlFtb;
        private System.Windows.Forms.ToolStripMenuItem mnuStdCtrlDragDrop;
        private System.Windows.Forms.ToolStripMenuItem mnuMisc;
        private System.Windows.Forms.ToolStripMenuItem mnuMiscManifestRes;
        private System.Windows.Forms.ToolStripMenuItem mnuMiscTypedRes;
        private System.Windows.Forms.ToolStripMenuItem mnuMiscAllCultures;
        private System.Windows.Forms.ToolStripMenuItem mnuMiscInputLanguages;
        private System.Windows.Forms.ToolStripMenuItem mnuMiscThrowExp;
        private System.Windows.Forms.ToolStripMenuItem mnuMiscCustomConfigSection;
    }
}

