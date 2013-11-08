namespace Andersc.CodeLib.WinFormsSamples.Multithread
{
    partial class CoolBackgroundWorker
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
            this.nudLoops = new System.Windows.Forms.NumericUpDown();
            this.btnDo = new System.Windows.Forms.Button();
            this.pbResult = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.nudLoops)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Loop Value:";
            // 
            // nudLoops
            // 
            this.nudLoops.Location = new System.Drawing.Point(124, 22);
            this.nudLoops.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudLoops.Name = "nudLoops";
            this.nudLoops.Size = new System.Drawing.Size(120, 20);
            this.nudLoops.TabIndex = 12;
            this.nudLoops.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnDo
            // 
            this.btnDo.Location = new System.Drawing.Point(260, 18);
            this.btnDo.Name = "btnDo";
            this.btnDo.Size = new System.Drawing.Size(75, 25);
            this.btnDo.TabIndex = 11;
            this.btnDo.Text = "Do";
            this.btnDo.UseVisualStyleBackColor = true;
            this.btnDo.Click += new System.EventHandler(this.btnDo_Click);
            // 
            // pbResult
            // 
            this.pbResult.Location = new System.Drawing.Point(124, 62);
            this.pbResult.Name = "pbResult";
            this.pbResult.Size = new System.Drawing.Size(319, 25);
            this.pbResult.TabIndex = 10;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(23, 62);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 14;
            this.lblStatus.Text = "Status";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // CoolBackgroundWorker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 116);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudLoops);
            this.Controls.Add(this.btnDo);
            this.Controls.Add(this.pbResult);
            this.Name = "CoolBackgroundWorker";
            this.Text = "CoolBackgroundWorker";
            ((System.ComponentModel.ISupportInitialize)(this.nudLoops)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudLoops;
        private System.Windows.Forms.Button btnDo;
        private System.Windows.Forms.ProgressBar pbResult;
        private System.Windows.Forms.Label lblStatus;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}