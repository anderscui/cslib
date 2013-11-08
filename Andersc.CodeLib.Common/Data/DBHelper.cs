using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

using log4net;

namespace Andersc.CodeLib.Common.Data
{
    /// <summary>
    /// �ṩ��Sql Server���ݿ�ĸ��ֲ���
    /// </summary>
    /// �� �� ��: Anders
    /// ��������: 2006-12-12
    /// �� �� ��: 
    /// �޸�����: 
    /// �޸�����: 
    /// ��    ��: 1.0.0
    /// <remarks>
    /// TODO: enhances support of stored procedure, transaction, logging, xml data
    /// </remarks>
    public class DBHelper
    {
        //for Parameters cashing mechanism
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        //private static readonly string RETURNVALUE = "RETURNVALUE";
        private static string errorMessageFormat = string.Empty +
            "��������ʱ��������." + Environment.NewLine +
            "�����ַ���: {0}" + Environment.NewLine +
            "ִ�����: {1}" + Environment.NewLine +
            "������Ϣ: {2}" + Environment.NewLine;

        private static ILog logger = LogManager.GetLogger(typeof(DBHelper));

        #region Constructors

        private DBHelper()
        {
        }

        #endregion

        #region ExecuteReader : Return a single DataReader

        public static SqlDataReader ExecuteReader(string commandText)
        {
            return ExecuteReader(CommandType.Text, commandText, (SqlParameter[])null);
        }

        public static SqlDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            return ExecuteReader(commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// ʹ��using����Թرն�ռ����
        /// </summary>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">�����ı�����</param>
        /// <param name="cmdParms">�����������</param>
        /// <example>
        ///		dataGridView1.DataSource = DBHelper.Instance.ExecuteReader(CommandType.Text, "Select ID, Name From Employees", (SqlParameter[])null);
        /// </example>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlConnection conn = DBFactory.GetConnection();
            {
                SqlCommand cmd = DBFactory.GetSqlCommand();

                try
                {
                    PrepareCommand(conn, cmd, null, cmdType, cmdText, cmdParms);
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    cmd.Parameters.Clear();
                    return rdr;
                }
                catch (Exception ex)
                {
                    Log(conn.ConnectionString, cmdText, ex.Message);
                    throw ex;
                }
            }
        }

        public static SqlDataReader ExecuteReader(string connStringKey, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlConnection conn = DBFactory.GetConnection(connStringKey);
            {
                SqlCommand cmd = DBFactory.GetSqlCommand();

                try
                {
                    PrepareCommand(conn, cmd, null, cmdType, cmdText, cmdParms);
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    cmd.Parameters.Clear();
                    return rdr;
                }
                catch (Exception ex)
                {
                    Log(conn.ConnectionString, cmdText, ex.Message);
                    throw ex;
                }
            }
        }

        public static SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, SqlParameter[] cmdParms, bool useTrans)
        {
            SqlConnection conn = DBFactory.GetConnection();
            {
                SqlTransaction trans = null;
                if (useTrans)
                {
                    trans = conn.BeginTransaction();
                }

                SqlCommand cmd = DBFactory.GetSqlCommand();
                try
                {
                    PrepareCommand(conn, cmd, trans, cmdType, cmdText, cmdParms);
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    cmd.Parameters.Clear();

                    //Commit current transaction
                    if (useTrans)
                    {
                        trans.Commit();
                    }

                    return rdr;
                }
                catch (Exception ex)
                {
                    if (useTrans)
                    {
                        trans.Rollback();
                    }

                    Log(conn.ConnectionString, cmdText, ex.Message);
                    throw ex;
                }
            }
        }

        #endregion

        #region ExecuteScalar : Return a single value(object)

        public static object ExecuteScalar(string cmdText)
        {
            return ExecuteScalar(CommandType.Text, cmdText, null);
        }

        public static object ExecuteScalar(CommandType cmdType, string cmdText)
        {
            return ExecuteScalar(cmdType, cmdText, null);
        }

