#region Comments

// 功能：XML相关处理的工具类。
// 作者：Anders Cui
// 日期：2007-03-26

// 最近修改：Anders Cui
// 最近修改日期：2007-03-31

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
    /// XML相关处理的工具类。
    /// </summary>
    public static class XmlHelper
    {
        #region 获取XML文档、节点、节点内容的方法

        /// <summary>
        /// 获取加载了指定XML文件的XmlDocment实例。
        /// </summary>
        /// <param name="fileName">XML文件名称。</param>
        /// <returns>XmlDocment类的实例。</returns>
        public static XmlDocument GetXmlDocument(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("要读取的XML文件不存在。", fileName);
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            return doc;
        }

        /// <summary>
        /// 获取XML文档中满足指定XPath的首个节点的文本内容。
        /// </summary>
        /// <param name="fileName">XML文件名称。</param>
        /// <param name="xpath">指定的xpath。</param>
        /// <returns>节点的文本内容。</returns>
        /// <remarks>通常用于XML中元素（Element）或属性（Attribute）的查询。</remarks>
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
        /// 获取XML文档中满足指定XPath的首个节点。
        /// </summary>
        /// <param name="fileName">XML文件名称。</param>
        /// <param name="xpath">指定的xpath。</param>
        /// <returns>满足指定XPath的首个节点。</returns>
        public static XmlNode GetSingleNode(string fileName, string xpath)
        {
            XmlDocument doc = GetXmlDocument(fileName);

            return doc.SelectSingleNode(xpath);
        }

        /// <summary>
        /// 获取满足指定XPath表达式的所有节点。
        /// </summary>
        /// <param name="fileName">XML文件名称。</param>
        /// <param name="xpath">指定的xpath。</param>
        /// <returns>满足指定XPath表达式的所有节点。</returns>
        public static XmlNodeList GetNodeList(string fileName, string xpath)
        {
            XmlDocument doc = GetXmlDocument(fileName);

            return doc.SelectNodes(xpath);
        }

        /// <summary>
        /// 获取具有指定名称的所有元素。
        /// </summary>
        /// <param name="fileName">XML文件名称。</param>
        /// <param name="tagName">指定的元素名称。</param>
        /// <returns>具有指定名称的所有节点。</returns>
        public static XmlNodeList GetElementsByTagName(string fileName, string tagName)
        {
            XmlDocument doc = GetXmlDocument(fileName);

            return doc.GetElementsByTagName(tagName);
        }

        /// <summary>
        /// 获取指定命名空间下具有指定名称的所有元素。
        /// </summary>
        /// <param name="fileName">XML文件名称。</param>
        /// <param name="tagName">指定的元素名称。</param>
        /// <param name="namespaceURI">指定的命名空间</param>
        /// <returns>指定命名空间下具有指定名称的所有元素。</returns>
        public static XmlNodeList GetElementsByTagName(string fileName, string tagName, string namespaceURI)
        {
            XmlDocument doc = GetXmlDocument(fileName);

            return doc.GetElementsByTagName(tagName, namespaceURI);
        }

        /// <summary>
        /// 获取XML文件中的所有元素。
        /// </summary>
        /// <param name="fileName">XML文件名称。</param>
        /// <returns>XML文件中的所有元素。</returns>
        public static XmlNodeList GetAllElements(string fileName)
        {
            return GetElementsByTagName(fileName, "*");
        }

        /// <summary>
        /// 获取XML文件中指定命名空间下的所有元素。
        /// </summary>
        /// <param name="fileName">XML文件名称。</param>
        /// <param name="namespaceURI">指定的命名空间。</param>
        /// <returns>XML文件中指定命名空间下的所有元素。</returns>
        public static XmlNodeList GetAllElements(string fileName, string namespaceURI)
        {
            return GetElementsByTagName(fileName, "*", namespaceURI);
        } 

        #endregion

        #region 修改XML文档、节点内容、属性值的方法

        /// <summary>
        /// 更新XML文档中满足指定XPath的首个节点的内容。
        /// </summary>
        /// <param name="fileName">XML文件名称。</param>
        /// <param name="xpath">指定的xpath。</param>
        /// <param name="newText">节点的新内容。</param>
        /// <remarks>在ASP.NET程序中，主要当前用户对XML的操作权限。</remarks>
        public static void UpdateNodeText(string fileName, string xpath, string newText)
        {
            XmlNode node = GetSingleNode(fileName, xpath);

            if (node != null)
            {
                node.InnerText = newText;
            }

            node.OwnerDocument.Save(fileName);
        }

        // TODO：添加修改节点的方法。

        #endregion

        #region 添加XML元素、属性的相关操作

        // TODO：这里的方法都要进行更多的测试

        /// <summary>
        /// 添加一个节点。
        /// </summary>
        /// <param name="fileName">要修改的XML文件名称。</param>
        /// <param name="xpath">要添加的节点的父节点的xpath。</param>
        /// <param name="nodeName">新节点的名称。</param>
        /// <param name="nodeContent">新节点的内容。</param>
        public static void AppendNode(string fileName, string xpath, string nodeName, string nodeContent)
        {
            if (string.IsNullOrEmpty(nodeName))
            {
                throw new ArgumentNullException("nodeName", "节点名称不可以为空。"); 
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
        /// 添加一个属性。
        /// </summary>
        /// <param name="fileName">要修改的XML文件名称。</param>
        /// <param name="xpath">要修改的节点的xpath。</param>
        /// <param name="attributeName">新属性的名称。</param>
        /// <param name="attributeContent">新属性的值。</param>
        public static void AppendAttribute(string fileName, string xpath, string attributeName, string attributeContent)
        {
            if (string.IsNullOrEmpty(attributeName))
            {
                throw new ArgumentNullException("attributeName", "属性名称不可以为空。");
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


        #region TODO：删除节点、属性的方法



        #endregion


        #region TODO： XML文档数据操作的相关方法



        #endregion


        #region TODO： 验证文档的相关方法

        /// <summary>
        /// 验证XML文档是否有效。
        /// </summary>
        /// <param name="xmlFileName">要验证的XML文件的路径。</param>
        /// <param name="schemaFileName">用于验证XML Schema文件路径。</param>
        /// <returns>如果通过验证，返回true，否则返回false。</returns>
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
            throw new XmlSchemaValidationException("文件格式有错误。", args.Exception);
        }

        #endregion


        #region TODO： 转换文档的相关方法

        #endregion
    }
}
