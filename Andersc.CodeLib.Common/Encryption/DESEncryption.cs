#region Comments

// ���ܣ��ԳƼ����㷨��
// ���ߣ�steven
// ���ڣ�2007-01-30

// ����޸ģ�Anders Cui
// ����޸����ڣ�2007-04-03
// �޸����ݣ����ע�͡�

#endregion

using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Security.Cryptography;

namespace Andersc.CodeLib.Common.Encryption
{
    /// <summary>
    /// DES�ԳƼ����㷨��
    /// </summary>
    public sealed class DESEncryption
    {
        //ʹ��8�ֽڣ�64λ������Կ��Ϊ��ʼ����
        private static byte[] KEY_64 = { 12, 233, 1, 122, 231, 221, 14, 8 };
        private static byte[] IV_64 = { 150, 3, 225, 13, 5, 11, 254, 112 };

        /// <summary>
        /// DES�����㷨������һ�ַ�����ʹ���Ѷ������Կ�ͳ�ʼ��������
        /// </summary>
        /// <param name="input">��Ҫ���ܵ��ַ�����</param>
        /// <returns>���ܺ���ַ�������������ַ���Ϊnull��գ�����null��</returns>
        /// <example>�ñ���Info���洢DES�㷨����һ�ַ��������Ϣ��
        /// <code>
        /// string Info = DESEncryption.Encrypt("This is a test");
        /// </code>
        /// </example>
        public static string Encrypt(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            else
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(KEY_64, IV_64), CryptoStreamMode.Write);
                StreamWriter sWrite = new StreamWriter(cStream);
                sWrite.Write(input);
                sWrite.Flush();
                cStream.FlushFinalBlock();
                mStream.Flush();
                return Convert.ToBase64String(mStream.GetBuffer(), 0, (int)mStream.Length);
            }
        }

        /// <summary>
        /// ʹ��DES�����㷨������һ�ַ�����
        /// </summary>
        /// <param name="input">��Ҫ���ܵ��ַ�����</param>
        /// <param name="key">��Կ��</param>
        /// <param name="iv">���ڼ��ܵĳ�ʼ��������</param>
        /// <returns>���ܺ���ַ��������</returns>
        public static string Encrypt(string input, byte[] key, byte[] iv)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            else
            {
                DESCryptoServiceProvider encrypt = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, encrypt.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                StreamWriter sWrite = new StreamWriter(cStream);
                sWrite.Write(input);
                sWrite.Flush();
                cStream.FlushFinalBlock();
                mStream.Flush();
                return Convert.ToBase64String(mStream.GetBuffer(), 0, (int)mStream.Length);
            }
        }

        /// <summary>
        /// ʹ��DES�㷨������һ�ַ�����
        /// </summary>
        /// <param name="input">��Ҫ���ܵ��ַ�����</param>
        /// <returns>���ܺ���ַ�������������ַ���Ϊnull��գ�����null��</returns>
        /// <example>�ñ���Info���洢DES�㷨����һ�ַ��������Ϣ��
        /// <code>
        /// // "VJISADF=" Ϊ�����˵��ַ�����
        /// string Info = DESEncryption.DESDecrypt("VJISADF=");
        /// </code>
        /// </example>
        public static string DESDecrypt(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            else
            {
                DESCryptoServiceProvider desEncrypt = new DESCryptoServiceProvider();

                byte[] buffer = Convert.FromBase64String(input);
                MemoryStream mStream = new MemoryStream(buffer);
                CryptoStream cStream = new CryptoStream(mStream, desEncrypt.CreateDecryptor(KEY_64, IV_64), CryptoStreamMode.Read);
                StreamReader sReader = new StreamReader(cStream);
                return sReader.ReadToEnd();
            }
        }
    }
}
