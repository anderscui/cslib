namespace Andersc.CodeLib.Lifeasier
{
    partial class PathFinder
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
            this.tabPathKinds = new System.Windows.Forms.TabControl();
            this.tabSystemPath = new System.Windows.Forms.TabPage();
            this.tabDevPath = new System.Windows.Forms.TabPage();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPathKinds.SuspendLayout();
            this.tabSystemPath.SuspendLayout();
            this.tabDevPath.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPathKinds
            // 
            this.tabPathKinds.Controls.Add(this.tabSystemPath);
            this.tabPathKinds.Controls.Add(this.tabDevPath);
            this.tabPathKinds.Location = new System.Drawing.Point(12, 12);
            this.tabPathKinds.Name = "tabPathKinds";
            this.tabPathKinds.SelectedIndex = 0;
            this.tabPathKinds.Size = new System.Drawing.Size(561, 407);
            this.tabPathKinds.TabIndex = 0;
            // 
            // tabSystemPath
            // 
            this.tabSystemPath.Controls.Add(this.textBox1);
            this.tabSystemPath.Location = new System.Drawing.Point(4, 21);
            this.tabSystemPath.Name = "tabSystemPath";
            this.tabSystemPath.Padding = new System.Windows.Forms.Padding(3);
            this.tabSystemPath.Size = new System.Drawing.Size(553, 382);
            this.tabSystemPath.TabIndex = 0;
            this.tabSystemPath.Text = "System";
            this.tabSystemPath.UseVisualStyleBackColor = true;
            // 
            // tabDevPath
            // 
            this.tabDevPath.Controls.Add(this.listBox1);
            this.tabDevPath.Controls.Add(this.label1);
            this.tabDevPath.Location = new System.Drawing.Point(4, 21);
            this.tabDevPath.Name = "tabDevPath";
            this.tabDevPath.Padding = new System.Windows.Forms.Padding(3);
            this.tabDevPath.Size = new System.Drawing.Size(553, 382);
            this.tabDevPath.TabIndex = 1;
            this.tabDevPath.Text = "Dev";
            this.tabDevPath.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(17, 39);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(497, 136);
            this.listBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = ".NET Framework Set";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 21);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(532, 331);
            this.textBox1.TabIndex = 0;
            // 
            // PathFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 445);
            this.Controls.Add(this.tabPathKinds);
            this.Name = "PathFinder";
            this.Text = "Path Finder";
            this.tabPathKinds.ResumeLayout(false);
            this.tabSystemPath.ResumeLayout(false);
            this.tabSystemPath.PerformLayout();
            this.tabDevPath.ResumeLayout(false);
            this.tabDevPath.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabPathKinds;
        private System.Windows.Forms.TabPage tabSystemPath;
        private System.Windows.Forms.TabPage tabDevPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox1;
    }
}