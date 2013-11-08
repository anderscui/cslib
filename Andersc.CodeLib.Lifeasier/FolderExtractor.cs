using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Andersc.CodeLib.Lifeasier
{
    public partial class FolderExtractor : Form
    {
        #region Properties

        private string FileFilter
        {
            get
            {
                return this.txtFilter.Text.Trim();
            }
        }

        private bool OverWrite
        {
            get
            {
                return this.ckbOverwrite.Checked;
            }
        }

        private bool UseFileFilter
        {
            get
            {
                return (this.ckbWithFiles.Checked && (this.txtFilter.Text.Trim().Length > 0));
            }
        }

        private string TargetFolder { get; set; }

        #endregion

        public FolderExtractor()
        {
            InitializeComponent();
        }

        private void FolderExtractor_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void InitControls()
        {
            ckbWithFiles.Checked = false;
            txtFilter.ReadOnly = true;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            RequestToSelectSourceFolder();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtFolder.Text))
            {
                MessageBox.Show("Please select source folder.");
                this.RequestToSelectSourceFolder();
                return;
            }

            this.TargetFolder = this.RequestToSelectTargetFolder();
            if (string.IsNullOrEmpty(this.TargetFolder))
            {
                MessageBox.Show("Please select target folder.");
            }
            else
            {
                this.txtMessage.Clear();
                this.txtMessage.Text = this.txtMessage.Text + string.Format("Source Folder: {0}", this.txtFolder.Text) + Environment.NewLine;
                this.txtMessage.Text = this.txtMessage.Text + string.Format("Target Folder: {0}", this.TargetFolder);
                try
                {
                    DirectoryInfo sourceDir = new DirectoryInfo(this.txtFolder.Text);
                    DirectoryInfo targetDir = new DirectoryInfo(this.TargetFolder).CreateSubdirectory(sourceDir.Name);
                    this.txtMessage.Text = this.txtMessage.Text + Environment.NewLine + string.Format("Created root folder: {0}, then the sub dirs----", sourceDir.Name);
                    this.CopyDirectory(sourceDir, targetDir, 0);
                }
                catch (IOException exception)
                {
                    MessageBox.Show("File Operation failed: " + exception.Message);
                }
                catch (Exception exception2)
                {
                    MessageBox.Show("Operation failed: " + exception2.Message);
                }
            }

        }

        private void ckbWithFiles_CheckedChanged(object sender, EventArgs e)
        {
            txtFilter.ReadOnly = !ckbWithFiles.Checked;
        }

        private void CopyDirectory(DirectoryInfo sourceDir, DirectoryInfo targetDir, int depth)
        {
            int num = depth + 1;
            string str = new string(' ', num * 2);

            if (this.UseFileFilter)
            {
                FileInfo[] files = sourceDir.GetFiles(this.FileFilter, SearchOption.TopDirectoryOnly);
                foreach (FileInfo info in files)
                {
                    try
                    {
                        string destFileName = Path.Combine(targetDir.FullName, info.Name);
                        info.CopyTo(destFileName, this.OverWrite);
                        this.txtMessage.Text = this.txtMessage.Text + Environment.NewLine + string.Format("{0}--{1}", str, info.Name);
                    }
                    catch (Exception ex)
                    {
                        this.txtMessage.Text =
                            this.txtMessage.Text + Environment.NewLine
                            + string.Format("Error occured when copy {0}, message: {1}", info.FullName, ex.Message);
                    }
                }
            }

            DirectoryInfo[] directories = sourceDir.GetDirectories();
            foreach (DirectoryInfo info2 in directories)
            {
                try
                {
                    DirectoryInfo info3 = targetDir.CreateSubdirectory(info2.Name);
                    this.txtMessage.Text = this.txtMessage.Text + Environment.NewLine + string.Format("{0}--{1}", str, info3.Name);
                    this.CopyDirectory(info2, info3, num);
                }
                catch (Exception ex)
                {
                    this.txtMessage.Text =
                        this.txtMessage.Text + Environment.NewLine
                        + string.Format("Error occured when copy {0}, message: {1}", info2.FullName, ex.Message);
                }
            }
        }

        private void RequestToSelectSourceFolder()
        {
            this.dlgOpenFolder.RootFolder = Environment.SpecialFolder.Desktop;
            if (this.dlgOpenFolder.ShowDialog() == DialogResult.OK)
            {
                this.txtFolder.Text = this.dlgOpenFolder.SelectedPath;
            }
        }

        private string RequestToSelectTargetFolder()
        {
            string selectedPath = string.Empty;
            this.dlgOpenFolder.RootFolder = Environment.SpecialFolder.Desktop;
            if (this.dlgOpenFolder.ShowDialog() == DialogResult.OK)
            {
                selectedPath = this.dlgOpenFolder.SelectedPath;
            }
            return selectedPath;
        }
    }
}
