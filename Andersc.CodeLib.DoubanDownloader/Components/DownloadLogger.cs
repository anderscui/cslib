using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Andersc.CodeLib.DoubanDownloader.Components
{
    internal class DownloadLogger
    {
        private static readonly string fileName = "downloaded.log";

        private static string LogFilePath
        {
            get { return Path.Combine(Application.StartupPath, fileName); }
        }

        internal static void AddLog(int id, int count)
        {
            using (FileStream fs = new FileStream(LogFilePath, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(string.Format("albumID:{0};photoCount:{1};datetime:{2};", id, count, DateTime.Now));
                }
            }
        }
    }
}
