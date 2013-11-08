#region Comments

// ���ܣ�WinForm��WebForm��ͨ�����ù����࣬�����ڱ�������ʹ�á�
// ���ߣ�Anders Cui
// ���ڣ�2007-03-13

// ����޸ģ�Anders Cui
// ����޸����ڣ�2007-04-01
// �޸����ݣ��������ע�͡�
// TODO��

#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Andersc.CodeLib.Common
{
    /// <summary>
    /// WinForm��WebForm��ͨ�����ù����࣬�����ڱ�������ʹ�á�
    /// </summary>
    internal class ConfigManager
    {
        ///// <summary>
        ///// ����ָ����ֵ�ж�appSettings���ý���һ���������Ƿ���ڡ�
        ///// </summary>
        ///// <param name="key">Ҫ���ҵ�appSettings������ļ�ֵ��</param>
        ///// <returns>���������ڣ�����true�����򷵻�false��</returns>
        //public static bool IsAppSettingExisted(string key)
        //{
        //    if (key == null)
        //    {
        //        throw new ArgumentNullException("key", "������ļ�ֵ������Ϊnull��");
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
        /// ��ȡappSettings���ý���ָ���������������Ϣ��
        /// </summary>
        /// <param name="key">Ҫ���ҵ�appSettings������ļ�ֵ</param>
        /// <returns>�������ָ��������ڣ�����ָ���������������Ϣ����������ڣ�����keyֵΪnull��������null</returns>
        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// ��ȡ����ָ�����Ƶ������ַ����������ֵ��connectionString���ԣ���
        /// </summary>
        /// <param name="name">�����ַ�������������ơ�</param>
        /// <returns></returns>
        public static string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

    }
}
