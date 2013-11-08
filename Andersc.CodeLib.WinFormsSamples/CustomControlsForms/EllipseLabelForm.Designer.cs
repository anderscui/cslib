namespace Andersc.CodeLib.WinFormsSamples.CustomControlsForms
{
    partial class EllipseLabelForm
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
            this.btnChangeText = new System.Windows.Forms.Button();
            this.btnChangePrefix = new System.Windows.Forms.Button();
            this.elblSample = new CodeLib.WinFormsSamples.CustomControls.EllipseLabel();
            this.SuspendLayout();
            // 
            // btnChangeText
            // 
            this.btnChangeText.Location = new System.Drawing.Point(21, 91);
            this.btnChangeText.Name = "btnChangeText";
            this.btnChangeText.Size = new System.Drawing.Size(227, 23);
            this.btnChangeText.TabIndex = 1;
            this.btnChangeText.Text = "Change Text";
            this.btnChangeText.UseVisualStyleBackColor = true;
            this.btnChangeText.Click += new System.EventHandler(this.btnChangeText_Click);
            // 
            // btnChangePrefix
            // 
            this.btnChangePrefix.Location = new System.Drawing.Point(21, 135);
            this.btnChangePrefix.Name = "btnChangePrefix";
            this.btnChangePrefix.Size = new System.Drawing.Size(227, 23);
            this.btnChangePrefix.TabIndex = 2;
            this.btnChangePrefix.Text = "Change Prefix";
            this.btnChangePrefix.UseVisualStyleBackColor = true;
            this.btnChangePrefix.Click += new System.EventHandler(this.btnChangePrefix_Click);
            // 
            // elblSample
            // 
            this.elblSample.BackColor = System.Drawing.SystemColors.Control;
            this.elblSample.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.elblSample.Location = new System.Drawing.Point(21, 26);
            this.elblSample.Name = "elblSample";
            this.elblSample.Prefix = "Fuck: ";
            this.elblSample.Size = new System.Drawing.Size(227, 34);
            this.elblSample.TabIndex = 0;
            this.elblSample.Text = "You!";
            // 
            // EllipseLabelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 274);
            this.Controls.Add(this.btnChangePrefix);
            this.Controls.Add(this.btnChangeText);
            this.Controls.Add(this.elblSample);
            this.Name = "EllipseLabelForm";
            this.Text = "EllipseLabelForm";
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControls.EllipseLabel elblSample;
        private System.Windows.Forms.Button btnChangeText;
        private System.Windows.Forms.Button btnChangePrefix;
    }
}