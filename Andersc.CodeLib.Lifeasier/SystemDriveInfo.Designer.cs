namespace Andersc.CodeLib.Lifeasier
{
    partial class SystemDriveInfo
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
            this.cboDrives = new System.Windows.Forms.ComboBox();
            this.txtDetails = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cboDrives
            // 
            this.cboDrives.FormattingEnabled = true;
            this.cboDrives.Location = new System.Drawing.Point(12, 12);
            this.cboDrives.Name = "cboDrives";
            this.cboDrives.Size = new System.Drawing.Size(158, 20);
            this.cboDrives.TabIndex = 0;
            this.cboDrives.SelectedIndexChanged += new System.EventHandler(this.cboDrives_SelectedIndexChanged);
            // 
            // txtDetails
            // 
            this.txtDetails.Location = new System.Drawing.Point(13, 48);
            this.txtDetails.Multiline = true;
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.Size = new System.Drawing.Size(405, 291);
            this.txtDetails.TabIndex = 1;
            // 
            // SystemDriveInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 385);
            this.Controls.Add(this.txtDetails);
            this.Controls.Add(this.cboDrives);
            this.Name = "SystemDriveInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SystemDriveInfo";
            this.Load += new System.EventHandler(this.SystemDriveInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboDrives;
        private System.Windows.Forms.TextBox txtDetails;
    }
}