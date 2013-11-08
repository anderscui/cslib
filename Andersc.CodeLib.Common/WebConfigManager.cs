#region Comments

// ���ܣ�ASP.NETӦ�ó���������ļ��Ĺ����ࡣ
// ���ߣ���Ǻ�
// ���ڣ�2007-03-13

// ����޸ģ�Anders Cui
// ����޸����ڣ�2007-03-31

#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web.Configuration;

namespace Andersc.CodeLib.Common
{
    /// <summary>
    /// ASP.NETӦ�ó���������ļ��Ĺ����ࡣ
    /// </summary>
    public static class WebConfigManager
    {
        private static string AppSettingsSectionName = "appSettings";
        private static string ConnectionStringSectionName = "connectionStrings";

        // TODO: ��Ӷ�ָ�������ļ�����Ĭ�������ļ����Ĳ�����

        #region appSettings�Ĳ���

        /// <summary>
        /// ����ָ����ֵ�ж�appSettings���ý���һ���������Ƿ���ڡ�
        /// </summary>
        /// <param name="key">Ҫ���ҵ�appSettings������ļ�ֵ</param>
        /// <returns>���������ڣ�����true�����򷵻�false��</returns>
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
        /// �޸�appSettings���ý���ָ���������������Ϣ��
        /// </summary>
        /// <param name="key">Ҫ�޸ĵ�appSettings������ļ�ֵ</param>
        /// <param name="value">Ҫ�޸ĵ�appSettings���������ֵ</param>
        public static void ModifyAppSetting(string key, string value)
        {
            ModifyAppSetting(ConfigurationSaveMode.Modified, key, value);
        }

        /// <summary>
        /// �޸�appSettings���ý���ָ���������������Ϣ��
        /// </summary>
        /// <param name="mode">�޸������ļ���ģʽ��</param>
        /// <param name="key">Ҫ�޸ĵ�appSettings������ļ�ֵ</param>
        /// <param name="value">Ҫ�޸ĵ�appSettings���������ֵ</param>
        public static void ModifyAppSetting(ConfigurationSaveMode mode, string key, string value)
        {
            if (!IsAppSettingExisted(key))
            {
                // TODO��ʹ����Դ�ļ�
                throw new InvalidOperationException(string.Format("��ֵΪ{0}����������ڡ�", key));
            }

            Configuration config = GetDefaultConfiguration();
            config.AppSettings.Settings[key].Value = value;
            config.Save(mode);
            ConfigurationManager.RefreshSection(AppSettingsSectionName);
        }

        /// <summary>
        /// ɾ��appSettings���ý���ָ���������
        /// </summary>
        /// <param name="key">Ҫɾ����������ļ�ֵ</param>
        public static void DeleteAppSetting(string key)
        {
            Configuration config = GetDefaultConfiguration();
            config.AppSettings.Settings.Remove(key);
            config.Save();
            ConfigurationManager.RefreshSection(AppSettingsSectionName);
        }

        /// <summary>
        /// ɾ��appSettings�����е�������Ϣ��
        /// </summary>
        public static void ClearAppSettings()
        {
            Configuration config = GetDefaultConfiguration();
            config.AppSettings.Settings.Clear();
            config.Save();
            ConfigurationManager.RefreshSection(AppSettingsSectionName);
        }

        /// <summary>
        /// ���һ��appSettings����������ͬ��ֵ���������Ѵ��ڣ��򱾷���������ֵ׷�ӵ�ԭ��ֵ֮��
        /// </summary>
        /// <param name="key">Ҫ��ӵ�appSetting������ļ�ֵ</param>
        /// <param name="value">Ҫ��ӵ�appSetting�������ֵ</param>
        public static void AddAppSetting(string key, string value)
        {
            Configuration config = GetDefaultConfiguration();
            config.AppSettings.Settings.Add(key, value);
            config.Save();
            ConfigurationManager.RefreshSection(AppSettingsSectionName);
        }

        /// <summary>
        /// ���һ��appSettings�����
        /// </summary>
        /// <param name="key">Ҫ��ӵ�appSetting������ļ�ֵ</param>
        /// <param name="value">Ҫ��ӵ�appSetting�������ֵ</param>
        /// <param name="overrideOldValue">���Ϊtrue���򸲸��Ѵ��ڵ��������ֵ������׷�ӵ�ԭ��ֵ֮��</param>
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


        #region �����ַ������������

        /// <summary>
        /// ����ָ�������ж�connectionStrings���ý���һ���������Ƿ���ڡ�
        /// </summary>
        /// <param name="name">Ҫ���ҵ�connectionString����������ơ�</param>
        /// <returns>���������ڣ�����true�����򷵻�false��</returns>
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
        /// ��ȡ����ָ�����Ƶ������ַ��������
        /// </summary>
        /// <param name="name">�����ַ�������������ơ�</param>
        /// <returns>�����������ڣ����ظ���������򷵻�null</returns>
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
        /// ��ȡ����ָ�����Ƶ������ַ����������ֵ��connectionString���ԣ���
        /// </summary>
        /// <param name="name">�����ַ�������������ơ�</param>
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
        /// ��ȡ����ָ�����Ƶ������ַ����������providerName��
        /// </summary>
        /// <param name="name">�����ַ�������������ơ�</param>
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
        /// �޸�ConnectionString��Ϣ������
        /// </summary>
        /// <param name="originalName">ConnectionString��ԭ������</param>
        /// <param name="newName">ConnectionString��������</param>
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
                throw new InvalidOperationException("Ҫ�޸ĵ�����Ϊ" + originalName + "�������ַ��������ڡ�");
            }

