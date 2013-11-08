using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.WinFormsSamples.Components
{
    internal static class ConfigManager
    {
        private static readonly string AppSettingOfAppName = "AppTitle";

        public static string AppName
        {
            get { return WinConfigManager.GetAppSetting(AppSettingOfAppName); }
        }
    }
}
