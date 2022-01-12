using System;
using System.Security.Cryptography;
using System.Text;

namespace CodeBase
{
    public class ODDesProcess
    {
        public static string _privateKeyPwd = "12345678";
        public static bool Decrypt(string obfuscatedText, out string plainText)
        {
            plainText = obfuscatedText;
            try
            {
                plainText = Decrypt(obfuscatedText, _privateKeyPwd);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool Encrypt(string plainText, out string obfuscatedText)
        {
            obfuscatedText = plainText;
            try
            {
                obfuscatedText = Encrypt(plainText, _privateKeyPwd);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static string TryDecrypt(string obfuscatedText)
        {
            if (string.IsNullOrEmpty(obfuscatedText) || string.IsNullOrWhiteSpace(obfuscatedText))
            {
                return string.Empty;
            }
            return Decrypt(obfuscatedText, _privateKeyPwd);
        }
        public static string TryEncrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText) || string.IsNullOrWhiteSpace(plainText))
            {
                return string.Empty;
            }
            return Encrypt(plainText, _privateKeyPwd);
        }
        /// <summary>
        /// 进行DES加密
        /// </summary>
        /// <param name="pToEncrypt">要加密的字符串</param>
        /// <param name="sKey">密钥，且必须8位</param>
        /// <returns>以Base64格式返回的加密字符串</returns>
        public static string Encrypt(string pToEncrypt, string sKey)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Convert.ToBase64String(ms.ToArray());
                ms.Close();
                return str;
            }
        }
        /// <summary>
        /// 进行DES解密。
        /// </summary>
        /// <param name="pToDecrypt">要解密的以Base64</param>
        /// <param name="sKey">密钥，且必须为8位。</param>
        /// <returns>已解密的字符串。</returns>
        public static string Decrypt(string pToDecrypt, string sKey)
        {
            if ((pToDecrypt.Length % 4) != 0)//因为加密后是base64，所以用4来求余进行验证
            {
                return pToDecrypt;
            }
            if (pToDecrypt.Contains("Password"))//如果包含Password，表示没有加密
            {
                return pToDecrypt;
            }

            byte[] inputByteArray = Convert.FromBase64String(pToDecrypt);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
        }
    }
}
