using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Andersc.CodeLib.Common.Serialization
{
    /// <summary>
    /// 序列化对象的操作类
    /// </summary>
    public class SerializationHelper
    {
        /// <summary>
        /// 将序列化信息写入Xml文件中
        /// </summary>
        /// <param name="obj">要写入Xml文件的对象</param>
        /// <returns>是否写入Xml文件</returns>
        public static bool XmlSeriale(object obj)
        {
            if (obj == null)
                return false;
            else
            {
                //将序列化信息写入Xml文件中
                // TODO: 
                FileStream fileStream = new FileStream(@"c:\SerialeTicket.xml", FileMode.Create);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, obj);
                fileStream.Close();
                return true;
            }
        }

        /// <summary>
        /// 将序列化信息写入Xml文件中
        /// </summary>
        /// <param name="xmlName">要写入Xml文件的名称</param>
        /// <param name="obj">要写入Xml文件的对象</param>
        /// <returns>是否写入Xml文件</returns>
        public static bool XmlSeriale(string xmlName, object obj)
        {
            if (obj == null)
                return false;
            else
            {
                //如果该文件已经存在，将其删除
                if (File.Exists(xmlName))
                {
                    File.Delete(xmlName);
                }
                //将序列化信息写入Xml文件中
                FileStream fileStream = new FileStream(xmlName, FileMode.Create);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, obj);
                fileStream.Close();
                return true;

            }
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="obj">序列化的对象</param>
        /// <returns>返回序列化对象后的字符串</returns>
        public static string DataSerializeString(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            else
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    formatter.Serialize(ms, obj);
                    byte[] bytes = ms.ToArray();
                    string result = Convert.ToBase64String(bytes, 0, bytes.Length);
                    return result;
                }
            }
        }

        /// <summary> 
        /// 序列化对象
        /// </summary>
        /// <param name="obj">序列化的对象</param>
        /// <returns>返回序列化对象后的字节</returns>
        public static byte[] DataSerializeByte(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            else
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    formatter.Serialize(ms, obj);
                    byte[] bytes = ms.ToArray();
                    ms.Read(bytes, 0, bytes.Length);
                    return bytes;
                }
            }
        }

        /// <summary>
        /// 反序列化一串字节
        /// </summary>
        /// <param name="objString">反序列化字符串</param>
        /// <returns>返回反序列化后的对象</returns>
        public static object DataDeserialize(string objString)
        {
            byte[] bytes = Convert.FromBase64String(objString);
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length))
            {
                object obj = formatter.Deserialize(ms);
                return obj;
            }
        }

        /// <summary>
        /// 反序列化一串字节
        /// </summary>
        /// <param name="bytes">反序列化的一串字节</param>
        /// <returns>返回反序列化后的对象</returns>
        public static object DataDeserializeByte(byte[] bytes)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                object obj = formatter.Deserialize(ms);
                return obj;
            }
        }

    }
}
