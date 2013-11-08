#region Comments

// 功能：对称加密算法。
// 作者：steven
// 日期：2007-03-27

// 最近修改：
// 最近修改日期：
// 修改内容：

#endregion

using System;
using System.Text;
using System.Diagnostics;
using System.Security;
using System.Security.Cryptography;

namespace Andersc.CodeLib.Common.Encryption
{
    /// <summary>
    /// 对传入的字符串进行Hash运算，返回通过Hash算法加密过的字串。
    /// 共提供MD5，SHA1，SHA256，SHA512等四种算法，加密字串的长度依次增大。
    /// </summary>
    public class HashEncryption
    {
        #region //-----MD5加密-----//

        /// <summary>
        /// 进行MD5加密。
        /// </summary>
        /// <param name="str">要加密的值。</param>
        /// <param name="isReturnNum">如果为<c>true</c>，返回加密后的字符串，否则不返回。</param>
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
        /// 进行MD5解密。
        /// </summary>
        /// <param name="str">要解密的值。</param>
        /// <returns></returns>
        public static string MD5Encrypt(string str)
        {
            return MD5Encrypt(str, true);
        }

        #endregion

        #region //-----SHA1加密-----//

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

        #region //-----SHA256加密-----//
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

        #region //-----SHA512加密-----//
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