        /// <summary>
        /// Executes a sqlCommand and return a single value(object) without using transaction
        /// </summary>
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            using (SqlConnection conn = DBFactory.GetConnection())
            {
                SqlCommand cmd = DBFactory.GetSqlCommand();

                try
                {
                    PrepareCommand(conn, cmd, null, cmdType, cmdText, cmdParms);
                    object val = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();

                    return val;
                }
                catch (Exception ex)
                {
                    Log(conn.ConnectionString, cmdText, ex.Message);
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Executes a sqlCommand and return a single value(object), we can use transaction by assign true to parameter 'useTrans'
        /// </summary>
        public static object ExecuteScalar(CommandType cmdType, string cmdText, SqlParameter[] cmdParms, bool useTrans)
        {
            using (SqlConnection conn = DBFactory.GetConnection())
            {
                SqlTransaction trans = null;
                if (useTrans)
                {
                    trans = conn.BeginTransaction();
                }

                SqlCommand cmd = DBFactory.GetSqlCommand();
                try
                {
                    PrepareCommand(conn, cmd, trans, cmdType, cmdText, cmdParms);
                    object val = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();

                    //Commit current transaction
                    if (useTrans)
                    {
                        trans.Commit();
                    }

                    return val;
                }
                catch (Exception ex)
                {
                    if (useTrans)
                    {
                        trans.Rollback();
                    }

                    Log(conn.ConnectionString, cmdText, ex.Message);
                    throw ex;
                }
            }
        }

        #endregion

        #region ExecuteNonQuery : Return the number of lines affected!

        public static int ExecuteNonQuery(string cmdText)
        {
            return ExecuteNonQuery(CommandType.Text, cmdText, (SqlParameter[])null);
        }

        public static int ExecuteNonQuery(CommandType cmdType, string cmdText)
        {
            return ExecuteNonQuery(cmdType, cmdText, (SqlParameter[])null);
        }

        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            using (SqlConnection conn = DBFactory.GetConnection())
            {
                SqlCommand cmd = DBFactory.GetSqlCommand();

                try
                {
                    PrepareCommand(conn, cmd, null, cmdType, cmdText, cmdParms);
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    return val;
                }
                catch (Exception ex)
                {
                    Log(conn.ConnectionString, cmdText, ex.Message);
                    throw ex;
                }
            }
        }

        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, SqlParameter[] cmdParms, bool useTrans)
        {
            using (SqlConnection conn = DBFactory.GetConnection())
            {
                SqlTransaction trans = null;
                if (useTrans)
                {
                    trans = conn.BeginTransaction();
                }

                SqlCommand cmd = DBFactory.GetSqlCommand();
                try
                {
                    PrepareCommand(conn, cmd, trans, cmdType, cmdText, cmdParms);
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    //Commit current transaction
                    if (useTrans)
                    {
                        trans.Commit();
                    }

                    return val;
                }
                catch (Exception ex)
                {
                    if (useTrans)
                    {
                        trans.Rollback();
                    }

                    Log(conn.ConnectionString, cmdText, ex.Message);
                    throw ex;
                }
            }
        }

        #endregion

        #region ExecuteDataSet : Return a DataSet

        public static DataSet ExecuteDataSet(string cmdText)
        {
            return ExecuteDataSet(CommandType.Text, cmdText, (SqlParameter[])null);
        }

        public static DataSet ExecuteDataSet(CommandType cmdType, string cmdText)
        {
            return ExecuteDataSet(cmdType, cmdText, (SqlParameter[])null);
        }

        public static DataSet ExecuteDataSet(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            using (SqlConnection conn = DBFactory.GetConnection())
            {
                //����һ��SqlCommand���󣬲�������г�ʼ��
                SqlCommand cmd = DBFactory.GetSqlCommand();

                try
                {
                    PrepareCommand(conn, cmd, null, cmdType, cmdText, cmdParms);

                    //����SqlDataAdapter�����Լ�DataSet
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    //���ds
                    da.Fill(ds);

                    // ���cmd�Ĳ�������	
                    cmd.Parameters.Clear();

                    //����ds
                    return ds;
                }
                catch (Exception ex)
                {
                    Log(conn.ConnectionString, cmdText, ex.Message);
                    throw ex;
                }
            }
        }

