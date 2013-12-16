using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

using Andersc.CodeLib.Common;

namespace Andersc.AnyRun.Config
{
    public static class ConfigManager
    {
        public static string ConnString
        {
            get { return "Data Source={0}".FormatWith(DataSource); }
        }

        public static string DataSource
        {
            get { return ConfigurationManager.AppSettings["DataSource"]; }
        }
    }
}
