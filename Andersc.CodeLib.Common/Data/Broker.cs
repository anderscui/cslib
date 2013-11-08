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
    /// ��װ��һЩ���ݿ�Ĳ���
    /// </summary>
    public class Broker
    {
        private static ILog logger = LogManager.GetLogger(typeof(Broker));

        private Broker()
        {
        }

        /// <summary>
        /// ִ��ָ����Sql����
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
        /// ִ��ָ����SqlMapObject
        /// </summary>
        /// <param name="map">ָ����SqlMapObject���ö����װ��SqlCommand�ı�Ҫ����</param>
        /// <returns></returns>
        public static SqlResult ExecuteMapObject(SqlMapObject map)
        {
            if (map == null) { return null; }

            SqlResult result;

            StatementType stype = map.GetStatementType();
            CommandType ctype = map.GetCommandType();
            string cmdText = map.Sql;
            SqlParameter[] paras = map.GetParameters();

            // TODO:�Ƿ�ʹ������ from map object
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
        /// ���ָ����SqlMapObject����
        /// </summary>
        /// <param name="blockName"></param>
        /// <param name="commandName"></param>
        /// <returns></returns>
        public static SqlMapObject GetSqlMapObject(string blockName, string commandName)
        {
            string sqlMapPath = ConfigManager.GetAppSetting("SqlMapFilePath");

            XmlDocument doc = XmlHelper.GetXmlDocument(sqlMapPath);

            // TODO: ʹ��һ��XmlPath����
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
                string errorMessage = "�ļ������л������ô���λ�ڣ�" + sqlMapPath + ",������Ϣ��" + ex.Message;
                logger.Error(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }
        }

        /// <summary>
        /// ���ָ����SqlMapObject����ͬʱ��ʼ����Ҫ�Ĳ�����δʵ��
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