        public static DataSet ExecuteDataSet(CommandType cmdType, string cmdText, SqlParameter[] cmdParms, bool useTrans)
        {
            using (SqlConnection conn = DBFactory.GetConnection())
            {
                SqlTransaction trans = null;
                if (useTrans)
                {
                    trans = conn.BeginTransaction();
                }

                //����һ��SqlCommand���󣬲�������г�ʼ��
                SqlCommand cmd = DBFactory.GetSqlCommand();
                try
                {
                    PrepareCommand(conn, cmd, trans, cmdType, cmdText, cmdParms);

                    //����SqlDataAdapter�����Լ�DataSet
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    //���ds
                    da.Fill(ds);
                    // ���cmd�Ĳ�������	
                    cmd.Parameters.Clear();

                    // �ύ����
                    if (useTrans)
                    {
                        trans.Commit();
                    }

                    //����ds
                    return ds;
                }
                catch (Exception ex)
                {
                    if (useTrans)
                    {
                        trans.Rollback();
                    }

                    Log(conn.ConnectionString, cmdText, ex.Message);
                    throw ex;
                }
            }
        }

        #endregion

        #region TODO: ExecuteXmlReader

        #endregion ExecuteXmlReader

        #region TODO: ExecProc : Execute a Stored Procedure

        /// <summary>
        /// ATODO:
        /// </summary>
        /// <param name="procName"></param>
        /// <returns></returns>
        public int ExecProc(string procName)
        {
            return 0;
        }

        public int ExecProc(string procName, SqlParameter[] paras)
        {
            return 0;
        }

        public SqlDataReader ExecProcReader(string procName)
        {
            return ExecuteReader(CommandType.StoredProcedure, procName);
        }

        public SqlDataReader ExecProcReader(string procName, SqlParameter[] paras)
        {
            return ExecuteReader(CommandType.StoredProcedure, procName, paras);
        }

        public DataSet ExecProcDataSet(string procName)
        {
            return ExecuteDataSet(CommandType.StoredProcedure, procName);
        }

        public DataSet ExecProcDataSet(string procName, SqlParameter[] paras)
        {
            return ExecuteDataSet(CommandType.StoredProcedure, procName, paras);
        }

        #endregion ExecProc : Execute a Stored Procedure

        #region Public Properties

        //TODO: Add Current Connection Manager?
        //public string DBName
        //{
        //    get
        //    {
        //        return _conn.Database;
        //    }
        //}

        //public string ConnString
        //{
        //    get
        //    {
        //        return _conn.ConnectionString;
        //    }
        //}

        //public string DataSource
        //{
        //    get
        //    {
        //        return _conn.DataSource;
        //    }
        //}

        ////get the sql server's version string
        //public string ServerVersion
        //{
        //    get
        //    {
        //        return _conn.ServerVersion;
        //    }
        //}

        #endregion Public Properties

        #region Public Methods

        public static void CacheParameters(string cacheKey, params SqlParameter[] cmdParms)
        {
            parmCache[cacheKey] = cmdParms;
        }

        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        #endregion Public Methods

        #region Private Methods

        private static void PrepareCommand(SqlConnection conn, SqlCommand cmd, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            //�ж����ӵ�״̬������ǹر�״̬�����
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            //cmd���Ը�ֵ
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            //�������ʱʱ�䣬Ĭ��Ϊ30�룬�������ִ��ʱ��ܳ������Ӧ�޸���������� ^_^
            //���Խ�������д��config�ļ���˴�����Ϊ300��
            cmd.CommandTimeout = 300;

            //���cmd��Ҫ�Ĵ洢���̲���
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }

            //�Ƿ���Ҫ�õ�������
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
        }

        private static void Log(string connString, string cmdText, string errorMessage)
        {
            logger.Error(string.Format(errorMessageFormat, connString, cmdText, errorMessage));
        }

        #endregion Private Methods
    }
}
