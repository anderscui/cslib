using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Lifeasier
{
    public partial class SystemDriveInfo : Form
    {
        DriveInfo[] drives;

        public SystemDriveInfo()
        {
            InitializeComponent();
        }

        private void SystemDriveInfo_Load(object sender, EventArgs e)
        {
            drives = DriveInfo.GetDrives();

            cboDrives.DisplayMember = "Name";
            cboDrives.ValueMember = "VolumeLabel";
            cboDrives.DataSource = drives;
        }

        private void cboDrives_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDrives.SelectedIndex < 0)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            DriveInfo di = cboDrives.SelectedItem as DriveInfo;

            sb.AppendLine(di.Name + ": ");
            if (di.IsNotNull())
            {
                if (di.IsReady)
                {
                    sb.AppendLine("VolumeLabel: " + di.VolumeLabel);
                    sb.AppendLine("RootDirectory: " + di.RootDirectory.FullName);
                    sb.AppendLine("DriveFormat: " + di.DriveFormat);
                    sb.AppendLine("DriveType: " + di.DriveType.ToString());
                    sb.AppendLine("AvailableFreeSpace: " + di.AvailableFreeSpace);
                    sb.AppendLine("TotalFreeSpace: " + di.TotalFreeSpace);
                    sb.AppendLine("TotalSize: " + di.TotalSize);
                }
                else
                {
                    sb.AppendLine("Drive Not Ready.");
                }
            }

            txtDetails.Text = sb.ToString();
        }
    }
}
