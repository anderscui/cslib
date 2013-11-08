using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

using Andersc.CodeLib.DoubanDownloader.Properties;
using Andersc.CodeLib.DoubanDownloader.Components;
using System.Diagnostics;

namespace Andersc.CodeLib.DoubanDownloader
{
    public partial class AlbumForm : Form
    {
        private class DownloadResult
        {
            public int PhotoCount { get; set; }
            public string LocalDir { get; set; }
        }

        private static readonly int MaxPageIndex = 100;

        private string albumUrl = "http://www.douban.com/photos/album/";
        private string albumUrlFormat = "http://www.douban.com/photos/album/{0}/?start={1}";
        private string thumbRegex = "<a(.+?)title=\"(?<title>(.|\\n)*?)\">((.|\\n)+?)<img src=\"(?<thumbSrc>.+)\" />";
        private string albumNameRegex = "<h(\\d+)>\\s*(?<albumName>(.|\\n)+?)\\s*</h\\1>";
        private string albumDescRegex = "<div.*style=\".*bottom.*\">(?<albumDesc>(.|\\n)*?)</div>";

        public AlbumForm()
        {
            InitializeComponent();

            InitControls();
        }

        private void InitControls()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            txtAlbumID.Select();
            txtAlbumID.Focus();
            txtTips.Text = ConfigManager.Instance.AlbumDownloadTips;
            txtLocalAlbumDir.Text = string.Empty;
            txtLocalAlbumDir.Visible = false;
            lblDownloadInfo.Text = string.Empty;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if ((txtAlbumID.Text.Trim().Length == 0)
                    || (!Regex.IsMatch(txtAlbumID.Text.Trim(), "^\\d+$")))
            {
                MessageBox.Show("Invalid input format, i need a number...");
                return;
            }

            dlgBrowse.ShowNewFolderButton = true;
            dlgBrowse.SelectedPath = ConfigManager.Instance.LastSelectedPath;

            if (dlgBrowse.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            SynchronizationContext uiContext = SynchronizationContext.Current;

            Thread taskThread = new Thread(DownloadTask);
            taskThread.Name = "Download Image";
            taskThread.Start(uiContext);

            btnDownload.Enabled = false;
            lblDownloadInfo.Text = "Downloading starts...";
        }

