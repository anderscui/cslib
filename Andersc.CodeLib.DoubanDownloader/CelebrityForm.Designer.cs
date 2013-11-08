namespace Andersc.CodeLib.DoubanDownloader
{
    partial class CelebrityForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CelebrityForm));
            this.btnDownload = new System.Windows.Forms.Button();
            this.lblAlbumId = new System.Windows.Forms.Label();
            this.txtAlbumID = new System.Windows.Forms.TextBox();
            this.dlgBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.txtTips = new System.Windows.Forms.TextBox();
            this.lblDownloadInfo = new System.Windows.Forms.Label();
            this.txtLocalAlbumDir = new System.Windows.Forms.TextBox();
            this.lnkOpen = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(12, 81);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(69, 25);
            this.btnDownload.TabIndex = 0;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // lblAlbumId
            // 
            this.lblAlbumId.AutoSize = true;
            this.lblAlbumId.Location = new System.Drawing.Point(9, 49);
            this.lblAlbumId.Name = "lblAlbumId";
            this.lblAlbumId.Size = new System.Drawing.Size(53, 13);
            this.lblAlbumId.TabIndex = 1;
            this.lblAlbumId.Text = "Album ID:";
            // 
            // txtAlbumID
            // 
            this.txtAlbumID.Location = new System.Drawing.Point(74, 46);
            this.txtAlbumID.Name = "txtAlbumID";
            this.txtAlbumID.Size = new System.Drawing.Size(336, 20);
            this.txtAlbumID.TabIndex = 2;
            // 
            // txtTips
            // 
            this.txtTips.Location = new System.Drawing.Point(12, 13);
            this.txtTips.Multiline = true;
            this.txtTips.Name = "txtTips";
            this.txtTips.ReadOnly = true;
            this.txtTips.Size = new System.Drawing.Size(398, 24);
            this.txtTips.TabIndex = 3;
            // 
            // lblDownloadInfo
            // 
            this.lblDownloadInfo.AutoSize = true;
            this.lblDownloadInfo.Location = new System.Drawing.Point(87, 87);
            this.lblDownloadInfo.Name = "lblDownloadInfo";
            this.lblDownloadInfo.Size = new System.Drawing.Size(73, 13);
            this.lblDownloadInfo.TabIndex = 4;
            this.lblDownloadInfo.Text = "DownloadInfo";
            // 
            // txtLocalAlbumDir
            // 
            this.txtLocalAlbumDir.Location = new System.Drawing.Point(12, 122);
            this.txtLocalAlbumDir.Name = "txtLocalAlbumDir";
            this.txtLocalAlbumDir.ReadOnly = true;
            this.txtLocalAlbumDir.Size = new System.Drawing.Size(335, 20);
            this.txtLocalAlbumDir.TabIndex = 5;
            // 
            // lnkOpen
            // 
            this.lnkOpen.AutoSize = true;
            this.lnkOpen.Location = new System.Drawing.Point(353, 129);
            this.lnkOpen.Name = "lnkOpen";
            this.lnkOpen.Size = new System.Drawing.Size(51, 13);
            this.lnkOpen.TabIndex = 7;
            this.lnkOpen.TabStop = true;
            this.lnkOpen.Text = "Open It...";
            this.lnkOpen.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkOpen_LinkClicked);
            // 
            // CelebrityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 153);
            this.Controls.Add(this.lnkOpen);
            this.Controls.Add(this.txtLocalAlbumDir);
            this.Controls.Add(this.lblDownloadInfo);
            this.Controls.Add(this.txtTips);
            this.Controls.Add(this.txtAlbumID);
            this.Controls.Add(this.lblAlbumId);
            this.Controls.Add(this.btnDownload);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CelebrityForm";
            this.Opacity = 0.6D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Douban Celebrity Downloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label lblAlbumId;
        private System.Windows.Forms.TextBox txtAlbumID;
        private System.Windows.Forms.FolderBrowserDialog dlgBrowse;
        private System.Windows.Forms.TextBox txtTips;
        private System.Windows.Forms.Label lblDownloadInfo;
        private System.Windows.Forms.TextBox txtLocalAlbumDir;
        private System.Windows.Forms.LinkLabel lnkOpen;
    }
}

