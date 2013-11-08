#region Comments

// ���ܣ�WinForm��WebForm��ͨ�����ù����࣬�����ڱ�������ʹ�á�
// ���ߣ�Steven����Ǻ���
// ���ڣ�2007-3-29

// ����޸ģ�
// ����޸����ڣ�
// �޸����ݣ�
// TODO��

#endregion

using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Security.Cryptography;

namespace Andersc.CodeLib.Common.Encryption
{
    /// <summary>
    /// ������Կ/˽Կ�ԣ����䱣������Ӧ�ļ��С�
    /// </summary>
    public class RSAKeyGenerator
    {
        RSACryptoServiceProvider rsa;
        RSAParameters keys;

        /// <summary>
        /// ���캯����
        /// </summary>
        public RSAKeyGenerator()
        {
            rsa = new RSACryptoServiceProvider();
            keys = rsa.ExportParameters(true);
        }

        /// <summary>
        /// ���湫Կ��
        /// </summary>
        /// <param name="filename">������ļ�����</param>
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
        /// ����˽Կ��
        /// </summary>
        /// <param name="filename">������ļ�����</param>
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
