#region Comments

// ���ܣ��ԳƼ����㷨��
// ���ߣ�steven
// ���ڣ�2007-03-27

// ����޸ģ�
// ����޸����ڣ�
// �޸����ݣ�

#endregion

using System;
using System.Text;
using System.Diagnostics;
using System.Security;
using System.Security.Cryptography;

namespace Andersc.CodeLib.Common.Encryption
{
    /// <summary>
    /// �Դ�����ַ�������Hash���㣬����ͨ��Hash�㷨���ܹ����ִ���
    /// ���ṩMD5��SHA1��SHA256��SHA512�������㷨�������ִ��ĳ�����������
    /// </summary>
    public class HashEncryption
    {
        #region //-----MD5����-----//

        /// <summary>
        /// ����MD5���ܡ�
        /// </summary>
        /// <param name="str">Ҫ���ܵ�ֵ��</param>
        /// <param name="isReturnNum">���Ϊ<c>true</c>�����ؼ��ܺ���ַ��������򲻷��ء�</param>
        /// <returns></returns>
        public static string MD5Encrypt(string str, bool isReturnNum)
        {
            byte[] b = GetKeyByteArray(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            b = md5.ComputeHash(b);
            md5.Clear();
            return GetStringValue(b, isReturnNum);
        }

        /// <summary>
        /// ����MD5���ܡ�
        /// </summary>
        /// <param name="str">Ҫ���ܵ�ֵ��</param>
        /// <returns></returns>
        public static string MD5Encrypt(string str)
        {
            return MD5Encrypt(str, true);
        }

        #endregion

        #region //-----SHA1����-----//

        public static string SHA1Encrypt(string str, bool isReturnNum)
        {
            byte[] b = GetKeyByteArray(str);
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            b = sha1.ComputeHash(b);
            sha1.Clear();
            return GetStringValue(b, isReturnNum);

        }

        public static string SHA1Encrypt(string str)
        {
            return SHA1Encrypt(str, true);
        }

        #endregion

        #region //-----SHA256����-----//
        public static string SHA256Encrypt(string str, bool isReturnNum)
        {
            byte[] b = GetKeyByteArray(str);
            SHA256 sha256 = new SHA256Managed();
            b = sha256.ComputeHash(b);
            sha256.Clear();
            return GetStringValue(b, isReturnNum);
        }

        public static string SHA256Encrypt(string str)
        {
            return SHA256Encrypt(str, true);
        }
        #endregion

        #region //-----SHA512����-----//
        public static string SHA512Encrypt(string str, bool isReturnNum)
        {
            byte[] b = GetKeyByteArray(str);
            SHA512 sha512 = new SHA512Managed();
            b = sha512.ComputeHash(b);
            sha512.Clear();
            return GetStringValue(b, isReturnNum);
        }

        public static string SHA512Encrypt(string str)
        {
            return SHA512Encrypt(str, true);
        }
        #endregion

        #region //-----Private Methods-----//

        private static string GetStringValue(byte[] b, bool isReturnNum)
        {
            string ren = String.Empty;
            if (!isReturnNum)
            {
                ren = Encoding.ASCII.GetString(b);
            }
            else
            {
                for (int i = 0; i < b.Length; i++)
                {
                    ren += b[i].ToString("x").PadLeft(2, '0');
                }
            }
            return ren;
        }

        private static byte[] GetKeyByteArray(string strKey)
        {
            return Encoding.Default.GetBytes(strKey);
        }

        #endregion
    }
}
