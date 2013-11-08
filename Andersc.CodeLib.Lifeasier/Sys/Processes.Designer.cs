namespace Andersc.CodeLib.Lifeasier.Sys
{
    partial class Processes
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
            this.lblProcesses = new System.Windows.Forms.Label();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.lblProcessCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblProcesses
            // 
            this.lblProcesses.AutoSize = true;
            this.lblProcesses.Location = new System.Drawing.Point(12, 20);
            this.lblProcesses.Name = "lblProcesses";
            this.lblProcesses.Size = new System.Drawing.Size(96, 13);
            this.lblProcesses.TabIndex = 0;
            this.lblProcesses.Text = "Runing Processes:";
            // 
            // txtInfo
            // 
            this.txtInfo.Location = new System.Drawing.Point(15, 46);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInfo.Size = new System.Drawing.Size(410, 271);
            this.txtInfo.TabIndex = 1;
            // 
            // lblProcessCount
            // 
            this.lblProcessCount.AutoSize = true;
            this.lblProcessCount.Location = new System.Drawing.Point(105, 20);
            this.lblProcessCount.Name = "lblProcessCount";
            this.lblProcessCount.Size = new System.Drawing.Size(35, 13);
            this.lblProcessCount.TabIndex = 2;
            this.lblProcessCount.Text = "Count";
            // 
            // Processes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 340);
            this.Controls.Add(this.lblProcessCount);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.lblProcesses);
            this.Name = "Processes";
            this.Text = "Processes";
            this.Load += new System.EventHandler(this.Processes_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProcesses;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.Label lblProcessCount;
    }
}