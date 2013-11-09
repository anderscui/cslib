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
using Andersc.CodeLib.Lifeasier.Components;

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
            using (var key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall", false))
            {
                if (key != null)
                {
                    var sb = new StringBuilder();
                    var apps = new List<AppInfo>();
                    foreach (string keyName in key.GetSubKeyNames())
                    {
                        using (var key2 = key.OpenSubKey(keyName, false))
                        {
                            if (key2 != null)
                            {
                                var name = key2.GetValue("DisplayName", "").ToString();
                                var installPath = key2.GetValue("InstallLocation", "").ToString();
                                var publisher = key2.GetValue("Publisher", "").ToString();
                                var version = key2.GetValue("DisplayVersion", "").ToString();
                                if (name.IsNotNullOrEmpty())
                                {
                                    apps.Add(new AppInfo()
                                    {
                                        Name = name, 
                                        Version = version, 
                                        Publisher = publisher,
                                        InstallPath = installPath
                                    });
                                }
                            }
                        }
                    }
                    apps.OrderBy(app => app.Name.ToLower()).ForEach(app => 
                        sb.AppendLine(string.Format("{0} {1} - {2} \r\n    {3}\r\n",
                                        app.Name, app.Version, app.Publisher, app.InstallPath)));
                    txtApps.Text = sb.ToString();
                }
            }
        }
    }
}
