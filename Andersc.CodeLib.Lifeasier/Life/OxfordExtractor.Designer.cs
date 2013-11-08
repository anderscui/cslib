namespace Andersc.CodeLib.Lifeasier.Life
{
    partial class OxfordExtractor
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
            this.components = new System.ComponentModel.Container();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.RichTextBox();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.btnShowClipboard = new System.Windows.Forms.Button();
            this.clipboardTimer = new System.Windows.Forms.Timer(this.components);
            this.btnSwitch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(7, 307);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtInput
            // 
            this.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInput.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInput.Location = new System.Drawing.Point(7, 12);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(561, 289);
            this.txtInput.TabIndex = 3;
            this.txtInput.Text = "";
            this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // txtOutput
            // 
            this.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOutput.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.Location = new System.Drawing.Point(7, 336);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(561, 140);
            this.txtOutput.TabIndex = 4;
            this.txtOutput.Text = "";
            // 
            // btnShowClipboard
            // 
            this.btnShowClipboard.Location = new System.Drawing.Point(242, 307);
            this.btnShowClipboard.Name = "btnShowClipboard";
            this.btnShowClipboard.Size = new System.Drawing.Size(75, 23);
            this.btnShowClipboard.TabIndex = 5;
            this.btnShowClipboard.Text = "Show Clipboard";
            this.btnShowClipboard.UseVisualStyleBackColor = true;
            this.btnShowClipboard.Visible = false;
            this.btnShowClipboard.Click += new System.EventHandler(this.btnShowClipboard_Click);
            // 
            // clipboardTimer
            // 
            this.clipboardTimer.Enabled = true;
            this.clipboardTimer.Interval = 2000;
            this.clipboardTimer.Tick += new System.EventHandler(this.clipboardTimer_Tick);
            // 
            // btnSwitch
            // 
            this.btnSwitch.Location = new System.Drawing.Point(91, 307);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(145, 23);
            this.btnSwitch.TabIndex = 6;
            this.btnSwitch.Text = "Not Monitoring Clipboard";
            this.btnSwitch.UseVisualStyleBackColor = true;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // OxfordExtractor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 488);
            this.Controls.Add(this.btnSwitch);
            this.Controls.Add(this.btnShowClipboard);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.btnOK);
            this.KeyPreview = true;
            this.Name = "OxfordExtractor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "OxfordExtractor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.RichTextBox txtInput;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.Button btnShowClipboard;
        private System.Windows.Forms.Timer clipboardTimer;
        private System.Windows.Forms.Button btnSwitch;
    }
}