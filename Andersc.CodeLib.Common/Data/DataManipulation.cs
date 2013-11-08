using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Andersc.CodeLib.Common.Data
{
    /// <summary>
    /// 数据库相关的通用方法（不包含数据访问）
    /// </summary>
    /// <seealso cref="DBHelper"/>
    /// <seealso cref="Broker"/>
    public class DataManipulation
    {
        private DataManipulation()
        {
        }

        /// <summary>
        /// 判断一个DataSet对象是否含有DataRow
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static bool IsDataSetNullOrEmpty(DataSet ds)
        {
            if ((ds == null) || (ds.Tables == null) || (ds.Tables[0].Rows.Count == 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 2006/12/22 处理sql语句中的非常规字符
        /// 目前的功能只有替换字符串里的单引号。如以后还有更多特殊字符的处理可以继续完善此方法.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <remarks>TODO:</remarks>
        public static string ReplaceSpecChar(string input)
        {
            return input.Replace("'", "''");
        }

        //对sql语句中下划线的处理 TODO:
        public static string ReplaceUnderlineChar(string input)
        {
            return input.Replace("_", @"\_");
        }
    }
}
