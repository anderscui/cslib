namespace Andersc.AnyRun.UserControls
{
    partial class Suggests
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ltbSuggests = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // ltbSuggests
            // 
            this.ltbSuggests.FormattingEnabled = true;
            this.ltbSuggests.Location = new System.Drawing.Point(0, 0);
            this.ltbSuggests.Name = "ltbSuggests";
            this.ltbSuggests.Size = new System.Drawing.Size(244, 199);
            this.ltbSuggests.TabIndex = 0;
            this.ltbSuggests.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ltbSuggests_KeyPress);
            // 
            // Suggests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.ltbSuggests);
            this.Name = "Suggests";
            this.Size = new System.Drawing.Size(244, 198);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox ltbSuggests;
    }
}