        private void DownloadTask(object state)
        {
            SynchronizationContext uiContext = state as SynchronizationContext;

            try
            {
                string rootDir = dlgBrowse.SelectedPath;
                ConfigManager.Instance.LastSelectedPath = rootDir;
                int id = Convert.ToInt32(txtAlbumID.Text);

                // TODO: Check folder.
                string localAlbumDir = Path.Combine(rootDir, id.ToString());
                if (!Directory.Exists(localAlbumDir))
                {
                    string[] possibleDirs = Directory.GetDirectories(rootDir, id + "?*");
                    if (possibleDirs.Length > 0)
                    {
                        DialogResult dr = 
                            MessageBox.Show(
                                String.Format("存在一个文件夹：{0}，要使用它吗？", possibleDirs[0]), 
                                "注意", 
                                MessageBoxButtons.YesNo);
                        if (dr == DialogResult.Yes)
                        {
                            localAlbumDir = possibleDirs[0];
                        }
                        else
                        {
                            Directory.CreateDirectory(localAlbumDir);
                        }
                    }
                    Directory.CreateDirectory(localAlbumDir);
                }

                string infoFilePath = Path.Combine(localAlbumDir, "info.txt");
                int photoCount = 0;
                using (StreamWriter sw = new StreamWriter(infoFilePath, false))
                {
                    // TODO: 
                    int pageItemCount = 18;
                    int pageIndex = 0;
                    int startIndex = 0;
                    bool isFirstPage = true;
                    do
                    {
                        startIndex = pageIndex * pageItemCount;

                        string pageContent = GetResponseString(string.Format(albumUrlFormat, id, startIndex));
                        File.WriteAllText(DateTime.Now.ToFileTime().ToString(), pageContent);
                        if (!string.IsNullOrEmpty(pageContent))
                        {
                            if (isFirstPage)
                            {
                                WriteAlbumHeader(id, sw, pageContent);
                                isFirstPage = false;
                            }

                            // Write photo info.
                            // TODO:
                            MatchCollection thumbs = Regex.Matches(pageContent, thumbRegex);
                            if (thumbs.Count == 0 || pageIndex >= MaxPageIndex)
                            {
                                break;
                            }

                            foreach (Match thumb in thumbs)
                            {
                                string title = thumb.Groups["title"].Value;
                                string thumbSrc = thumb.Groups["thumbSrc"].Value;
                                string fileName = Path.GetFileName(thumbSrc);
                                string localFullName = Path.Combine(localAlbumDir, fileName);

                                sw.WriteLine(fileName + ": ");
                                sw.WriteLine("description: " + title);
                                sw.WriteLine(new string('*', 50));
                                sw.WriteLine();

                                if (!File.Exists(localFullName))
                                {
                                    DoanloadFile(thumbSrc.Replace("view/photo/thumb", "view/photo/photo"), localFullName);
                                    photoCount++;

                                    uiContext.Post(HasDownloadANewFile, photoCount);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("下载过程中出现问题:(");
                            break;
                        }

                        pageIndex++;
                    } while (true);
                }

                DownloadLogger.AddLog(id, photoCount);
                uiContext.Post(DownloadCompleted, new DownloadResult { PhotoCount = photoCount, LocalDir = localAlbumDir });
            }
            catch (Exception ex)
            {
                uiContext.Post(ErrorOccured, ex);
            }
        }

        private void HasDownloadANewFile(object state)
        {
            int photoCount = Convert.ToInt32(state);
            lblDownloadInfo.Text = String.Format("Has downloaded {0} photos...", photoCount);
        }

        private void DownloadCompleted(object state)
        {
            DownloadResult dr = state as DownloadResult;
            if (dr.PhotoCount > 0)
            {
                lblDownloadInfo.Text = string.Format("Job done, {0} photos were downloaded to the below folder", dr.PhotoCount);
                txtLocalAlbumDir.Text = dr.LocalDir;
                txtLocalAlbumDir.Visible = true;
            }
            else
            {
                lblDownloadInfo.Text = "Job done, no new photos were downloaded";
            }
            
            btnDownload.Enabled = true;
        }

        private void ErrorOccured(object state)
        {
            Exception ex = state as Exception;
            string msg = "未能完成下载，原因可能是：" + Environment.NewLine
                    + ex.Message + Environment.NewLine + Environment.NewLine
                    + "可以将该信息豆邮给作者";

            MessageBox.Show(msg, "下载失败");

            lblDownloadInfo.Text = "Please try it again:-)";
            btnDownload.Enabled = true;
        }

        private void WriteAlbumHeader(int albumID, StreamWriter sw, string pageContent)
        {
            Match albumNameMatch = Regex.Match(pageContent, albumNameRegex);
            if (albumNameMatch != null)
            {
                sw.WriteLine("album: " + albumNameMatch.Groups["albumName"]);
            }

            sw.WriteLine("url: " + albumUrl + albumID.ToString());

            Match albumDescMatch = Regex.Match(pageContent, albumDescRegex);
            if (albumDescMatch != null)
            {
                sw.WriteLine("description: " + albumDescMatch.Groups["albumDesc"]);

            }
            sw.WriteLine();
        }

        private void DoanloadFile(string url, string localFileName)
        {
            WebClient wc = new WebClient();
            byte[] res = wc.DownloadData(url);
            File.WriteAllBytes(localFileName, res);
        }

        private string GetResponseString(string url)
        {
            WebClient wc = new WebClient();
            byte[] res = wc.DownloadData(url);
            return Encoding.UTF8.GetString(res);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigManager.Instance.Save();
        }

        private void lnkOpen_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var dir = txtLocalAlbumDir.Text;
            if (!string.IsNullOrWhiteSpace(dir) && Directory.Exists(dir))
            {
                Process.Start(dir);
            }
        }
    }
}
