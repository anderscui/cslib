using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Andersc.CodeLib.Common.Data
{
    public class SqlResult
    {
        private StatementType statementType;
        /// <summary>
        ///返回语句类型
        /// </summary>
        public StatementType StatementType
        {
            get { return this.statementType; }
        }

        #region 处理各种返回结果的字段和属性

        private int rowsAffected = 0;
        /// <summary>
        /// 执行一个命令影响的行数，用于Insert, Update, Delete
        /// </summary>
        public int RowsAffected
        {
            get { return this.rowsAffected; }
        }

        private DataSet ds = null;
        /// <summary>
        /// 执行命令返回的DataSet，用于Select
        /// </summary>
        public DataSet DataSet
        {
            get { return this.ds; }
        }

        private object scalar = null;
        /// <summary>
        /// 执行命令返回的对象，用于Sum, Count等
        /// </summary>
        public object ResultObject
        {
            get { return this.scalar; }
        }

        // TODO:如何使DataSet和DataReader并存
        private SqlDataReader reader = null;
        public SqlDataReader Reader
        {
            get { return this.reader; }
        }

        #endregion 处理各种返回结果的字段和属性

        #region 处理异常信息的字段和属性

        // 0 = no error, TODO: errorCode in depth
        private int errorCode = 0;
        /// <summary>
        /// <p>执行语句引发的错误的Code</p>
        /// </summary>
        public int ErrorCode
        {
            get { return errorCode; }
        }

        private Exception error = null;
        /// <summary>
        /// 执行命令引发的异常对象
        /// </summary>
        public Exception Error
        {
            get { return error; }
        }

        /// <summary>
        /// 执行命令引发的异常对象的描述信息
        /// </summary>
        public string ErrorMessage
        {
            get { return (error != null) ? error.Message : string.Empty; }
        }

        #endregion 处理各种返回结果的字段和属性

        #region Constructors

        public SqlResult(int rowAffected, StatementType type)
        {
            this.statementType = type;
            this.rowsAffected = rowAffected;
        }

        public SqlResult(DataSet data, StatementType type)
        {
            this.statementType = type;
            this.ds = data;
        }

        public SqlResult(object obj, StatementType type)
        {
            this.statementType = type;
            this.scalar = obj;
        }

        /// <summary>
        /// 数据库访问引发异常时
        /// </summary>
        /// <param name="error">异常对象</param>
        public SqlResult(Exception error)
        {
            this.error = error;
            // SQL Server specific:
            if (error is SqlException)
            {
                this.errorCode = (error as SqlException).Number;
            }
            else
            {
                this.errorCode = 42; // it's magic
            }
        }

        #endregion
    }
}
