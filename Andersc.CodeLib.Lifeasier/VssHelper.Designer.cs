namespace Andersc.CodeLib.Lifeasier
{
    partial class VssHelper
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.ltbFilesCheckedOutItems = new System.Windows.Forms.ListBox();
            this.btnCheckinSelected = new System.Windows.Forms.Button();
            this.btnCheckinAll = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblServerPath = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSourcePath = new System.Windows.Forms.TextBox();
            this.chbIncludingItemsCheckedOutByOthers = new System.Windows.Forms.CheckBox();
            this.btnCopyPaths = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Gray;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(444, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "You can set the logging info in app.config file or Options dialog.";
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(15, 121);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(123, 21);
            this.btnFind.TabIndex = 1;
            this.btnFind.Text = "&Find Checkouts";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // ltbFilesCheckedOutItems
            // 
            this.ltbFilesCheckedOutItems.FormattingEnabled = true;
            this.ltbFilesCheckedOutItems.HorizontalScrollbar = true;
            this.ltbFilesCheckedOutItems.ItemHeight = 12;
            this.ltbFilesCheckedOutItems.Location = new System.Drawing.Point(15, 148);
            this.ltbFilesCheckedOutItems.Name = "ltbFilesCheckedOutItems";
            this.ltbFilesCheckedOutItems.ScrollAlwaysVisible = true;
            this.ltbFilesCheckedOutItems.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ltbFilesCheckedOutItems.Size = new System.Drawing.Size(655, 256);
            this.ltbFilesCheckedOutItems.TabIndex = 2;
            // 
            // btnCheckinSelected
            // 
            this.btnCheckinSelected.Location = new System.Drawing.Point(15, 423);
            this.btnCheckinSelected.Name = "btnCheckinSelected";
            this.btnCheckinSelected.Size = new System.Drawing.Size(171, 21);
            this.btnCheckinSelected.TabIndex = 3;
            this.btnCheckinSelected.Text = "Checkin the Selected Items";
            this.btnCheckinSelected.UseVisualStyleBackColor = true;
            this.btnCheckinSelected.Click += new System.EventHandler(this.btnCheckinSelected_Click);
            // 
            // btnCheckinAll
            // 
            this.btnCheckinAll.Location = new System.Drawing.Point(205, 423);
            this.btnCheckinAll.Name = "btnCheckinAll";
            this.btnCheckinAll.Size = new System.Drawing.Size(93, 21);
            this.btnCheckinAll.TabIndex = 4;
            this.btnCheckinAll.Text = "Checkin All";
            this.btnCheckinAll.UseVisualStyleBackColor = true;
            this.btnCheckinAll.Click += new System.EventHandler(this.btnCheckinAll_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "User Name: ";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(97, 41);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(41, 12);
            this.lblUserName.TabIndex = 6;
            this.lblUserName.Text = "label3";
            // 
            // lblServerPath
            // 
            this.lblServerPath.AutoSize = true;
            this.lblServerPath.Location = new System.Drawing.Point(97, 65);
            this.lblServerPath.Name = "lblServerPath";
            this.lblServerPath.Size = new System.Drawing.Size(41, 12);
            this.lblServerPath.TabIndex = 8;
            this.lblServerPath.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Server Path: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "Root Source: ";
            // 
            // txtSourcePath
            // 
            this.txtSourcePath.Location = new System.Drawing.Point(99, 85);
            this.txtSourcePath.Name = "txtSourcePath";
            this.txtSourcePath.Size = new System.Drawing.Size(462, 21);
            this.txtSourcePath.TabIndex = 10;
            // 
            // chbIncludingItemsCheckedOutByOthers
            // 
            this.chbIncludingItemsCheckedOutByOthers.AutoSize = true;
            this.chbIncludingItemsCheckedOutByOthers.Location = new System.Drawing.Point(153, 124);
            this.chbIncludingItemsCheckedOutByOthers.Name = "chbIncludingItemsCheckedOutByOthers";
            this.chbIncludingItemsCheckedOutByOthers.Size = new System.Drawing.Size(234, 16);
            this.chbIncludingItemsCheckedOutByOthers.TabIndex = 11;
            this.chbIncludingItemsCheckedOutByOthers.Text = "Including Items CheckedOut ByOthers";
            this.chbIncludingItemsCheckedOutByOthers.UseVisualStyleBackColor = true;
            // 
            // btnCopyPaths
            // 
            this.btnCopyPaths.Location = new System.Drawing.Point(319, 423);
            this.btnCopyPaths.Name = "btnCopyPaths";
            this.btnCopyPaths.Size = new System.Drawing.Size(137, 23);
            this.btnCopyPaths.TabIndex = 12;
            this.btnCopyPaths.Text = "Copy Selected Paths";
            this.btnCopyPaths.UseVisualStyleBackColor = true;
            this.btnCopyPaths.Click += new System.EventHandler(this.btnCopyPaths_Click);
            // 
            // VssHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 468);
            this.Controls.Add(this.btnCopyPaths);
            this.Controls.Add(this.chbIncludingItemsCheckedOutByOthers);
            this.Controls.Add(this.txtSourcePath);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblServerPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCheckinAll);
            this.Controls.Add(this.btnCheckinSelected);
            this.Controls.Add(this.ltbFilesCheckedOutItems);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.label1);
            this.Name = "VssHelper";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VSS Helper";
            this.Load += new System.EventHandler(this.VssHelper_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.ListBox ltbFilesCheckedOutItems;
        private System.Windows.Forms.Button btnCheckinSelected;
        private System.Windows.Forms.Button btnCheckinAll;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblServerPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSourcePath;
        private System.Windows.Forms.CheckBox chbIncludingItemsCheckedOutByOthers;
        private System.Windows.Forms.Button btnCopyPaths;
    }
}