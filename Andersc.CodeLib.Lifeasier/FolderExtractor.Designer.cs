namespace Andersc.CodeLib.Lifeasier
{
    partial class FolderExtractor
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
            this.dlgOpenFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.ckbWithFiles = new System.Windows.Forms.CheckBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.ckbOverwrite = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(14, 25);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(106, 25);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.ReadOnly = true;
            this.txtFolder.Size = new System.Drawing.Size(468, 21);
            this.txtFolder.TabIndex = 1;
            // 
            // ckbWithFiles
            // 
            this.ckbWithFiles.AutoSize = true;
            this.ckbWithFiles.Location = new System.Drawing.Point(14, 62);
            this.ckbWithFiles.Name = "ckbWithFiles";
            this.ckbWithFiles.Size = new System.Drawing.Size(84, 16);
            this.ckbWithFiles.TabIndex = 2;
            this.ckbWithFiles.Text = "With files";
            this.ckbWithFiles.UseVisualStyleBackColor = true;
            this.ckbWithFiles.CheckedChanged += new System.EventHandler(this.ckbWithFiles_CheckedChanged);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(106, 60);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(368, 21);
            this.txtFilter.TabIndex = 3;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(14, 156);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessage.Size = new System.Drawing.Size(560, 268);
            this.txtMessage.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Message:";
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(14, 90);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(84, 23);
            this.btnGo.TabIndex = 6;
            this.btnGo.Text = "Ready, Go!";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // ckbOverwrite
            // 
            this.ckbOverwrite.AutoSize = true;
            this.ckbOverwrite.Location = new System.Drawing.Point(490, 61);
            this.ckbOverwrite.Name = "ckbOverwrite";
            this.ckbOverwrite.Size = new System.Drawing.Size(84, 16);
            this.ckbOverwrite.TabIndex = 7;
            this.ckbOverwrite.Text = "Overwrite?";
            this.ckbOverwrite.UseVisualStyleBackColor = true;
            // 
            // FolderExtractor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 470);
            this.Controls.Add(this.ckbOverwrite);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.ckbWithFiles);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.btnBrowse);
            this.Name = "FolderExtractor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Folder Extractor";
            this.Load += new System.EventHandler(this.FolderExtractor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.CheckBox ckbOverwrite;
        private System.Windows.Forms.CheckBox ckbWithFiles;
        private System.Windows.Forms.FolderBrowserDialog dlgOpenFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.TextBox txtMessage;


    }
}