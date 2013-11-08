#region Comments

// 功能：对称加密算法。
// 作者：steven
// 日期：2007-01-30

// 最近修改：Anders Cui
// 最近修改日期：2007-04-03
// 修改内容：添加注释。

#endregion

using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Security.Cryptography;

namespace Andersc.CodeLib.Common.Encryption
{
    /// <summary>
    /// DES对称加密算法。
    /// </summary>
    public sealed class DESEncryption
    {
        //使用8字节（64位）的密钥作为初始向量
        private static byte[] KEY_64 = { 12, 233, 1, 122, 231, 221, 14, 8 };
        private static byte[] IV_64 = { 150, 3, 225, 13, 5, 11, 254, 112 };

        /// <summary>
        /// DES加密算法来加密一字符串（使用已定义的密钥和初始向量）。
        /// </summary>
        /// <param name="input">需要加密的字符串。</param>
        /// <returns>加密后的字符串。如果输入字符串为null或空，返回null。</returns>
        /// <example>用变量Info来存储DES算法加密一字符串后的信息：
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
        /// 使用DES加密算法来加密一字符串。
        /// </summary>
        /// <param name="input">需要加密的字符串。</param>
        /// <param name="key">密钥。</param>
        /// <param name="iv">用于加密的初始化向量。</param>
        /// <returns>加密后的字符串。如果</returns>
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
        /// 使用DES算法来解密一字符串。
        /// </summary>
        /// <param name="input">需要解密的字符串。</param>
        /// <returns>解密后的字符串。如果输入字符串为null或空，返回null。</returns>
        /// <example>用变量Info来存储DES算法解密一字符串后的信息：
        /// <code>
        /// // "VJISADF=" 为加密了的字符串。
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
