namespace Andersc.CodeLib.WinFormsSamples.StandardControls
{
    partial class DragDropForm
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
            this.ltbLeft = new System.Windows.Forms.ListBox();
            this.ltbRight = new System.Windows.Forms.ListBox();
            this.gpbMultiple = new System.Windows.Forms.GroupBox();
            this.txtDrop = new System.Windows.Forms.TextBox();
            this.btnDrag = new System.Windows.Forms.Button();
            this.gpbMultiple.SuspendLayout();
            this.SuspendLayout();
            // 
            // ltbLeft
            // 
            this.ltbLeft.AllowDrop = true;
            this.ltbLeft.FormattingEnabled = true;
            this.ltbLeft.ItemHeight = 12;
            this.ltbLeft.Location = new System.Drawing.Point(30, 30);
            this.ltbLeft.Name = "ltbLeft";
            this.ltbLeft.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ltbLeft.Size = new System.Drawing.Size(152, 124);
            this.ltbLeft.TabIndex = 0;
            this.ltbLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ltbLeft_MouseDown);
            // 
            // ltbRight
            // 
            this.ltbRight.AllowDrop = true;
            this.ltbRight.FormattingEnabled = true;
            this.ltbRight.ItemHeight = 12;
            this.ltbRight.Location = new System.Drawing.Point(223, 30);
            this.ltbRight.Name = "ltbRight";
            this.ltbRight.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ltbRight.Size = new System.Drawing.Size(152, 124);
            this.ltbRight.TabIndex = 1;
            this.ltbRight.DragDrop += new System.Windows.Forms.DragEventHandler(this.ltbRight_DragDrop);
            this.ltbRight.DragEnter += new System.Windows.Forms.DragEventHandler(this.ltbRight_DragEnter);
            // 
            // gpbMultiple
            // 
            this.gpbMultiple.Controls.Add(this.btnDrag);
            this.gpbMultiple.Controls.Add(this.txtDrop);
            this.gpbMultiple.Location = new System.Drawing.Point(30, 173);
            this.gpbMultiple.Name = "gpbMultiple";
            this.gpbMultiple.Size = new System.Drawing.Size(345, 131);
            this.gpbMultiple.TabIndex = 2;
            this.gpbMultiple.TabStop = false;
            this.gpbMultiple.Text = "Multiple Effects";
            // 
            // txtDrop
            // 
            this.txtDrop.AllowDrop = true;
            this.txtDrop.Location = new System.Drawing.Point(32, 30);
            this.txtDrop.Name = "txtDrop";
            this.txtDrop.Size = new System.Drawing.Size(292, 21);
            this.txtDrop.TabIndex = 0;
            this.txtDrop.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtDrop_DragDrop);
            this.txtDrop.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtDrop_DragEnter);
            this.txtDrop.DragOver += new System.Windows.Forms.DragEventHandler(this.txtDrop_DragOver);
            // 
            // btnDrag
            // 
            this.btnDrag.Location = new System.Drawing.Point(32, 70);
            this.btnDrag.Name = "btnDrag";
            this.btnDrag.Size = new System.Drawing.Size(75, 23);
            this.btnDrag.TabIndex = 1;
            this.btnDrag.Text = "Drag Me";
            this.btnDrag.UseVisualStyleBackColor = true;
            this.btnDrag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDrag_MouseDown);
            // 
            // DragDropForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 325);
            this.Controls.Add(this.gpbMultiple);
            this.Controls.Add(this.ltbRight);
            this.Controls.Add(this.ltbLeft);
            this.Name = "DragDropForm";
            this.Text = "DragDropForm";
            this.Load += new System.EventHandler(this.DragDropForm_Load);
            this.gpbMultiple.ResumeLayout(false);
            this.gpbMultiple.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox ltbLeft;
        private System.Windows.Forms.ListBox ltbRight;
        private System.Windows.Forms.GroupBox gpbMultiple;
        private System.Windows.Forms.Button btnDrag;
        private System.Windows.Forms.TextBox txtDrop;
    }
}