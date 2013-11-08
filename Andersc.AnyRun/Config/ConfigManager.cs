using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Andersc.AnyRun.Config
{
    public static class ConfigManager
    {
        public static string ConnString
        {
            get { return string.Format("Data Source={0}", DataSource); }
        }

        public static string DataSource
        {
            get { return ConfigurationManager.AppSettings["DataSource"]; }
        }
    }
}
