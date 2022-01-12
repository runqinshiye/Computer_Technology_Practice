
using RunQinBusiness.Remoting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;

namespace WinFormClient
{
    ///<summary></summary>
    public class CentralConnection
    {
        public static byte[] EncryptionKey;
        public CentralConnection()
        {
            UTF8Encoding enc = new UTF8Encoding();
            EncryptionKey = enc.GetBytes("mQlEGebnokhGFEFV");
        }
        public static string Encrypt(string str, byte[] key)
        {
            //No need to check RemotingRole; no call to db.
            if (str == "")
            {
                return "";
            }
            byte[] ecryptBytes = Encoding.UTF8.GetBytes(str);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = null;
            Aes aes = new AesCryptoServiceProvider();
            aes.Key = key;
            aes.IV = new byte[16];
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            cs.Write(ecryptBytes, 0, ecryptBytes.Length);
            cs.FlushFinalBlock();
            byte[] encryptedBytes = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(encryptedBytes, 0, (int)ms.Length);
            cs.Dispose();
            ms.Dispose();
            if (aes != null)
            {
                aes.Clear();
            }
            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypt(string str, byte[] key)
        {
            if (str == "")
            {
                return "";
            }
            try
            {
                byte[] encrypted = Convert.FromBase64String(str);
                MemoryStream ms = null;
                CryptoStream cs = null;
                StreamReader sr = null;
                Aes aes = new AesCryptoServiceProvider();
                aes.Key = key;
                aes.IV = new byte[16];
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                ms = new MemoryStream(encrypted);
                cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                sr = new StreamReader(cs);
                string decrypted = sr.ReadToEnd();
                ms.Dispose();
                cs.Dispose();
                sr.Dispose();
                if (aes != null)
                {
                    aes.Clear();
                }
                return decrypted;
            }
            catch
            {
                return "";
            }
        }
        ///<summary></summary>
        private static string GenerateHash(string message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            HashAlgorithm algorithm = SHA1.Create();
            byte[] hashbytes = algorithm.ComputeHash(data);
            byte digit1;
            byte digit2;
            string char1;
            string char2;
            StringBuilder strHash = new StringBuilder();
            for (int i = 0; i < hashbytes.Length; i++)
            {
                if (hashbytes[i] == 0)
                {
                    digit1 = 0;
                    digit2 = 0;
                }
                else
                {
                    digit1 = (byte)Math.Floor((double)hashbytes[i] / 16d);
                    digit2 = (byte)(hashbytes[i] - (byte)(16 * digit1));
                }
                char1 = ByteToStr(digit1);
                char2 = ByteToStr(digit2);
                strHash.Append(char1);
                strHash.Append(char2);
            }
            return strHash.ToString();
        }

        ///<summary>The only valid input is a value between 0 and 15.  Text returned will be 1-9 or a-f.</summary>
        private static string ByteToStr(Byte byteVal)
        {
            switch (byteVal)
            {
                case 10:
                    return "a";
                case 11:
                    return "b";
                case 12:
                    return "c";
                case 13:
                    return "d";
                case 14:
                    return "e";
                case 15:
                    return "f";
                default:
                    return byteVal.ToString();
            }
        }
        public static void SetDatabaseConnectionInfo()
        {
            string connectionString = "";
            List<string> listAdminCompNames = new List<string>();
            bool useDynamicMode = false;
            bool allowAutoLogin = true;
            string xmlPath = System.IO.Path.Combine(Application.StartupPath, "RunQinServerConfig.xml");
            if (RemotingClient.RemotingRole == RemotingRole.ClientDirect)
            {
                xmlPath = System.IO.Path.Combine(Application.StartupPath, "RunQinLocalConfig.xml");
            }
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlPath);
                XPathNavigator Navigator = document.CreateNavigator();
                XPathNavigator nav;
                //Always look for these settings first in order to always preserve them correctly.
                nav = Navigator.SelectSingleNode("//AdminCompNames");
                if (nav != null)
                {
                    listAdminCompNames.Clear(); //this method gets called more than once
                    XPathNodeIterator navIterator = nav.SelectChildren(XPathNodeType.All);
                    for (int i = 0; i < navIterator.Count; i++)
                    {
                        navIterator.MoveNext();
                        listAdminCompNames.Add(navIterator.Current.Value);//Add this computer name to the list.
                    }
                }
                //See if there's a UseXWebTestGateway
                nav = Navigator.SelectSingleNode("//UseXWebTestGateway");
                //See if there's a AllowAutoLogin node
                nav = Navigator.SelectSingleNode("//AllowAutoLogin");
                if (nav != null && nav.Value.ToLower() == "false")
                {
                    //Node must be specifically set to false to change the allowAutoLogin bool.
                    allowAutoLogin = false;
                }
                //Database Type
                nav = Navigator.SelectSingleNode("//DatabaseType");
                //ConnectionString
                nav = Navigator.SelectSingleNode("//ConnectionString");
                if (nav != null)
                {
                    //If there is a ConnectionString, then use it.
                    connectionString = nav.Value;
                }
                //UseDynamicMode
                nav = Navigator.SelectSingleNode("//UseDynamicMode");
                if (nav != null)
                {
                    //If there is a node, take in its value
                    useDynamicMode = SinBool(nav.Value);
                }
                //See if there's a DatabaseConnection
                nav = Navigator.SelectSingleNode("//DatabaseConnection");
                if (nav != null)
                {
                    //If there is a DatabaseConnection, then use it.
                    string ServerName = nav.SelectSingleNode("ComputerName").Value;
                    string DatabaseName = nav.SelectSingleNode("Database").Value;
                    string MySqlUser = nav.SelectSingleNode("User").Value;
                    string MySqlPassword = nav.SelectSingleNode("Password").Value;
                    XPathNavigator encryptedPwdNode = nav.SelectSingleNode("MySQLPassHash");
                    //If the Password node is empty, but there is a value in the MySQLPassHash node, decrypt the node value and use that instead
                    string _decryptedPwd = Decrypt(encryptedPwdNode.Value, EncryptionKey);
                    if (MySqlPassword == ""
                        && encryptedPwdNode != null
                        && encryptedPwdNode.Value != "")
                    {
                        MySqlPassword = _decryptedPwd;
                    }
                    XPathNavigator noshownav = nav.SelectSingleNode("NoShowOnStartup");
                    RunQinBusiness.comm.SetDataBaseInfo(ServerName, DatabaseName, MySqlUser, MySqlPassword);
                }
                nav = Navigator.SelectSingleNode("//ServerConnection");
                if (nav != null)
                {
                    //If there is a ServerConnection, then use it.
                    string ServiceURI = nav.SelectSingleNode("URI").Value;
                    XPathNavigator ecwnav = nav.SelectSingleNode("UsingEcw");
                    XPathNavigator autoLoginNav = nav.SelectSingleNode("UsingAutoLogin");
                    if (autoLoginNav != null && autoLoginNav.Value == "True" && allowAutoLogin)
                    {

                    }
                    RemotingClient.ServerURI = ServiceURI;
                }
            }
            catch (Exception)
            {

            }

        }

        ///<summary></summary>
        public static bool SinBool(string myString)
        {
            if (myString == "" || myString == "0" || myString.ToLower() == "false")
            {
                return false;
            }
            return true;
        }
    }
}