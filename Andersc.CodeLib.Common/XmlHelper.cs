#region Comments

// ���ܣ�XML��ش���Ĺ����ࡣ
// ���ߣ�Anders Cui
// ���ڣ�2007-03-26

// ����޸ģ�Anders Cui
// ����޸����ڣ�2007-03-31

#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Andersc.CodeLib.Common
{
    /// <summary>
    /// XML��ش���Ĺ����ࡣ
    /// </summary>
    public static class XmlHelper
    {
        #region ��ȡXML�ĵ����ڵ㡢�ڵ����ݵķ���

        /// <summary>
        /// ��ȡ������ָ��XML�ļ���XmlDocmentʵ����
        /// </summary>
        /// <param name="fileName">XML�ļ����ơ�</param>
        /// <returns>XmlDocment���ʵ����</returns>
        public static XmlDocument GetXmlDocument(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("Ҫ��ȡ��XML�ļ������ڡ�", fileName);
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            return doc;
        }

        /// <summary>
        /// ��ȡXML�ĵ�������ָ��XPath���׸��ڵ���ı����ݡ�
        /// </summary>
        /// <param name="fileName">XML�ļ����ơ�</param>
        /// <param name="xpath">ָ����xpath��</param>
        /// <returns>�ڵ���ı����ݡ�</returns>
        /// <remarks>ͨ������XML��Ԫ�أ�Element�������ԣ�Attribute���Ĳ�ѯ��</remarks>
        public static string GetNodeText(string fileName, string xpath)
        {
            XmlNode node = GetSingleNode(fileName, xpath);
            if (node != null)
            {
                return node.InnerText;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ��ȡXML�ĵ�������ָ��XPath���׸��ڵ㡣
        /// </summary>
        /// <param name="fileName">XML�ļ����ơ�</param>
        /// <param name="xpath">ָ����xpath��</param>
        /// <returns>����ָ��XPath���׸��ڵ㡣</returns>
        public static XmlNode GetSingleNode(string fileName, string xpath)
        {
            XmlDocument doc = GetXmlDocument(fileName);

            return doc.SelectSingleNode(xpath);
        }

        /// <summary>
        /// ��ȡ����ָ��XPath���ʽ�����нڵ㡣
        /// </summary>
        /// <param name="fileName">XML�ļ����ơ�</param>
        /// <param name="xpath">ָ����xpath��</param>
        /// <returns>����ָ��XPath���ʽ�����нڵ㡣</returns>
        public static XmlNodeList GetNodeList(string fileName, string xpath)
        {
            XmlDocument doc = GetXmlDocument(fileName);

            return doc.SelectNodes(xpath);
        }

        /// <summary>
        /// ��ȡ����ָ�����Ƶ�����Ԫ�ء�
        /// </summary>
        /// <param name="fileName">XML�ļ����ơ�</param>
        /// <param name="tagName">ָ����Ԫ�����ơ�</param>
        /// <returns>����ָ�����Ƶ����нڵ㡣</returns>
        public static XmlNodeList GetElementsByTagName(string fileName, string tagName)
        {
            XmlDocument doc = GetXmlDocument(fileName);

            return doc.GetElementsByTagName(tagName);
        }

        /// <summary>
        /// ��ȡָ�������ռ��¾���ָ�����Ƶ�����Ԫ�ء�
        /// </summary>
        /// <param name="fileName">XML�ļ����ơ�</param>
        /// <param name="tagName">ָ����Ԫ�����ơ�</param>
        /// <param name="namespaceURI">ָ���������ռ�</param>
        /// <returns>ָ�������ռ��¾���ָ�����Ƶ�����Ԫ�ء�</returns>
        public static XmlNodeList GetElementsByTagName(string fileName, string tagName, string namespaceURI)
        {
            XmlDocument doc = GetXmlDocument(fileName);

            return doc.GetElementsByTagName(tagName, namespaceURI);
        }

        /// <summary>
        /// ��ȡXML�ļ��е�����Ԫ�ء�
        /// </summary>
        /// <param name="fileName">XML�ļ����ơ�</param>
        /// <returns>XML�ļ��е�����Ԫ�ء�</returns>
        public static XmlNodeList GetAllElements(string fileName)
        {
            return GetElementsByTagName(fileName, "*");
        }

        /// <summary>
        /// ��ȡXML�ļ���ָ�������ռ��µ�����Ԫ�ء�
        /// </summary>
        /// <param name="fileName">XML�ļ����ơ�</param>
        /// <param name="namespaceURI">ָ���������ռ䡣</param>
        /// <returns>XML�ļ���ָ�������ռ��µ�����Ԫ�ء�</returns>
        public static XmlNodeList GetAllElements(string fileName, string namespaceURI)
        {
            return GetElementsByTagName(fileName, "*", namespaceURI);
        } 

        #endregion

        #region �޸�XML�ĵ����ڵ����ݡ�����ֵ�ķ���

        /// <summary>
        /// ����XML�ĵ�������ָ��XPath���׸��ڵ�����ݡ�
        /// </summary>
        /// <param name="fileName">XML�ļ����ơ�</param>
        /// <param name="xpath">ָ����xpath��</param>
        /// <param name="newText">�ڵ�������ݡ�</param>
        /// <remarks>��ASP.NET�����У���Ҫ��ǰ�û���XML�Ĳ���Ȩ�ޡ�</remarks>
        public static void UpdateNodeText(string fileName, string xpath, string newText)
        {
            XmlNode node = GetSingleNode(fileName, xpath);

            if (node != null)
            {
                node.InnerText = newText;
            }

            node.OwnerDocument.Save(fileName);
        }

        // TODO������޸Ľڵ�ķ�����

        #endregion

        #region ���XMLԪ�ء����Ե���ز���

        // TODO������ķ�����Ҫ���и���Ĳ���

        /// <summary>
        /// ���һ���ڵ㡣
        /// </summary>
        /// <param name="fileName">Ҫ�޸ĵ�XML�ļ����ơ�</param>
        /// <param name="xpath">Ҫ��ӵĽڵ�ĸ��ڵ��xpath��</param>
        /// <param name="nodeName">�½ڵ�����ơ�</param>
        /// <param name="nodeContent">�½ڵ�����ݡ�</param>
        public static void AppendNode(string fileName, string xpath, string nodeName, string nodeContent)
        {
            if (string.IsNullOrEmpty(nodeName))
            {
                throw new ArgumentNullException("nodeName", "�ڵ����Ʋ�����Ϊ�ա�"); 
            }

            XmlNode parentNode = GetSingleNode(fileName, xpath);
            if (parentNode != null)
            {
                XmlNode node = parentNode.OwnerDocument.CreateElement(nodeName);
                parentNode.AppendChild(node);
                if (nodeContent != null)
                {
                    XmlNode content = parentNode.OwnerDocument.CreateTextNode(nodeContent);
                    node.AppendChild(content);

                    parentNode.OwnerDocument.Save(fileName);
                }
            }
        }

        /// <summary>
        /// ���һ�����ԡ�
        /// </summary>
        /// <param name="fileName">Ҫ�޸ĵ�XML�ļ����ơ�</param>
        /// <param name="xpath">Ҫ�޸ĵĽڵ��xpath��</param>
        /// <param name="attributeName">�����Ե����ơ�</param>
        /// <param name="attributeContent">�����Ե�ֵ��</param>
        public static void AppendAttribute(string fileName, string xpath, string attributeName, string attributeContent)
        {
            if (string.IsNullOrEmpty(attributeName))
            {
                throw new ArgumentNullException("attributeName", "�������Ʋ�����Ϊ�ա�");
            }

            XmlNode node = GetSingleNode(fileName, xpath);
            if (node != null)
            {
                XmlAttribute attribute = node.OwnerDocument.CreateAttribute(attributeName);
                if (attributeContent != null)
                {
                    attribute.Value = attributeContent;
                    node.Attributes.Append(attribute);

                    node.OwnerDocument.Save(fileName);
                }
            }
        }

        #endregion


        #region TODO��ɾ���ڵ㡢���Եķ���



        #endregion


        #region TODO�� XML�ĵ����ݲ�������ط���



        #endregion


        #region TODO�� ��֤�ĵ�����ط���

        /// <summary>
        /// ��֤XML�ĵ��Ƿ���Ч��
        /// </summary>
        /// <param name="xmlFileName">Ҫ��֤��XML�ļ���·����</param>
        /// <param name="schemaFileName">������֤XML Schema�ļ�·����</param>
        /// <returns>���ͨ����֤������true�����򷵻�false��</returns>
        public static bool Validate(string xmlFileName, string schemaFileName)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;

            XmlSchemaSet schemas = new XmlSchemaSet();
            settings.Schemas = schemas;
            schemas.Add(null, schemaFileName);

            settings.ValidationEventHandler += new ValidationEventHandler(XmlHelper.ValidateEventHandler);

            XmlReader validator = XmlReader.Create(xmlFileName, settings);

            try
            {
                while (validator.Read())
                {
                }
            }
            finally
            {
                validator.Close();
            }

            return true;
        }

        private static void ValidateEventHandler(object sender, ValidationEventArgs args)
        {
            throw new XmlSchemaValidationException("�ļ���ʽ�д���", args.Exception);
        }

        #endregion


        #region TODO�� ת���ĵ�����ط���

        #endregion
    }
}
