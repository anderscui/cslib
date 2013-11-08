namespace Andersc.CodeLib.WinFormsSamples.Multithread
{
    partial class AsyncOperForm
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
            this.btnDo = new System.Windows.Forms.Button();
            this.pbResult = new System.Windows.Forms.ProgressBar();
            this.nudLoops = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudLoops)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDo
            // 
            this.btnDo.Location = new System.Drawing.Point(228, 24);
            this.btnDo.Name = "btnDo";
            this.btnDo.Size = new System.Drawing.Size(75, 23);
            this.btnDo.TabIndex = 3;
            this.btnDo.Text = "Do";
            this.btnDo.UseVisualStyleBackColor = true;
            this.btnDo.Click += new System.EventHandler(this.btnDo_Click);
            // 
            // pbResult
            // 
            this.pbResult.Location = new System.Drawing.Point(11, 71);
            this.pbResult.Name = "pbResult";
            this.pbResult.Size = new System.Drawing.Size(328, 23);
            this.pbResult.TabIndex = 2;
            // 
            // nudLoops
            // 
            this.nudLoops.Location = new System.Drawing.Point(89, 27);
            this.nudLoops.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudLoops.Name = "nudLoops";
            this.nudLoops.Size = new System.Drawing.Size(120, 21);
            this.nudLoops.TabIndex = 4;
            this.nudLoops.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Loop Value:";
            // 
            // AsyncOperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 213);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudLoops);
            this.Controls.Add(this.btnDo);
            this.Controls.Add(this.pbResult);
            this.Name = "AsyncOperForm";
            this.Text = "AsyncOperForm";
            ((System.ComponentModel.ISupportInitialize)(this.nudLoops)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDo;
        private System.Windows.Forms.ProgressBar pbResult;
        private System.Windows.Forms.NumericUpDown nudLoops;
        private System.Windows.Forms.Label label1;
    }
}