namespace Andersc.CodeLib.WinFormsSamples.StandardControls
{
    partial class OwnerDrawCtrlForm
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
            this.ltbOwnerDraw = new System.Windows.Forms.ListBox();
            this.status = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.status.SuspendLayout();
            this.SuspendLayout();
            // 
            // ltbOwnerDraw
            // 
            this.ltbOwnerDraw.FormattingEnabled = true;
            this.ltbOwnerDraw.ItemHeight = 12;
            this.ltbOwnerDraw.Location = new System.Drawing.Point(30, 22);
            this.ltbOwnerDraw.Name = "ltbOwnerDraw";
            this.ltbOwnerDraw.Size = new System.Drawing.Size(364, 172);
            this.ltbOwnerDraw.TabIndex = 0;
            // 
            // status
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.status.Location = new System.Drawing.Point(0, 227);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(458, 22);
            this.status.TabIndex = 1;
            this.status.Text = "Bottom Status Bar";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(173, 17);
            this.statusLabel.Text = "Next panel is OwnerDrawn -->";
            // 
            // OwnerDrawCtrlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 249);
            this.Controls.Add(this.status);
            this.Controls.Add(this.ltbOwnerDraw);
            this.Name = "OwnerDrawCtrlForm";
            this.Text = "OwnerDrawCtrlForm";
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ltbOwnerDraw;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
    }
}