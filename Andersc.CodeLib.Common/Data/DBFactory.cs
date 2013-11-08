using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Andersc.CodeLib.Common.Data
{
    public class DBFactory
    {
        private DBFactory()
        {
        }

        /// <summary>
        /// ����Ĭ�����ݿ�����
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection()
        {
            string connString = ConfigManager.GetConnectionString("DefaultConnString");
            return new SqlConnection(connString);
        }

        /// <summary>
        /// ����������ָ�������ݿ�����
        /// </summary>
        /// <param name="dbKey">���ü�</param>
        /// <returns></returns>
        public static SqlConnection GetConnection(string dbKey)
        {
            string connString = ConfigManager.GetConnectionString(dbKey);
            return new SqlConnection(connString);
        }

        /// <summary>
        /// ����һ���½���SqlCommand����
        /// </summary>
        /// <returns></returns>
        public static SqlCommand GetSqlCommand()
        {
            return new SqlCommand();
        }

        /// <summary>
        /// ����һ���½���SqlCommand���󣬸ö��󸽼ӵ�ָ������
        /// </summary>
        /// <param name="conn">ָ������</param>
        /// <returns></returns>
        public static SqlCommand GetSqlCommand(SqlConnection conn)
        {
            if (conn == null)
            {
                return new SqlCommand();
            }
            else
            {
                return conn.CreateCommand();
            }
        }
    }
}
