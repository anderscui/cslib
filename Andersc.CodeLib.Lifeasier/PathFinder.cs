using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Lifeasier
{
    public partial class PathFinder : Form
    {
        public PathFinder()
        {
            InitializeComponent();

            InitControls();
        }

        private void InitControls()
        {
            StringBuilder sb = new StringBuilder();
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall", false))
            {
                if (key != null)
                {
                    foreach (string keyName in key.GetSubKeyNames())
                    {
                        using (RegistryKey key2 = key.OpenSubKey(keyName, false))
                        {
                            if (key2 != null)
                            {
                                string softwareName = key2.GetValue("DisplayName", "").ToString();
                                string installLocation = key2.GetValue("UninstallString", "").ToString();
                                if (!string.IsNullOrEmpty(installLocation))
                                {
                                    sb.AppendLine(string.Format("软件名：{0} -- 安装路径：{1}\r\n", softwareName, installLocation));
                                }
                            }
                        }
                    }
                }
            }

            // 显示图标；
            textBox1.Text = sb.ToString();
        }
    }
}
