namespace Andersc.CodeLib.Lifeasier.Dev
{
    partial class TestDatabaseConnectivity
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
            this.btnTest = new System.Windows.Forms.Button();
            this.txtConnString = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(12, 85);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtConnString
            // 
            this.txtConnString.Location = new System.Drawing.Point(12, 12);
            this.txtConnString.Multiline = true;
            this.txtConnString.Name = "txtConnString";
            this.txtConnString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConnString.Size = new System.Drawing.Size(419, 54);
            this.txtConnString.TabIndex = 1;
            // 
            // TestDatabaseConnectivity
            // 
            this.ClientSize = new System.Drawing.Size(443, 165);
            this.Controls.Add(this.txtConnString);
            this.Controls.Add(this.btnTest);
            this.KeyPreview = true;
            this.Name = "TestDatabaseConnectivity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TextBox txtConnString;
    }
}
