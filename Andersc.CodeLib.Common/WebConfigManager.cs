#region Comments

// 功能：ASP.NET应用程序的配置文件的管理类。
// 作者：裴登海
// 日期：2007-03-13

// 最近修改：Anders Cui
// 最近修改日期：2007-03-31

#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web.Configuration;

namespace Andersc.CodeLib.Common
{
    /// <summary>
    /// ASP.NET应用程序的配置文件的管理类。
    /// </summary>
    public static class WebConfigManager
    {
        private static string AppSettingsSectionName = "appSettings";
        private static string ConnectionStringSectionName = "connectionStrings";

        // TODO: 添加对指定配置文件（非默认配置文件）的操作。

        #region appSettings的操作

        /// <summary>
        /// 根据指定键值判断appSettings配置节中一个配置项是否存在。
        /// </summary>
        /// <param name="key">要查找的appSettings配置项的键值</param>
        /// <returns>如果该项存在，返回true，否则返回false。</returns>
        public static bool IsAppSettingExisted(string key)
        {
            bool exists = false;

            string[] keys = WebConfigurationManager.AppSettings.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i] == key)
                {
                    exists = true;
                    break;
                }
            }

            return exists;
        }

        /// <summary>
        /// Gets config info of specified appSettings key.
        /// </summary>
        /// <param name="key">The specified appSettings key</param>
        /// <returns>If the config item exists, return its value; otherwise, null.</returns>
        public static string GetAppSetting(string key)
        {
            return WebConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 修改appSettings配置节中指定配置项的配置信息。
        /// </summary>
        /// <param name="key">要修改的appSettings配置项的键值</param>
        /// <param name="value">要修改的appSettings配置项的新值</param>
        public static void ModifyAppSetting(string key, string value)
        {
            ModifyAppSetting(ConfigurationSaveMode.Modified, key, value);
        }

        /// <summary>
        /// 修改appSettings配置节中指定配置项的配置信息。
        /// </summary>
        /// <param name="mode">修改配置文件的模式。</param>
        /// <param name="key">要修改的appSettings配置项的键值</param>
        /// <param name="value">要修改的appSettings配置项的新值</param>
        public static void ModifyAppSetting(ConfigurationSaveMode mode, string key, string value)
        {
            if (!IsAppSettingExisted(key))
            {
                // TODO：使用资源文件
                throw new InvalidOperationException(string.Format("键值为{0}的配置项不存在。", key));
            }

            Configuration config = GetDefaultConfiguration();
            config.AppSettings.Settings[key].Value = value;
            config.Save(mode);
            ConfigurationManager.RefreshSection(AppSettingsSectionName);
        }

        /// <summary>
        /// 删除appSettings配置节中指定的配置项。
        /// </summary>
        /// <param name="key">要删除的配置项的键值</param>
        public static void DeleteAppSetting(string key)
        {
            Configuration config = GetDefaultConfiguration();
            config.AppSettings.Settings.Remove(key);
            config.Save();
            ConfigurationManager.RefreshSection(AppSettingsSectionName);
        }

        /// <summary>
        /// 删除appSettings中所有的配置信息。
        /// </summary>
        public static void ClearAppSettings()
        {
            Configuration config = GetDefaultConfiguration();
            config.AppSettings.Settings.Clear();
            config.Save();
            ConfigurationManager.RefreshSection(AppSettingsSectionName);
        }

        /// <summary>
        /// 添加一个appSettings配置项。如果相同键值的配置项已存在，则本方法将新增值追加到原来值之后。
        /// </summary>
        /// <param name="key">要添加的appSetting配置项的键值</param>
        /// <param name="value">要添加的appSetting配置项的值</param>
        public static void AddAppSetting(string key, string value)
        {
            Configuration config = GetDefaultConfiguration();
            config.AppSettings.Settings.Add(key, value);
            config.Save();
            ConfigurationManager.RefreshSection(AppSettingsSectionName);
        }

        /// <summary>
        /// 添加一个appSettings配置项。
        /// </summary>
        /// <param name="key">要添加的appSetting配置项的键值</param>
        /// <param name="value">要添加的appSetting配置项的值</param>
        /// <param name="overrideOldValue">如果为true，则覆盖已存在的配置项的值，否则追加到原有值之后。</param>
        public static void AddAppSetting(string key, string value, bool overrideOldValue)
        {
            if (overrideOldValue)
            {
                if (IsAppSettingExisted(key))
                {
                    ModifyAppSetting(key, value);
                }
            }
            else
            {
                AddAppSetting(key, value);
            }
        }

        #endregion


        #region 连接字符串配置项操作

        /// <summary>
        /// 根据指定名称判断connectionStrings配置节中一个配置项是否存在。
        /// </summary>
        /// <param name="name">要查找的connectionString配置项的名称。</param>
        /// <returns>如果该项存在，返回true，否则返回false。</returns>
        public static bool IsConnectionExisted(string name)
        {
            bool IsExisted = false;

            Configuration config = GetDefaultConfiguration();
            ConnectionStringsSection ConfigSections = config.ConnectionStrings;
            for (int i = 0; i < ConfigSections.ConnectionStrings.Count; i++)
            {
                if (ConfigSections.ConnectionStrings[i].Name == name)
                {
                    IsExisted = true;
                    break;
                }
            }

            return IsExisted;
        }

        /// <summary>
        /// 获取具有指定名称的连接字符串配置项。
        /// </summary>
        /// <param name="name">连接字符串配置项的名称。</param>
        /// <returns>如果配置项存在，返回该配置项，否则返回null</returns>
        public static ConnectionStringSettings GetConnectionSettings(string name)
        {
            ConnectionStringSettings settrings = null;

            Configuration config = GetDefaultConfiguration();
            ConnectionStringsSection ConfigSections = config.ConnectionStrings;
            for (int i = 0; i < ConfigSections.ConnectionStrings.Count; i++)
            {
                if (ConfigSections.ConnectionStrings[i].Name == name)
                {
                    settrings = ConfigSections.ConnectionStrings[i];
                    break;
                }
            }

            return settrings;
        }

        /// <summary>
        /// 获取具有指定名称的连接字符串配置项的值（connectionString属性）。
        /// </summary>
        /// <param name="name">连接字符串配置项的名称。</param>
        /// <returns></returns>
        public static string GetConnectionString(string name)
        {
            string connString = null;

            Configuration config = GetDefaultConfiguration();
            ConnectionStringSettings settings = GetConnectionSettings(name);
            if (settings != null)
            {
                connString = settings.ConnectionString;
            }

            return connString;
        }

        /// <summary>
        /// 获取具有指定名称的连接字符串配置项的providerName。
        /// </summary>
        /// <param name="name">连接字符串配置项的名称。</param>
        /// <returns></returns>
        public static string GetConnectionProvider(string name)
        {
            string providerName = null;

            Configuration config = GetDefaultConfiguration();
            ConnectionStringSettings settings = GetConnectionSettings(name);
            if (settings != null)
            {
                providerName = settings.ProviderName;
            }

            return providerName;
        }

        /// <summary>
        /// 修改ConnectionString信息的名称
        /// </summary>
        /// <param name="originalName">ConnectionString的原有名称</param>
        /// <param name="newName">ConnectionString的新名称</param>
        /// <returns></returns>
        public static void ModifyConnectionName(string originalName, string newName)
        {
            Configuration config = GetDefaultConfiguration();

            bool existed = IsConnectionExisted(originalName);
            if (existed)
            {
                config.ConnectionStrings.ConnectionStrings[originalName].Name = newName;
            }
            else
            {
                throw new InvalidOperationException("要修改的名称为" + originalName + "的连接字符串不存在。");
            }

            config.Save(ConfigurationSaveMode.Modified);
            RefreshConfigurationSection(ConnectionStringSectionName);
        }

        /// <summary>
        /// 修改ConnectionString信息的Provider
        /// </summary>
        /// <param name="name">ConnectionString的名称</param>
        /// <param name="newProvider">ConnectionString的新Povider</param>
        /// <returns></returns>
        public static void ModifyConnectionPovider(string name, string newProvider)
        {
            Configuration config = GetDefaultConfiguration();

            bool existed = IsConnectionExisted(name);
            if (existed)
            {
                config.ConnectionStrings.ConnectionStrings[name].ProviderName = newProvider;
            }
            else
            {
                throw new InvalidOperationException("要修改的名称为" + name + "的连接字符串不存在。");
            }

            config.Save(ConfigurationSaveMode.Modified);
            RefreshConfigurationSection(ConnectionStringSectionName);
        }

        /// <summary>
        /// 修改指定的ConnectionString值
        /// </summary>
        /// <param name="name">要修改的ConnectionString的名称</param>
        /// <param name="newConnectionString">ConnectionString的新值</param>
        /// <returns></returns>
        public static void ModifyConnectionString(string name, string newConnectionString)
        {
            Configuration config = GetDefaultConfiguration();

            bool existed = IsConnectionExisted(name);
            if (existed)
            {
                config.ConnectionStrings.ConnectionStrings[name].ConnectionString = newConnectionString;
            }
            else
            {
                throw new InvalidOperationException("要修改的名称为" + name + "的连接字符串不存在。");
            }

            config.Save(ConfigurationSaveMode.Modified);
            RefreshConfigurationSection(ConnectionStringSectionName);
        }

        /// <summary>
        /// 修改ConnectionString配置项的Provider和ConnectionString属性
        /// </summary>
        /// <param name="name">要修改ConnectionString配置项的名称</param>
        /// <param name="newPrivoder">ConnectionString配置项的新Provider值</param>
        /// <param name="newConnectionString">ConnectionString配置项的新ConnectionString值</param>
        /// <returns></returns>
        public static void ModifyProviderAndConnectionString(string name, string newPrivoder, string newConnectionString)
        {
            Configuration config = GetDefaultConfiguration();

            bool existed = IsConnectionExisted(name);
            if (existed)
            {
                config.ConnectionStrings.ConnectionStrings[name].ProviderName = newPrivoder;
                config.ConnectionStrings.ConnectionStrings[name].ConnectionString = newConnectionString;
            }
            else
            {
                throw new InvalidOperationException("要修改的名称为" + name + "的连接字符串不存在。");
            }

            config.Save(ConfigurationSaveMode.Modified);
            RefreshConfigurationSection(ConnectionStringSectionName);
        }

        /// <summary>
        /// 修改指定名称的ConnectionString配置项。
        /// </summary>
        /// <param name="name">要修改ConnectionString配置项的名称</param>
        /// <param name="section">要修改ConnectionString配置项的新值</param>
        /// <returns></returns>
        public static void ModifyConnectionStringSection(string name, ConnectionStringSettings section)
        {
            Configuration config = GetDefaultConfiguration();

            bool existed = IsConnectionExisted(name);
            if (existed)
            {
                config.ConnectionStrings.ConnectionStrings[name].Name = section.Name;
                config.ConnectionStrings.ConnectionStrings[name].ProviderName = section.ProviderName;
                config.ConnectionStrings.ConnectionStrings[name].ConnectionString = section.ConnectionString;
            }
            else
            {
                throw new InvalidOperationException("要修改的名称为" + name + "的连接字符串不存在。");
            }

            config.Save(ConfigurationSaveMode.Modified);
            RefreshConfigurationSection(ConnectionStringSectionName);
        }

        /// <summary>
        /// 删除指定名称的ConnectionString配置项。
        /// </summary>
        /// <param name="name">要删除的ConnectionString配置项的名称</param>
        public static void DeleteConnectionString(string name)
        {
            Configuration config = GetDefaultConfiguration();

            config.ConnectionStrings.ConnectionStrings.Remove(name);
            config.Save();

            RefreshConfigurationSection(ConnectionStringSectionName);
        }

        /// <summary>
        /// 删除所有ConnectionString配置节  
        /// </summary>
        public static void ClearConnectionString()
        {
            Configuration config = GetDefaultConfiguration();

            ConnectionStringsSection ConfigSections = config.ConnectionStrings;
            ConfigSections.ConnectionStrings.Clear();
            config.Save();

            RefreshConfigurationSection(ConnectionStringSectionName);
        }

        /// <summary>
        /// 添加一个ConnectionString配置项。
        /// </summary>
        /// <param name="section">要添加的ConnectionString配置项的内容。</param>
        public static void AddConnectionStringSection(ConnectionStringSettings section)
        {
            if (section == null)
            {
                throw new ArgumentNullException("section", "连接字符串配置项的值不能为null。");
            }

            string name = section.Name;
            if (IsConnectionExisted(name))
            {
                throw new InvalidCastException(string.Format("名称为{0}的连接字符串配置项已存在。", name));
            }

            Configuration config = GetDefaultConfiguration();
            config.ConnectionStrings.ConnectionStrings.Add(section);
            config.Save();

            RefreshConfigurationSection(ConnectionStringSectionName);
        }

        /// <summary>
        /// 添加一个ConnectionString配置项。
        /// </summary>
        /// <param name="name">要添加的ConnectionString配置项的名称</param>
        /// <param name="connectionString">要添加的ConnectionString配置项的连接字符串</param>
        public static void AddConnectionString(string name, string connectionString)
        {
            Configuration config = GetDefaultConfiguration();

            ConnectionStringSettings section = new ConnectionStringSettings();
            section.Name = name;
            section.ConnectionString = connectionString;

            AddConnectionStringSection(section);
        }

        /// <summary>
        /// 添加一个ConnectionString配置项。
        /// </summary>
        /// <param name="name">要添加的ConnectionString配置项的名称</param>
        /// <param name="providerName">要添加的ConnectionString配置项的Provider名称</param>
        /// <param name="connectionString">要添加的ConnectionString配置项的连接字符串</param>
        public static void AddConnectionString(string name, string providerName, string connectionString)
        {
            Configuration config = GetDefaultConfiguration();

            ConnectionStringSettings section = new ConnectionStringSettings();
            section.Name = name;
            section.ProviderName = providerName;
            section.ConnectionString = connectionString;

            AddConnectionStringSection(section);
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// 刷新一个配置节。
        /// </summary>
        /// <param name="sectionName">要刷新的配置节的名称。</param>
        private static void RefreshConfigurationSection(string sectionName)
        {
            ConfigurationManager.RefreshSection(sectionName);
        }

        /// <summary>
        /// 获取默认的配置文件（Configuration类型）
        /// </summary>
        /// <returns>默认的配置文件</returns>
        private static Configuration GetDefaultConfiguration()
        {
            // TODO: Check Here.
            return WebConfigurationManager.OpenWebConfiguration("~");
        }

        #endregion
    }
}
