namespace Andersc.CodeLib.WinFormsSamples.CustomControlsForms
{
    partial class FileTextBoxForm
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
            this.txtFileName = new CodeLib.WinFormsSamples.CustomControls.FileTextBox();
            this.fileBrowseTextBox1 = new CodeLib.WinFormsSamples.CustomControls.FileBrowseTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Input a file name: ";
            // 
            // txtFileName
            // 
            this.txtFileName.ForeColor = System.Drawing.Color.Red;
            this.txtFileName.Location = new System.Drawing.Point(31, 47);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(320, 21);
            this.txtFileName.TabIndex = 0;
            this.txtFileName.TextChanged += new System.EventHandler(this.txtFileName_TextChanged);
            // 
            // fileBrowseTextBox1
            // 
            this.fileBrowseTextBox1.Location = new System.Drawing.Point(19, 89);
            this.fileBrowseTextBox1.Name = "fileBrowseTextBox1";
            this.fileBrowseTextBox1.Size = new System.Drawing.Size(332, 42);
            this.fileBrowseTextBox1.TabIndex = 2;
            // 
            // FileTextBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 319);
            this.Controls.Add(this.fileBrowseTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFileName);
            this.Name = "FileTextBoxForm";
            this.Text = "FileTextBoxForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomControls.FileTextBox txtFileName;
        private System.Windows.Forms.Label label1;
        private CustomControls.FileBrowseTextBox fileBrowseTextBox1;
    }
}