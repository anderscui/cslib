#region Comments

// ���ܣ����ݲ����Ĺ����ࡣ
// ���ߣ�Anders Cui
// ���ڣ�2007-04-13

// ����޸ģ�Anders Cui
// ����޸����ڣ�2007-04-13
// �����ࡣ

#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Andersc.CodeLib.Common
{
    /// <summary>
    /// ���ݲ����Ĺ����ࡣ
    /// </summary>
    public static class DataHelper
    {
        public static bool IsConnectionAvailable(string connectionString)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
