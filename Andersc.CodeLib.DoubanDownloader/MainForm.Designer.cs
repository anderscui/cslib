namespace Andersc.CodeLib.DoubanDownloader
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnAlbum = new System.Windows.Forms.Button();
            this.btnCelebrity = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAlbum
            // 
            this.btnAlbum.Location = new System.Drawing.Point(28, 23);
            this.btnAlbum.Name = "btnAlbum";
            this.btnAlbum.Size = new System.Drawing.Size(75, 23);
            this.btnAlbum.TabIndex = 0;
            this.btnAlbum.Text = "Album";
            this.btnAlbum.UseVisualStyleBackColor = true;
            this.btnAlbum.Click += new System.EventHandler(this.btnAlbum_Click);
            // 
            // btnCelebrity
            // 
            this.btnCelebrity.Location = new System.Drawing.Point(28, 64);
            this.btnCelebrity.Name = "btnCelebrity";
            this.btnCelebrity.Size = new System.Drawing.Size(75, 23);
            this.btnCelebrity.TabIndex = 1;
            this.btnCelebrity.Text = "Celebrity";
            this.btnCelebrity.UseVisualStyleBackColor = true;
            this.btnCelebrity.Click += new System.EventHandler(this.btnCelebrity_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 369);
            this.Controls.Add(this.btnCelebrity);
            this.Controls.Add(this.btnAlbum);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Douban Downloader";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAlbum;
        private System.Windows.Forms.Button btnCelebrity;
    }
}