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
        /// 返回默认数据库连接
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection()
        {
            string connString = ConfigManager.GetConnectionString("DefaultConnString");
            return new SqlConnection(connString);
        }

        /// <summary>
        /// 返回配置项指定的数据库连接
        /// </summary>
        /// <param name="dbKey">配置键</param>
        /// <returns></returns>
        public static SqlConnection GetConnection(string dbKey)
        {
            string connString = ConfigManager.GetConnectionString(dbKey);
            return new SqlConnection(connString);
        }

        /// <summary>
        /// 返回一个新建的SqlCommand对象
        /// </summary>
        /// <returns></returns>
        public static SqlCommand GetSqlCommand()
        {
            return new SqlCommand();
        }

        /// <summary>
        /// 返回一个新建的SqlCommand对象，该对象附加到指定连接
        /// </summary>
        /// <param name="conn">指定连接</param>
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
