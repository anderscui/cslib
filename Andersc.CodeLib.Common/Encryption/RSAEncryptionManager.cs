#region Comments

// 功能：对称加密算法。
// 作者：steven
// 日期：2007-03-29

// 最近修改：
// 修改日期：
// 修改内容：

#endregion

using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Xml;
using System.Configuration;
using System.Security.Cryptography;

namespace Andersc.CodeLib.Common.Encryption
{
    /// <summary>
    /// 非对称加密算法。
    /// </summary>
    public class RSAEncryptionManager
    {
        RSACryptoServiceProvider rsa;
        RSACryptoServiceProvider Drsa;

        /// <summary>
        /// 构造函数(使用默认的公钥/密钥存放文件)。
        /// </summary>
        public RSAEncryptionManager() : 
            this(ConfigurationManager.AppSettings["PublicKeyFile"], ConfigurationManager.AppSettings["PrivateKeyFile"])
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="publicfile">读取公钥存放的文件名/</param>
        /// <param name="privatefile">读取私钥存放的文件名</param>
        public RSAEncryptionManager(string publicfile ,string privatefile)
        {
            if (!File.Exists(publicfile)|| !File.Exists(privatefile))
            {
                throw new FileNotFoundException("文件不存在！");
            }
            else
            {
                rsa = CreateRSAEncryptProvider(publicfile);
                Drsa = CreateRSAProvider(privatefile);
            }
        }

        /// <summary>
        /// 创建解密的RSACryptoServiceProvider。
        /// </summary>
        /// <param name="privateKeyFile">读取私钥存放的文件名。</param>
        /// <returns>用于解密的RSACryptoServiceProvider。</returns>
        public RSACryptoServiceProvider CreateRSAProvider(string privateKeyFile){
			RSAParameters parameters1;
			parameters1 = new RSAParameters();
			StreamReader reader1 = new StreamReader(privateKeyFile);
			XmlDocument document1 = new XmlDocument();
			document1.LoadXml(reader1.ReadToEnd());
			XmlElement element1 = (XmlElement) document1.SelectSingleNode("root");
			parameters1.Modulus = ReadChild(element1, "Modulus");
			parameters1.Exponent = ReadChild(element1, "Exponent");
			parameters1.D = ReadChild(element1, "D");
			parameters1.DP = ReadChild(element1, "DP");
			parameters1.DQ = ReadChild(element1, "DQ");
			parameters1.P = ReadChild(element1, "P");
			parameters1.Q = ReadChild(element1, "Q");
			parameters1.InverseQ = ReadChild(element1, "InverseQ");
			CspParameters parameters2 = new CspParameters();
			parameters2.Flags = CspProviderFlags.UseMachineKeyStore;
			RSACryptoServiceProvider provider1 = new RSACryptoServiceProvider(parameters2);
			provider1.ImportParameters(parameters1);
			return provider1;
		}

        /// <summary>
        /// 创建加密的 RSACryptoServiceProvider。
        /// </summary>
        /// <param name="publicKeyFile">读取公钥存放的文件名。</param>
        /// <returns>用于加密的RSACryptoServiceProvider。</returns>
		public RSACryptoServiceProvider CreateRSAEncryptProvider(string publicKeyFile){
			RSAParameters parameters1;
			parameters1 = new RSAParameters();
			StreamReader reader1 = new StreamReader(publicKeyFile);
			XmlDocument document1 = new XmlDocument();
			document1.LoadXml(reader1.ReadToEnd());
			XmlElement element1 = (XmlElement) document1.SelectSingleNode("root");
			parameters1.Modulus = ReadChild(element1, "Modulus");
			parameters1.Exponent = ReadChild(element1, "Exponent");
			CspParameters parameters2 = new CspParameters();
			parameters2.Flags = CspProviderFlags.UseMachineKeyStore;
			RSACryptoServiceProvider provider1 = new RSACryptoServiceProvider(parameters2);
			provider1.ImportParameters(parameters1);
			return provider1;
		}

