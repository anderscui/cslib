using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Andersc.CodeLib.Common.Data
{
    /// <summary>
    /// ���ݿ���ص�ͨ�÷��������������ݷ��ʣ�
    /// </summary>
    /// <seealso cref="DBHelper"/>
    /// <seealso cref="Broker"/>
    public class DataManipulation
    {
        private DataManipulation()
        {
        }

        /// <summary>
        /// �ж�һ��DataSet�����Ƿ���DataRow
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
        /// 2006/12/22 ����sql����еķǳ����ַ�
        /// Ŀǰ�Ĺ���ֻ���滻�ַ�����ĵ����š����Ժ��и��������ַ��Ĵ�����Լ������ƴ˷���.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <remarks>TODO:</remarks>
        public static string ReplaceSpecChar(string input)
        {
            return input.Replace("'", "''");
        }

        //��sql������»��ߵĴ��� TODO:
        public static string ReplaceUnderlineChar(string input)
        {
            return input.Replace("_", @"\_");
        }
    }
}
