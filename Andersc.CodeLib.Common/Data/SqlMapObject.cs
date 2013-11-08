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
    /// 复杂对象的映射类，使用xml文件进行的反序列化操作得到
    /// TODO:增加对事务的支持
    /// </summary>
    [XmlRootAttribute("Command")]
    public sealed class SqlMapObject
    {
        public SqlMapObject()
        {
        }

        #region Mapping Properties

        /// <summary>
        /// 命令标识
        /// </summary>
        [XmlAttribute("Name")]
        public string Name;

        /// <summary>
        /// 当前查询的内容
        /// </summary>
        [XmlElement(ElementName = "CommandText")]
        public string Sql;

        /// <summary>
        /// 命令类型
        /// </summary>
        [XmlAttribute("CommandType")]
        public string CommandTypeText;

        /// <summary>
        /// 语句类型
        /// </summary>
        [XmlAttribute("StatementType")]
        public string StateTypeText;


        #endregion Mapping Properties

        /// <summary>
        /// 数据连接 // TODO:考虑支持多数据库
        /// </summary>
        //[XmlAttribute("DataConn")]
        //public string DataConn;

        /// <summary>
        /// 存放当前命令的参数
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
        /// 返回要执行命令的类型
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
                    //throw new InvalidCastException("SqlMap中语句定义的命令类型出错，不能完成转换");
            }
        }

        /// <summary>
        /// 向命令添加参数
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <param name="paraValue">参数值</param>
        /// <remarks>TODO:添加更多的重载方法以支持更多的参数类型</remarks>
        public void AddParameter(string paraName, string paraValue)
        {
            SqlParameter para = new SqlParameter();
            para.ParameterName = paraName;
            para.Value = paraValue;
            paras.Add(para);
        }

        /// <summary>
        /// 格式化当前命令字符串，顺序需与SqlMap中定义一致
        /// </summary>
        /// <param name="args"></param>
        public void AddArgs(params object[] args)
        {
            if (this.CommandTypeText != "TextWithArgs")
            {
                throw new InvalidOperationException("当前命令类型：" + this.CommandTypeText + "不支持该操作");
            }
            this.Sql = string.Format(this.Sql, args);
            this.CommandTypeText = "Text";
        }

        /// <summary>
        /// 将类型文字返回枚举类型
        /// </summary>
        /// <remarks>
        /// xml 中有一种类型TextWithargs,如果在没有调用AddArgs前调用此方法回throw
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
                    //throw new InvalidCastException("SqlMap中语句定义的命令类型出错，不能完成转换");
            }
        }
    }
}
