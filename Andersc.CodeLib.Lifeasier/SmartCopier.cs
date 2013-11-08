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
    public partial class SmartCopier : Form
    {
        private const string COMMON_PATH_TEXT = "Common Path:";
        private const string PROMPT_WORK_COMPLETE = "Work complete.";
        private readonly char SEPARATOR = Path.DirectorySeparatorChar;

        private List<string> fileList = null;

        private string commonPath = null;
        private string CommonPath
        {
            get { return commonPath; }
            set
            {
                commonPath = value;
                lblCommonPath.Text = COMMON_PATH_TEXT + commonPath;
            }
        }

        public SmartCopier()
        {
            InitializeComponent();

            InitForm();
        }

        private void InitForm()
        {
            fileList = new List<string>();
            CommonPath = string.Empty;

            btnAddFile.Select();
            btnRemoveFile.Enabled = false;
            SetFileCountLabel();
        }

        private void BindFiles()
        {
            fileListBox.Items.Clear();
            fileList.Sort();
            fileList.ForEach(file => fileListBox.Items.Add(file));
            //fileListBox.Items.AddRange(fileList.ToArray());

            SetRemoveButtonStatus();
            FindCommonPath();
            SetFileCountLabel();
        }

        private void SetFileCountLabel()
        {
            lblFileCount.Text = fileListBox.Items.Count.ToString();
        }

        private void SetRemoveButtonStatus()
        {
            btnRemoveFile.Enabled =
                fileList.Count > 0;
        }

        private void FindCommonPath()
        {
            CommonPath = string.Empty;

            if (fileListBox.Items.Count < 2)
            {
                return;
            }

            string commonString = fileListBox.Items[0] as string;
            for (int i = 1; i < fileListBox.Items.Count; i++)
            {
                var path = fileListBox.Items[i] as string;
                //var path2 = fileListBox.Items[i + 1] as string;
                commonString = GetCommonPath(path, commonString);

                if (string.IsNullOrEmpty(commonString))
                {
                    break;
                }
            }

            if (string.IsNullOrEmpty(commonString))
            {
                CommonPath = commonString;
            }
            else
            {
                int index = commonString.LastIndexOf(SEPARATOR);
                if (index > 0)
                {
                    CommonPath = commonString.Substring(0, index + 1);
                }
                else
                {
                    CommonPath = string.Empty;
                }
            }
        }

        private string GetCommonPath(string path1, string path2)
        {
            List<char> commonChars = new List<char>();

            for (int i = 0; i < Math.Min(path1.Length, path2.Length); i++)
            {
                if (path1[i] == path2[i])
                {
                    commonChars.Add(path1[i]);
                }
                else
                {
                    break;
                }
            }

            return new string(commonChars.ToArray());
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            DialogResult dr = dlgOpenFile.ShowDialog();
            if (dr == DialogResult.OK)
            {
                foreach (string addingFile in dlgOpenFile.FileNames)
                {
                    if (!fileList.Contains(addingFile))
                    {
                        fileList.Add(addingFile);
                        BindFiles();
                    }
                }
            }
        }

        private void btnRemoveFile_Click(object sender, EventArgs e)
        {
            if (fileListBox.SelectedIndex >= 0)
            {
                // Remove all the selected items.
                foreach (var item in fileListBox.SelectedItems)
                {
                    fileList.Remove(item as string);
                }

                // Rebind files.
                BindFiles();
            }
        }

        private void btnCopyFile_Click(object sender, EventArgs e)
        {
            if (fileListBox.Items.Count == 0)
            {
                MessageBox.Show("Please add at least one file.");
                return;
            }

            DialogResult dr = dlgOpenFolder.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string folder = dlgOpenFolder.SelectedPath;

                if (string.IsNullOrEmpty(CommonPath) || !chkReserveRelativePath.Checked)
                {
                    CopyFilesDirectly(folder);
                }
                else
                {
                    CopyFilesReservingRelativePath(folder);
                }

                MessageBox.Show(PROMPT_WORK_COMPLETE);
            }
        }

        private void CopyFilesDirectly(string targetFolder)
        {
            try
            {
                var fileNames = fileListBox.Items.Cast<string>();
                foreach (var fileName in fileNames)
                {
                    File.Copy(fileName, Path.Combine(targetFolder, Path.GetFileName(fileName)), true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured: " + ex.Message);
            }
        }

        private void CopyFilesReservingRelativePath(string targetFolder)
        {
            try
            {
                var fileNames = fileListBox.Items.Cast<string>();
                DirectoryInfo di = new DirectoryInfo(targetFolder);

                foreach (var filePath in fileNames)
                {
                    string relativePath = filePath.Remove(0, CommonPath.Length);
                    string relDir = string.Empty;
                    string fileName = Path.GetFileName(filePath);
                    int index = relativePath.LastIndexOf(SEPARATOR);
                    if (index > 0)
                    {
                        // Not in the common folder, consider the relative path.
                        relDir = relativePath.Substring(0, index);
                        di.CreateSubdirectory(relDir);
                        File.Copy(filePath, Path.Combine(targetFolder, relativePath), true);
                    }
                    else
                    {
                        // In the common folder, just copy it.
                        File.Copy(filePath, Path.Combine(targetFolder, fileName), true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured: " + ex.Message);
            }
        }
    }
}
