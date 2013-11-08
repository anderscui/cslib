#region Comments

// 功能：数据操作的工具类。
// 作者：Anders Cui
// 日期：2007-04-13

// 最近修改：Anders Cui
// 最近修改日期：2007-04-13
// 建立类。

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
    /// 数据操作的工具类。
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
