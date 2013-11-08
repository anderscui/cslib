#region Comments

// 功能：WinForm和WebForm的通用配置管理类，仅能在本程序集内使用。
// 作者：Anders Cui
// 日期：2007-03-13

// 最近修改：Anders Cui
// 最近修改日期：2007-04-01
// 修改内容：添加完整注释。
// TODO：

#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Andersc.CodeLib.Common
{
    /// <summary>
    /// WinForm和WebForm的通用配置管理类，仅能在本程序集内使用。
    /// </summary>
    internal class ConfigManager
    {
        ///// <summary>
        ///// 根据指定键值判断appSettings配置节中一个配置项是否存在。
        ///// </summary>
        ///// <param name="key">要查找的appSettings配置项的键值。</param>
        ///// <returns>如果该项存在，返回true，否则返回false。</returns>
        //public static bool IsAppSettingExisted(string key)
        //{
        //    if (key == null)
        //    {
        //        throw new ArgumentNullException("key", "配置项的键值不可以为null。");
        //    }

        //    bool exists = false;
        //    string[] keys = ConfigurationManager.AppSettings.;
        //    for (int i = 0; i < keys.Length; i++)
        //    {
        //        if (keys[i] == key)
        //        {
        //            exists = true;
        //            break;
        //        }
        //    }

        //    return exists;
        //}

        /// <summary>
        /// 获取appSettings配置节中指定配置项的配置信息。
        /// </summary>
        /// <param name="key">要查找的appSettings配置项的键值</param>
        /// <returns>如果可以指定的项存在，返回指定配置项的配置信息；如果不存在（包括key值为null），返回null</returns>
        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 获取具有指定名称的连接字符串配置项的值（connectionString属性）。
        /// </summary>
        /// <param name="name">连接字符串配置项的名称。</param>
        /// <returns></returns>
        public static string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

    }
}