            config.Save(ConfigurationSaveMode.Modified);
            RefreshConfigurationSection(ConnectionStringSectionName);
        }

        /// <summary>
        /// �޸�ConnectionString��Ϣ��Provider
        /// </summary>
        /// <param name="name">ConnectionString������</param>
        /// <param name="newProvider">ConnectionString����Povider</param>
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
                throw new InvalidOperationException("Ҫ�޸ĵ�����Ϊ" + name + "�������ַ��������ڡ�");
            }

            config.Save(ConfigurationSaveMode.Modified);
            RefreshConfigurationSection(ConnectionStringSectionName);
        }

        /// <summary>
        /// �޸�ָ����ConnectionStringֵ
        /// </summary>
        /// <param name="name">Ҫ�޸ĵ�ConnectionString������</param>
        /// <param name="newConnectionString">ConnectionString����ֵ</param>
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
                throw new InvalidOperationException("Ҫ�޸ĵ�����Ϊ" + name + "�������ַ��������ڡ�");
            }

            config.Save(ConfigurationSaveMode.Modified);
            RefreshConfigurationSection(ConnectionStringSectionName);
        }

        /// <summary>
        /// �޸�ConnectionString�������Provider��ConnectionString����
        /// </summary>
        /// <param name="name">Ҫ�޸�ConnectionString�����������</param>
        /// <param name="newPrivoder">ConnectionString���������Providerֵ</param>
        /// <param name="newConnectionString">ConnectionString���������ConnectionStringֵ</param>
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
                throw new InvalidOperationException("Ҫ�޸ĵ�����Ϊ" + name + "�������ַ��������ڡ�");
            }

            config.Save(ConfigurationSaveMode.Modified);
            RefreshConfigurationSection(ConnectionStringSectionName);
        }

        /// <summary>
        /// �޸�ָ�����Ƶ�ConnectionString�����
        /// </summary>
        /// <param name="name">Ҫ�޸�ConnectionString�����������</param>
        /// <param name="section">Ҫ�޸�ConnectionString���������ֵ</param>
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
                throw new InvalidOperationException("Ҫ�޸ĵ�����Ϊ" + name + "�������ַ��������ڡ�");
            }

            config.Save(ConfigurationSaveMode.Modified);
            RefreshConfigurationSection(ConnectionStringSectionName);
        }

        /// <summary>
        /// ɾ��ָ�����Ƶ�ConnectionString�����
        /// </summary>
        /// <param name="name">Ҫɾ����ConnectionString�����������</param>
        public static void DeleteConnectionString(string name)
        {
            Configuration config = GetDefaultConfiguration();

            config.ConnectionStrings.ConnectionStrings.Remove(name);
            config.Save();

            RefreshConfigurationSection(ConnectionStringSectionName);
        }

        /// <summary>
        /// ɾ������ConnectionString���ý�  
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
        /// ���һ��ConnectionString�����
        /// </summary>
        /// <param name="section">Ҫ��ӵ�ConnectionString����������ݡ�</param>
        public static void AddConnectionStringSection(ConnectionStringSettings section)
        {
            if (section == null)
            {
                throw new ArgumentNullException("section", "�����ַ����������ֵ����Ϊnull��");
            }

            string name = section.Name;
            if (IsConnectionExisted(name))
            {
                throw new InvalidCastException(string.Format("����Ϊ{0}�������ַ����������Ѵ��ڡ�", name));
            }

            Configuration config = GetDefaultConfiguration();
            config.ConnectionStrings.ConnectionStrings.Add(section);
            config.Save();

            RefreshConfigurationSection(ConnectionStringSectionName);
        }

        /// <summary>
        /// ���һ��ConnectionString�����
        /// </summary>
        /// <param name="name">Ҫ��ӵ�ConnectionString�����������</param>
        /// <param name="connectionString">Ҫ��ӵ�ConnectionString������������ַ���</param>
        public static void AddConnectionString(string name, string connectionString)
        {
            Configuration config = GetDefaultConfiguration();

            ConnectionStringSettings section = new ConnectionStringSettings();
            section.Name = name;
            section.ConnectionString = connectionString;

            AddConnectionStringSection(section);
        }

        /// <summary>
        /// ���һ��ConnectionString�����
        /// </summary>
        /// <param name="name">Ҫ��ӵ�ConnectionString�����������</param>
        /// <param name="providerName">Ҫ��ӵ�ConnectionString�������Provider����</param>
        /// <param name="connectionString">Ҫ��ӵ�ConnectionString������������ַ���</param>
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
        /// ˢ��һ�����ýڡ�
        /// </summary>
        /// <param name="sectionName">Ҫˢ�µ����ýڵ����ơ�</param>
        private static void RefreshConfigurationSection(string sectionName)
        {
            ConfigurationManager.RefreshSection(sectionName);
        }

        /// <summary>
        /// ��ȡĬ�ϵ������ļ���Configuration���ͣ�
        /// </summary>
        /// <returns>Ĭ�ϵ������ļ�</returns>
        private static Configuration GetDefaultConfiguration()
        {
            // TODO: Check Here.
            return WebConfigurationManager.OpenWebConfiguration("~");
        }

        #endregion
    }
}
