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
    /// ���л�����Ĳ�����
    /// </summary>
    public class SerializationHelper
    {
        /// <summary>
        /// �����л���Ϣд��Xml�ļ���
        /// </summary>
        /// <param name="obj">Ҫд��Xml�ļ��Ķ���</param>
        /// <returns>�Ƿ�д��Xml�ļ�</returns>
        public static bool XmlSeriale(object obj)
        {
            if (obj == null)
                return false;
            else
            {
                //�����л���Ϣд��Xml�ļ���
                // TODO: 
                FileStream fileStream = new FileStream(@"c:\SerialeTicket.xml", FileMode.Create);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, obj);
                fileStream.Close();
                return true;
            }
        }

        /// <summary>
        /// �����л���Ϣд��Xml�ļ���
        /// </summary>
        /// <param name="xmlName">Ҫд��Xml�ļ�������</param>
        /// <param name="obj">Ҫд��Xml�ļ��Ķ���</param>
        /// <returns>�Ƿ�д��Xml�ļ�</returns>
        public static bool XmlSeriale(string xmlName, object obj)
        {
            if (obj == null)
                return false;
            else
            {
                //������ļ��Ѿ����ڣ�����ɾ��
                if (File.Exists(xmlName))
                {
                    File.Delete(xmlName);
                }
                //�����л���Ϣд��Xml�ļ���
                FileStream fileStream = new FileStream(xmlName, FileMode.Create);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, obj);
                fileStream.Close();
                return true;

            }
        }

        /// <summary>
        /// ���л�����
        /// </summary>
        /// <param name="obj">���л��Ķ���</param>
        /// <returns>�������л��������ַ���</returns>
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
        /// ���л�����
        /// </summary>
        /// <param name="obj">���л��Ķ���</param>
        /// <returns>�������л��������ֽ�</returns>
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
        /// �����л�һ���ֽ�
        /// </summary>
        /// <param name="objString">�����л��ַ���</param>
        /// <returns>���ط����л���Ķ���</returns>
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
        /// �����л�һ���ֽ�
        /// </summary>
        /// <param name="bytes">�����л���һ���ֽ�</param>
        /// <returns>���ط����л���Ķ���</returns>
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
