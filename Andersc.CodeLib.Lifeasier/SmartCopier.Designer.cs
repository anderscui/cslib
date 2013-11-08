namespace Andersc.CodeLib.Lifeasier
{
    partial class SmartCopier
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
            this.fileListBox = new System.Windows.Forms.ListBox();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.btnRemoveFile = new System.Windows.Forms.Button();
            this.lblCommonPath = new System.Windows.Forms.Label();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFileCount = new System.Windows.Forms.Label();
            this.btnCopyFile = new System.Windows.Forms.Button();
            this.dlgOpenFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.chkReserveRelativePath = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // fileListBox
            // 
            this.fileListBox.FormattingEnabled = true;
            this.fileListBox.ItemHeight = 12;
            this.fileListBox.Location = new System.Drawing.Point(12, 12);
            this.fileListBox.Name = "fileListBox";
            this.fileListBox.ScrollAlwaysVisible = true;
            this.fileListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.fileListBox.Size = new System.Drawing.Size(540, 268);
            this.fileListBox.TabIndex = 0;
            // 
            // btnAddFile
            // 
            this.btnAddFile.Location = new System.Drawing.Point(22, 342);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(75, 23);
            this.btnAddFile.TabIndex = 1;
            this.btnAddFile.Text = "Add File";
            this.btnAddFile.UseVisualStyleBackColor = true;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // btnRemoveFile
            // 
            this.btnRemoveFile.Location = new System.Drawing.Point(116, 342);
            this.btnRemoveFile.Name = "btnRemoveFile";
            this.btnRemoveFile.Size = new System.Drawing.Size(91, 23);
            this.btnRemoveFile.TabIndex = 2;
            this.btnRemoveFile.Text = "Remove File";
            this.btnRemoveFile.UseVisualStyleBackColor = true;
            this.btnRemoveFile.Click += new System.EventHandler(this.btnRemoveFile_Click);
            // 
            // lblCommonPath
            // 
            this.lblCommonPath.AutoSize = true;
            this.lblCommonPath.Location = new System.Drawing.Point(28, 297);
            this.lblCommonPath.Name = "lblCommonPath";
            this.lblCommonPath.Size = new System.Drawing.Size(17, 12);
            this.lblCommonPath.TabIndex = 3;
            this.lblCommonPath.Text = "cp";
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.AddExtension = false;
            this.dlgOpenFile.Multiselect = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 320);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "File Count: ";
            // 
            // lblFileCount
            // 
            this.lblFileCount.AutoSize = true;
            this.lblFileCount.Location = new System.Drawing.Point(106, 320);
            this.lblFileCount.Name = "lblFileCount";
            this.lblFileCount.Size = new System.Drawing.Size(11, 12);
            this.lblFileCount.TabIndex = 5;
            this.lblFileCount.Text = "0";
            // 
            // btnCopyFile
            // 
            this.btnCopyFile.Location = new System.Drawing.Point(435, 342);
            this.btnCopyFile.Name = "btnCopyFile";
            this.btnCopyFile.Size = new System.Drawing.Size(91, 23);
            this.btnCopyFile.TabIndex = 6;
            this.btnCopyFile.Text = "Copy File To";
            this.btnCopyFile.UseVisualStyleBackColor = true;
            this.btnCopyFile.Click += new System.EventHandler(this.btnCopyFile_Click);
            // 
            // chkReserveRelativePath
            // 
            this.chkReserveRelativePath.AutoSize = true;
            this.chkReserveRelativePath.Checked = true;
            this.chkReserveRelativePath.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReserveRelativePath.Location = new System.Drawing.Point(279, 346);
            this.chkReserveRelativePath.Name = "chkReserveRelativePath";
            this.chkReserveRelativePath.Size = new System.Drawing.Size(150, 16);
            this.chkReserveRelativePath.TabIndex = 7;
            this.chkReserveRelativePath.Text = "Reserve Relative Path";
            this.chkReserveRelativePath.UseVisualStyleBackColor = true;
            // 
            // SmartCopier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 377);
            this.Controls.Add(this.chkReserveRelativePath);
            this.Controls.Add(this.btnCopyFile);
            this.Controls.Add(this.lblFileCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCommonPath);
            this.Controls.Add(this.btnRemoveFile);
            this.Controls.Add(this.btnAddFile);
            this.Controls.Add(this.fileListBox);
            this.Name = "SmartCopier";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Smart Copier";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox fileListBox;
        private System.Windows.Forms.Button btnAddFile;
        private System.Windows.Forms.Button btnRemoveFile;
        private System.Windows.Forms.Label lblCommonPath;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFileCount;
        private System.Windows.Forms.Button btnCopyFile;
        private System.Windows.Forms.FolderBrowserDialog dlgOpenFolder;
        private System.Windows.Forms.CheckBox chkReserveRelativePath;
    }
}