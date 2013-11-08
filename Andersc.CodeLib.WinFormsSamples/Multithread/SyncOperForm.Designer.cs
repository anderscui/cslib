namespace Andersc.CodeLib.WinFormsSamples.Multithread
{
    partial class SyncOperForm
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
            this.pbResult = new System.Windows.Forms.ProgressBar();
            this.btnDo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pbResult
            // 
            this.pbResult.Location = new System.Drawing.Point(12, 73);
            this.pbResult.Name = "pbResult";
            this.pbResult.Size = new System.Drawing.Size(328, 23);
            this.pbResult.TabIndex = 0;
            // 
            // btnDo
            // 
            this.btnDo.Location = new System.Drawing.Point(13, 26);
            this.btnDo.Name = "btnDo";
            this.btnDo.Size = new System.Drawing.Size(75, 23);
            this.btnDo.TabIndex = 1;
            this.btnDo.Text = "Do";
            this.btnDo.UseVisualStyleBackColor = true;
            this.btnDo.Click += new System.EventHandler(this.btnDo_Click);
            // 
            // SyncOperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 252);
            this.Controls.Add(this.btnDo);
            this.Controls.Add(this.pbResult);
            this.Name = "SyncOperForm";
            this.Text = "SyncOperForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbResult;
        private System.Windows.Forms.Button btnDo;
    }
}