        /// <summary>
        /// Creates the RSA deformatter.
        /// </summary>
        /// <param name="publicKeyFile">The public key file.</param>
        /// <returns>the RSA deformatter.</returns>
		public RSAPKCS1SignatureDeformatter CreateRSADeformatter(string publicKeyFile)
		{
			  RSAParameters parameters1;
			  parameters1 = new RSAParameters();
			  StreamReader reader1 = new StreamReader(publicKeyFile);
			  XmlDocument document1 = new XmlDocument();
			  document1.LoadXml(reader1.ReadToEnd());
			  XmlElement element1 = (XmlElement) document1.SelectSingleNode("root");
			  parameters1.Modulus = ReadChild(element1, "Modulus");
			  parameters1.Exponent = ReadChild(element1, "Exponent");
			  CspParameters parameters2 = new CspParameters();
			  parameters2.Flags = CspProviderFlags.UseMachineKeyStore;
			  RSACryptoServiceProvider provider1 = new RSACryptoServiceProvider(parameters2);
			  provider1.ImportParameters(parameters1);
			  RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(provider1);
			  deformatter.SetHashAlgorithm("SHA1");
			  return deformatter;
		}

        /// <summary>
        /// Creates the RSA formatter.
        /// </summary>
        /// <param name="privateKeyFile">The private key file.</param>
        /// <returns>the RSA formatter.</returns>
		public RSAPKCS1SignatureFormatter CreateRSAFormatter(string privateKeyFile)
		{
			  RSAParameters parameters1;
			  parameters1 = new RSAParameters();
			  StreamReader reader1 = new StreamReader(privateKeyFile);
			  XmlDocument document1 = new XmlDocument();
			  document1.LoadXml(reader1.ReadToEnd());
			  XmlElement element1 = (XmlElement) document1.SelectSingleNode("root");
			  parameters1.Modulus = ReadChild(element1, "Modulus");
			  parameters1.Exponent = ReadChild(element1, "Exponent");
			  parameters1.D = ReadChild(element1, "D");
			  parameters1.DP = ReadChild(element1, "DP");
			  parameters1.DQ = ReadChild(element1, "DQ");
			  parameters1.P = ReadChild(element1, "P");
			  parameters1.Q = ReadChild(element1, "Q");
			  parameters1.InverseQ = ReadChild(element1, "InverseQ");
			  CspParameters parameters2 = new CspParameters();
			  parameters2.Flags = CspProviderFlags.UseMachineKeyStore;
			  RSACryptoServiceProvider provider1 = new RSACryptoServiceProvider(parameters2);
			  provider1.ImportParameters(parameters1);
			  RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(provider1);
			  formatter.SetHashAlgorithm("SHA1");
			  return formatter;
		}

        /// <summary>
        /// 加密。
        /// </summary>
        /// <param name="text">需要加密的内容。</param>
        /// <returns>加密后的内容。</returns>
        public string Encrypt(string text)
        {
            byte[] data = new UnicodeEncoding().GetBytes(text);
            byte[] bytes = rsa.Encrypt(data, true);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 解密。
        /// </summary>
        /// <param name="text">需要解密的内容。</param>
        /// <returns>解密后的内容。</returns>
        public string Decrypt(string text)
        {
            byte[] data = Convert.FromBase64String(text);
            byte[] bytes = Drsa.Decrypt(data, true);
            string result = new UnicodeEncoding().GetString(bytes);
            return result;
        }

        private byte[] GetHashData(byte[] data)
        {
			HashAlgorithm algorithm1 = HashAlgorithm.Create("SHA1");
			return algorithm1.ComputeHash(data);
		}

        private byte[] GetHashData(Stream data)
        {
			HashAlgorithm algorithm1 = HashAlgorithm.Create("SHA1");
			return algorithm1.ComputeHash(data);
		}

        /// <summary>
        /// 读取子节点
        /// </summary>
        /// <param name="parent">父节点</param>
        /// <param name="name">节点的名称</param>
        /// <returns></returns>
		private byte[] ReadChild(XmlElement parent, string name)
		{
			  XmlElement element1 = (XmlElement) parent.SelectSingleNode(name);
			  return Convert.FromBase64String(element1.InnerText);
		}
    }
}
