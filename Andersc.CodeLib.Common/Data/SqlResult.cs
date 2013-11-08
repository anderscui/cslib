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
        ///�����������
        /// </summary>
        public StatementType StatementType
        {
            get { return this.statementType; }
        }

        #region ������ַ��ؽ�����ֶκ�����

        private int rowsAffected = 0;
        /// <summary>
        /// ִ��һ������Ӱ�������������Insert, Update, Delete
        /// </summary>
        public int RowsAffected
        {
            get { return this.rowsAffected; }
        }

        private DataSet ds = null;
        /// <summary>
        /// ִ������ص�DataSet������Select
        /// </summary>
        public DataSet DataSet
        {
            get { return this.ds; }
        }

        private object scalar = null;
        /// <summary>
        /// ִ������صĶ�������Sum, Count��
        /// </summary>
        public object ResultObject
        {
            get { return this.scalar; }
        }

        // TODO:���ʹDataSet��DataReader����
        private SqlDataReader reader = null;
        public SqlDataReader Reader
        {
            get { return this.reader; }
        }

        #endregion ������ַ��ؽ�����ֶκ�����

        #region �����쳣��Ϣ���ֶκ�����

        // 0 = no error, TODO: errorCode in depth
        private int errorCode = 0;
        /// <summary>
        /// <p>ִ����������Ĵ����Code</p>
        /// </summary>
        public int ErrorCode
        {
            get { return errorCode; }
        }

        private Exception error = null;
        /// <summary>
        /// ִ�������������쳣����
        /// </summary>
        public Exception Error
        {
            get { return error; }
        }

        /// <summary>
        /// ִ�������������쳣�����������Ϣ
        /// </summary>
        public string ErrorMessage
        {
            get { return (error != null) ? error.Message : string.Empty; }
        }

        #endregion ������ַ��ؽ�����ֶκ�����

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
        /// ���ݿ���������쳣ʱ
        /// </summary>
        /// <param name="error">�쳣����</param>
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
