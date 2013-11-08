using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Andersc.CodeLib.WinFormsSamples.Components
{
    // TODO: Common caption; 
    public static class Mbox
    {
        public static DialogResult ShowInfo(string info)
        {
            return MessageBox.Show(info, ConfigManager.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult ShowWarning(string warning)
        {
            return MessageBox.Show(warning, ConfigManager.AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult ShowError(string error)
        {
            return MessageBox.Show(error, ConfigManager.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
