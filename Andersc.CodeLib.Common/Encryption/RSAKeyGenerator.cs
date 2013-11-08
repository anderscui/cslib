#region Comments

// 功能：WinForm和WebForm的通用配置管理类，仅能在本程序集内使用。
// 作者：Steven（裴登海）
// 日期：2007-3-29

// 最近修改：
// 最近修改日期：
// 修改内容：
// TODO：

#endregion

using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Security.Cryptography;

namespace Andersc.CodeLib.Common.Encryption
{
    /// <summary>
    /// 产生公钥/私钥对，将其保存在相应文件中。
    /// </summary>
    public class RSAKeyGenerator
    {
        RSACryptoServiceProvider rsa;
        RSAParameters keys;

        /// <summary>
        /// 构造函数。
        /// </summary>
        public RSAKeyGenerator()
        {
            rsa = new RSACryptoServiceProvider();
            keys = rsa.ExportParameters(true);
        }

        /// <summary>
        /// 保存公钥。
        /// </summary>
        /// <param name="filename">保存的文件名。</param>
        public void SavePublicKey(string filename)
        {
            string pkxml = "<root>" + Environment.NewLine + "<Modulus>" + Convert.ToBase64String(keys.Modulus) + "</Modulus>";
            pkxml += Environment.NewLine + "<Exponent>" + Convert.ToBase64String(keys.Exponent) + "</Exponent>" + Environment.NewLine + "</root>";

            using (System.IO.StreamWriter sw = System.IO.File.CreateText(filename))
            {
                sw.WriteLine(pkxml);
            }
        }

        /// <summary>
        /// 保存私钥。
        /// </summary>
        /// <param name="filename">保存的文件名。</param>
        public void SavePrivateKey(string filename)
        {
            string psxml = "<root>" + Environment.NewLine + "<Modulus>" + Convert.ToBase64String(keys.Modulus) + "</Modulus>";
            psxml += Environment.NewLine + "<Exponent>" + Convert.ToBase64String(keys.Exponent) + "</Exponent>";
            psxml += Environment.NewLine + "<D>" + Convert.ToBase64String(keys.D) + "</D>";
            psxml += Environment.NewLine + "<DP>" + Convert.ToBase64String(keys.DP) + "</DP>";
            psxml += Environment.NewLine + "<P>" + Convert.ToBase64String(keys.P) + "</P>";
            psxml += Environment.NewLine + "<Q>" + Convert.ToBase64String(keys.Q) + "</Q>";
            psxml += Environment.NewLine + "<DQ>" + Convert.ToBase64String(keys.DQ) + "</DQ>";
            psxml += Environment.NewLine + "<InverseQ>" + Convert.ToBase64String(keys.InverseQ) + "</InverseQ>" + Environment.NewLine + "</root>";

            using (System.IO.StreamWriter sw = System.IO.File.CreateText(filename))
            {
                sw.WriteLine(psxml);
            }
        }
    }
}
