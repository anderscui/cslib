using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Serialization;
using log4net;

namespace Andersc.CodeLib.Common.Data
{
    /// <summary>
    /// 封装了一些数据库的操作
    /// </summary>
    public class Broker
    {
        private static ILog logger = LogManager.GetLogger(typeof(Broker));

        private Broker()
        {
        }

        /// <summary>
        /// 执行指定的Sql命令
        /// </summary>
        /// <param name="blockName"></param>
        /// <param name="commandName"></param>
        /// <returns></returns>
        public static SqlResult ExecuteMapObject(string blockName, string commandName)
        {
            SqlMapObject map = GetSqlMapObject(blockName, commandName);
            return ExecuteMapObject(map);
        }

        /// <summary>
        /// 执行指定的SqlMapObject
        /// </summary>
        /// <param name="map">指定的SqlMapObject，该对象封装了SqlCommand的必要属性</param>
        /// <returns></returns>
        public static SqlResult ExecuteMapObject(SqlMapObject map)
        {
            if (map == null) { return null; }

            SqlResult result;

            StatementType stype = map.GetStatementType();
            CommandType ctype = map.GetCommandType();
            string cmdText = map.Sql;
            SqlParameter[] paras = map.GetParameters();

            // TODO:是否使用事务 from map object
            //bool useTrans = false;
            switch (stype)
            {
                case StatementType.Insert:
                    int rowAffected = DBHelper.ExecuteNonQuery(ctype, cmdText, paras);
                    result = new SqlResult(rowAffected, stype);
                    break;
                case StatementType.Update:
                    int rowUpdated = DBHelper.ExecuteNonQuery(ctype, cmdText, paras);
                    result = new SqlResult(rowUpdated, stype);
                    break;
                case StatementType.Delete:
                    int rowDeleted = DBHelper.ExecuteNonQuery(ctype, cmdText, paras);
                    result = new SqlResult(rowDeleted, stype);
                    break;
                case StatementType.Select:
                    DataSet ds = DBHelper.ExecuteDataSet(ctype, cmdText, paras);
                    result = new SqlResult(ds, stype);
                    break;
                default:
                    result = null;
                    break;
            }

            return result;
        }

        /// <summary>
        /// 获得指定的SqlMapObject对象
        /// </summary>
        /// <param name="blockName"></param>
        /// <param name="commandName"></param>
        /// <returns></returns>
        public static SqlMapObject GetSqlMapObject(string blockName, string commandName)
        {
            string sqlMapPath = ConfigManager.GetAppSetting("SqlMapFilePath");

            XmlDocument doc = XmlHelper.GetXmlDocument(sqlMapPath);

            // TODO: 使用一个XmlPath查找
            string srch = "ViewMap/Block[@Name='" + blockName + "']";
            XmlNode xmlNode = doc.SelectSingleNode(srch);
            if (xmlNode == null)
            {
                return null;
            }

            srch = "Command[@Name='" + commandName + "']";
            xmlNode = xmlNode.SelectSingleNode(srch);
            if (xmlNode == null)
            {
                return null;
            }

            XmlNodeReader reader = new XmlNodeReader(xmlNode);
            try
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(SqlMapObject));
                return (SqlMapObject)mySerializer.Deserialize(reader);
            }
            catch (Exception ex)
            {
                string errorMessage = "文件反序列化出错，该错误位于：" + sqlMapPath + ",错误信息：" + ex.Message;
                logger.Error(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }
        }

        /// <summary>
        /// 获得指定的SqlMapObject对象，同时初始化必要的参数，未实现
        /// </summary>
        /// <param name="blockName"></param>
        /// <param name="commandName"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static SqlMapObject GetSqlMapObject(string blockName, string commandName, params object[] paras)
        {
            // TODO:
            return null;
        }
    }
}
