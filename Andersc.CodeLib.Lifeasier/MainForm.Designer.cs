namespace Andersc.CodeLib.Lifeasier
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
            this.mainMemu = new System.Windows.Forms.MenuStrip();
            this.menuFunctionList = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSmartCopier = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFolderExtractor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPathFinder = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSystemDriveInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSystemFileVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProcesses = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.vSSHelperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVssRemover = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDevTestDbConn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsPop3Mail = new System.Windows.Forms.ToolStripMenuItem();
            this.sMTPMailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLifeOxford = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMemu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMemu
            // 
            this.mainMemu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFunctionList,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.mnuTools,
            this.toolStripMenuItem1,
            this.helpToolStripMenuItem});
            this.mainMemu.Location = new System.Drawing.Point(0, 0);
            this.mainMemu.Name = "mainMemu";
            this.mainMemu.ShowItemToolTips = true;
            this.mainMemu.Size = new System.Drawing.Size(576, 24);
            this.mainMemu.TabIndex = 0;
            this.mainMemu.Text = "mainMenu";
            // 
            // menuFunctionList
            // 
            this.menuFunctionList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSmartCopier,
            this.mnuFolderExtractor,
            this.mnuPathFinder,
            this.mnuSystemDriveInfo,
            this.mnuSystemFileVersion,
            this.mnuProcesses});
            this.menuFunctionList.Name = "menuFunctionList";
            this.menuFunctionList.Size = new System.Drawing.Size(57, 20);
            this.menuFunctionList.Text = "&System";
            // 
            // menuSmartCopier
            // 
            this.menuSmartCopier.Name = "menuSmartCopier";
            this.menuSmartCopier.Size = new System.Drawing.Size(158, 22);
            this.menuSmartCopier.Text = "Smart &Copier";
            this.menuSmartCopier.Click += new System.EventHandler(this.menuPathExtractor_Click);
            // 
            // mnuFolderExtractor
            // 
            this.mnuFolderExtractor.Name = "mnuFolderExtractor";
            this.mnuFolderExtractor.Size = new System.Drawing.Size(158, 22);
            this.mnuFolderExtractor.Text = "&Folder Extractor";
            this.mnuFolderExtractor.Click += new System.EventHandler(this.mnuFolderExtractor_Click);
            // 
            // mnuPathFinder
            // 
            this.mnuPathFinder.Name = "mnuPathFinder";
            this.mnuPathFinder.Size = new System.Drawing.Size(158, 22);
            this.mnuPathFinder.Text = "&Path Finder";
            this.mnuPathFinder.Click += new System.EventHandler(this.mnuPathFinder_Click);
            // 
            // mnuSystemDriveInfo
            // 
            this.mnuSystemDriveInfo.Name = "mnuSystemDriveInfo";
            this.mnuSystemDriveInfo.Size = new System.Drawing.Size(158, 22);
            this.mnuSystemDriveInfo.Text = "&Drives";
            this.mnuSystemDriveInfo.Click += new System.EventHandler(this.mnuSystemDriveInfo_Click);
            // 
            // mnuSystemFileVersion
            // 
            this.mnuSystemFileVersion.Name = "mnuSystemFileVersion";
            this.mnuSystemFileVersion.Size = new System.Drawing.Size(158, 22);
            this.mnuSystemFileVersion.Text = "File &Version Info";
            // 
            // mnuProcesses
            // 
            this.mnuProcesses.Name = "mnuProcesses";
            this.mnuProcesses.Size = new System.Drawing.Size(158, 22);
            this.mnuProcesses.Text = "P&rocesses";
            this.mnuProcesses.Click += new System.EventHandler(this.mnuProcesses_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vSSHelperToolStripMenuItem,
            this.mnuVssRemover,
            this.mnuDevTestDbConn});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(39, 20);
            this.toolStripMenuItem2.Text = "&Dev";
            // 
            // vSSHelperToolStripMenuItem
            // 
            this.vSSHelperToolStripMenuItem.Name = "vSSHelperToolStripMenuItem";
            this.vSSHelperToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.vSSHelperToolStripMenuItem.Text = "&VSS Helper";
            this.vSSHelperToolStripMenuItem.Click += new System.EventHandler(this.vSSHelperToolStripMenuItem_Click);
            // 
            // mnuVssRemover
            // 
            this.mnuVssRemover.Name = "mnuVssRemover";
            this.mnuVssRemover.Size = new System.Drawing.Size(179, 22);
            this.mnuVssRemover.Text = "VSS Remover";
            this.mnuVssRemover.Click += new System.EventHandler(this.mnuVssRemover_Click);
            // 
            // mnuDevTestDbConn
            // 
            this.mnuDevTestDbConn.Name = "mnuDevTestDbConn";
            this.mnuDevTestDbConn.Size = new System.Drawing.Size(179, 22);
            this.mnuDevTestDbConn.Text = "Test &Database Conn";
            this.mnuDevTestDbConn.Click += new System.EventHandler(this.mnuDevTestDbConn_Click);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsPop3Mail,
            this.sMTPMailToolStripMenuItem});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(48, 20);
            this.mnuTools.Text = "&Tools";
            // 
            // mnuToolsPop3Mail
            // 
            this.mnuToolsPop3Mail.Name = "mnuToolsPop3Mail";
            this.mnuToolsPop3Mail.Size = new System.Drawing.Size(152, 22);
            this.mnuToolsPop3Mail.Text = "&Pop3 Mail";
            this.mnuToolsPop3Mail.Click += new System.EventHandler(this.mnuToolsPop3Mail_Click);
            // 
            // sMTPMailToolStripMenuItem
            // 
            this.sMTPMailToolStripMenuItem.Name = "sMTPMailToolStripMenuItem";
            this.sMTPMailToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sMTPMailToolStripMenuItem.Text = "&SMTP Mail";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(61, 20);
            this.toolStripMenuItem1.Text = "&Options";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.preferencesToolStripMenuItem.Text = "&Preferences";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLifeOxford});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(38, 20);
            this.toolStripMenuItem3.Text = "&Life";
            // 
            // menuLifeOxford
            // 
            this.menuLifeOxford.Name = "menuLifeOxford";
            this.menuLifeOxford.Size = new System.Drawing.Size(152, 22);
            this.menuLifeOxford.Text = "Oxford Dict";
            this.menuLifeOxford.Click += new System.EventHandler(this.menuLifeOxford_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 468);
            this.Controls.Add(this.mainMemu);
            this.MainMenuStrip = this.mainMemu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Make your life easier";
            this.mainMemu.ResumeLayout(false);
            this.mainMemu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMemu;
        private System.Windows.Forms.ToolStripMenuItem menuFunctionList;
        private System.Windows.Forms.ToolStripMenuItem menuSmartCopier;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuFolderExtractor;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem vSSHelperToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuPathFinder;
        private System.Windows.Forms.ToolStripMenuItem mnuVssRemover;
        private System.Windows.Forms.ToolStripMenuItem mnuSystemDriveInfo;
        private System.Windows.Forms.ToolStripMenuItem mnuSystemFileVersion;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsPop3Mail;
        private System.Windows.Forms.ToolStripMenuItem sMTPMailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuProcesses;
        private System.Windows.Forms.ToolStripMenuItem mnuDevTestDbConn;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem menuLifeOxford;
    }
}

