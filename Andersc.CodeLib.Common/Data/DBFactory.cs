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

        public static SqlConnection GetConnection()
        {
            string connString = ConfigManager.GetConnectionString("DefaultConnString");
            return new SqlConnection(connString);
        }

        public static SqlConnection GetConnection(string dbKey)
        {
            string connString = ConfigManager.GetConnectionString(dbKey);
            return new SqlConnection(connString);
        }

        public static SqlCommand GetSqlCommand()
        {
            return new SqlCommand();
        }

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
