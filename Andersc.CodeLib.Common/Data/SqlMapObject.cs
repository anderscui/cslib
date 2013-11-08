using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Andersc.CodeLib.Common.Data
{
    /// <summary>
    /// ���Ӷ����ӳ���࣬ʹ��xml�ļ����еķ����л������õ�
    /// TODO:���Ӷ������֧��
    /// </summary>
    [XmlRootAttribute("Command")]
    public sealed class SqlMapObject
    {
        public SqlMapObject()
        {
        }

        #region Mapping Properties

        /// <summary>
        /// �����ʶ
        /// </summary>
        [XmlAttribute("Name")]
        public string Name;

        /// <summary>
        /// ��ǰ��ѯ������
        /// </summary>
        [XmlElement(ElementName = "CommandText")]
        public string Sql;

        /// <summary>
        /// ��������
        /// </summary>
        [XmlAttribute("CommandType")]
        public string CommandTypeText;

        /// <summary>
        /// �������
        /// </summary>
        [XmlAttribute("StatementType")]
        public string StateTypeText;


        #endregion Mapping Properties

        /// <summary>
        /// �������� // TODO:����֧�ֶ����ݿ�
        /// </summary>
        //[XmlAttribute("DataConn")]
        //public string DataConn;

        /// <summary>
        /// ��ŵ�ǰ����Ĳ���
        /// </summary>
        private IList paras = new ArrayList();

        public SqlParameter[] GetParameters()
        {
            SqlParameter[] tempParas;
            if (paras.Count != 0)
            {
                tempParas = new SqlParameter[paras.Count];
                for (int i = 0; i < paras.Count; i++)
                {
                    tempParas[i] = paras[i] as SqlParameter;
                }
                return tempParas;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ����Ҫִ�����������
        /// </summary>
        /// <returns>System.Data.CommandType Type, Text by default</returns>
        public CommandType GetCommandType()
        {
            switch (this.CommandTypeText)
            {
                case "Text":
                    return CommandType.Text;
                case "StoredProcedure":
                    return CommandType.StoredProcedure;
                case "TableDirect":
                    return CommandType.TableDirect;
                default:
                    return CommandType.Text;
                    //throw new InvalidCastException("SqlMap����䶨����������ͳ����������ת��");
            }
        }

        /// <summary>
        /// ��������Ӳ���
        /// </summary>
        /// <param name="paraName">��������</param>
        /// <param name="paraValue">����ֵ</param>
        /// <remarks>TODO:��Ӹ�������ط�����֧�ָ���Ĳ�������</remarks>
        public void AddParameter(string paraName, string paraValue)
        {
            SqlParameter para = new SqlParameter();
            para.ParameterName = paraName;
            para.Value = paraValue;
            paras.Add(para);
        }

        /// <summary>
        /// ��ʽ����ǰ�����ַ�����˳������SqlMap�ж���һ��
        /// </summary>
        /// <param name="args"></param>
        public void AddArgs(params object[] args)
        {
            if (this.CommandTypeText != "TextWithArgs")
            {
                throw new InvalidOperationException("��ǰ�������ͣ�" + this.CommandTypeText + "��֧�ָò���");
            }
            this.Sql = string.Format(this.Sql, args);
            this.CommandTypeText = "Text";
        }

        /// <summary>
        /// ���������ַ���ö������
        /// </summary>
        /// <remarks>
        /// xml ����һ������TextWithargs,�����û�е���AddArgsǰ���ô˷�����throw
        /// </remarks>
        /// <returns>StatementType.Select by default</returns>
        public StatementType GetStatementType()
        {
            switch (this.StateTypeText)
            {
                case "Insert":
                    return StatementType.Insert;
                case "Delete":
                    return StatementType.Delete;
                case "Update":
                    return StatementType.Update;
                case "Select":
                    return StatementType.Select;
                default:
                    return StatementType.Select;
                    //throw new InvalidCastException("SqlMap����䶨����������ͳ����������ת��");
            }
        }
    }
}
