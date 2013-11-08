using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Andersc.CodeLib.WinFormsSamples.Components
{
    public class VssInfoSection : ConfigurationSection
    {
        //private static readonly string PropertyNodeNameOfDbPath = "dbPath";
        //private static readonly string PropertyNodeNameOfUserName = "userName";
        //private static readonly string PropertyNodeNameOfPassword = "password";

        [ConfigurationProperty("dbPath", IsRequired = true)]
        public string DbPath
        {
            get { return this["dbPath"] as string; }
            set { this["dbPath"] = value; }
        }

        [ConfigurationProperty("userName", IsRequired = true)]
        public string UserName
        {
            get { return this["userName"] as string; }
            set { this["userName"] = value; }
        }

        [ConfigurationProperty("password", DefaultValue = "", IsRequired = true)]
        public string Password
        {
            get { return this["password"] as string; }
            set { this["password"] = value; }
        }
    }
}
