using CodeBase;
using DataConnectionBase;
using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Web.Hosting;
using System.Xml;
using System.Xml.XPath;

namespace RunQinBusiness.Remoting
{
    public class DtoProcessor
    {
        
        ///<summary>Used to indicate whether the middle tier process has initialized.</summary>
        private static bool _isMiddleTierInitialized = false;

        ///<summary>Pass in a serialized dto.  It returns a dto which must be deserialized by the client.
        ///Set serverMapPath to the root directory of the RunQinServerConfig.xml.  Typically Server.MapPath(".") from a web service.
        ///Optional parameter because it is not necessary for Unit Tests (mock server).</summary>
        public static string ProcessDto(string dtoString, string serverMapPath = "")
        {
            try
            {
                RemotingClient.RemotingRole = RemotingRole.ServerWeb;

                #region Normalize DateTime
                if (!_isMiddleTierInitialized)
                {
                    //If this fails, the exception will throw and be serialized and sent to the client.
                    ODInitialize.Initialize();
                   _isMiddleTierInitialized = true;
                }
                #endregion
                #region Initialize Database Connection
                //Always attempt to set the database connection settings from the config file if they haven't been set yet.
                //We use to ONLY load in database settings when Security.LogInWeb was called but that is not good enough now that we have more services.
                //E.g. We do not want to manually call "Security.LogInWeb" from the CEMT when all we want is a single preference value.
                if (string.IsNullOrEmpty(DataConnection.GetServerName()) && string.IsNullOrEmpty(DataConnection.GetConnectionString()))
                {
                    //the application virtual path is usually /RunQinServer, but may be different if hosting multiple databases on one IIS server
                    string configFilePath = "";
                    if (!string.IsNullOrWhiteSpace(HostingEnvironment.ApplicationVirtualPath) && HostingEnvironment.ApplicationVirtualPath.Length > 1)
                    {
                        //There can be multiple config files within a physical path that is shared by multiple IIS ASP.NET applications.
                        //In order for the same physical path to host multiple applications, they each need a unique config file for db connection settings.
                        //Each application will have a unique ApplicationVirtualPath which we will use to identify the corresponding config.xml.
                        configFilePath = ODFileUtils.CombinePaths(serverMapPath, HostingEnvironment.ApplicationVirtualPath.Trim('/') + "Config.xml");
                    }
                    if (string.IsNullOrWhiteSpace(configFilePath) || !File.Exists(configFilePath))//returns false if the file doesn't exist, user doesn't have permission for file, path is blank or null
                    {
                        //either configFilePath not set or file doesn't exist, default to RunQinServerConfig.xml
                        configFilePath = ODFileUtils.CombinePaths(serverMapPath, "RunQinServerConfig.xml");
                    }
                    LoadDatabaseInfoFromFile(configFilePath);
                }
                #endregion
                DataTransferObject dto = DataTransferObject.Deserialize(dtoString);
                DtoInformation dtoInformation = new DtoInformation(dto);
                //Set Security.CurUser so that queries can be run against the db as if it were this user.
                CheckUserAndPassword(dto.Credentials.Username, dto.Credentials.Password);
                Type type = dto.GetType();
                #region DtoGetTable
                if (type == typeof(DtoGetTable))
                {
                    DataTable dt = (DataTable)dtoInformation.MethodInfo.Invoke(null, dtoInformation.ParamObjs);
                    return XmlConverter.TableToXml(dt);
                }
                #endregion
                #region DtoGetDS
                else if (type == typeof(DtoGetDS))
                {
                    DataSet ds = (DataSet)dtoInformation.MethodInfo.Invoke(null, dtoInformation.ParamObjs);
                    return XmlConverter.DsToXml(ds);
                }
                #endregion
                #region DtoGetSerializableDictionary
                else if (type == typeof(DtoGetSerializableDictionary))
                {
                    Object objResult = dtoInformation.MethodInfo.Invoke(null, dtoInformation.ParamObjs);
                    Type returnType = dtoInformation.MethodInfo.ReturnType;
                    return XmlConverterSerializer.Serialize(returnType, objResult);
                }
                #endregion
                #region DtoGetLong
                else if (type == typeof(DtoGetLong))
                {
                    long longResult = (long)dtoInformation.MethodInfo.Invoke(null, dtoInformation.ParamObjs);
                    return longResult.ToString();
                }
                #endregion
                #region DtoGetInt
                else if (type == typeof(DtoGetInt))
                {
                    int intResult = (int)dtoInformation.MethodInfo.Invoke(null, dtoInformation.ParamObjs);
                    return intResult.ToString();
                }
                #endregion
                #region DtoGetDouble
                else if (type == typeof(DtoGetDouble))
                {
                    double doubleResult = (double)dtoInformation.MethodInfo.Invoke(null, dtoInformation.ParamObjs);
                    return doubleResult.ToString();
                }
                #endregion
                #region DtoGetVoid
                else if (type == typeof(DtoGetVoid))
                {
                    dtoInformation.MethodInfo.Invoke(null, dtoInformation.ParamObjs);
                    return "0";
                }
                #endregion
                #region DtoGetObject
                else if (type == typeof(DtoGetObject))
                {
                    Object objResult = dtoInformation.MethodInfo.Invoke(null, dtoInformation.ParamObjs);
                    Type returnType = dtoInformation.MethodInfo.ReturnType;
                    if (returnType.IsInterface)
                    {
                        objResult = new DtoObject(objResult, objResult?.GetType() ?? returnType);
                        returnType = typeof(DtoObject);
                    }
                    return XmlConverterSerializer.Serialize(returnType, objResult);
                }
                #endregion
                #region DtoGetString
                else if (type == typeof(DtoGetString))
                {
                    string strResult = (string)dtoInformation.MethodInfo.Invoke(null, dtoInformation.ParamObjs);
                    return XmlConverter.XmlEscape(strResult);
                }
                #endregion
                #region DtoGetBool
                else if (type == typeof(DtoGetBool))
                {
                    bool boolResult = (bool)dtoInformation.MethodInfo.Invoke(null, dtoInformation.ParamObjs);
                    return boolResult.ToString();
                }
                #endregion
                else
                {
                    throw new NotSupportedException("Dto type not supported: " + type.FullName);
                }
            }
            catch (Exception e)
            {
                DtoException exception = new DtoException();
                if (e.InnerException == null)
                {
                    exception = GetDtoException(e);
                }
                else
                {
                    exception = GetDtoException(e.InnerException);
                }
                return exception.Serialize();
            }
        }

        //模拟入口验证
        public static Credentials CheckUserAndPassword(string username, string plaintext)
        {
            Credentials ins = new Credentials();
            ins.Username = username;
            ins.Password = plaintext;
            if (ins == null)
            {
                throw new ODException("Invalid username or password.", ODException.ErrorCodes.CheckUserAndPasswordFailed);
            }
            return ins;
        }

        public static void LoadDatabaseInfoFromFile(string configFilePath)
        {
            //No need to check RemotingRole; no call to db.
            if (!File.Exists(configFilePath))
            {
                throw new Exception("Could not find " + configFilePath + " on the web server.");
            }
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(configFilePath);
            }
            catch
            {
                throw new Exception("Web server " + configFilePath + " could not be opened or is in an invalid format.");
            }
            XPathNavigator Navigator = doc.CreateNavigator();
            //always picks the first database entry in the file:
            XPathNavigator navConn = Navigator.SelectSingleNode("//DatabaseConnection");//[Database='"+database+"']");
            if (navConn == null)
            {
                throw new Exception(configFilePath + " does not contain a valid database entry.");//database+" is not an allowed database.");
            }
            #region Verify ApplicationName Config File Value
            XPathNavigator configFileNode = navConn.SelectSingleNode("ApplicationName");//usually /OpenDentalServer
            if (configFileNode == null)
            {//when first updating, this node will not exist in the xml file, so just add it.
                try
                {
                    //AppendChild does not affect the position of the XPathNavigator; adds <ApplicationName>/OpenDentalServer<ApplicationName/> to the xml
                    using (XmlWriter writer = navConn.AppendChild())
                    {
                        writer.WriteElementString("ApplicationName", HostingEnvironment.ApplicationVirtualPath);
                    }
                    doc.Save(configFilePath);
                }
                catch { }//do nothing, unable to write to the XML file, move on anyway
            }
            else if (string.IsNullOrWhiteSpace(configFileNode.Value))
            {//empty node, add the Application Virtual Path
                try
                {
                    configFileNode.SetValue(HostingEnvironment.ApplicationVirtualPath);//sets value to /OpenDentalServer or whatever they named their app
                    doc.Save(configFilePath);
                }
                catch { }//do nothing, unable to write to the XML file, move on anyway
            }
            else if (configFileNode.Value.ToLower() != HostingEnvironment.ApplicationVirtualPath.ToLower())
            {
                //the xml node exists and this file already has an Application Virtual Path in it that does not match the name of the IIS attempting to access it
                string filePath = ODFileUtils.CombinePaths(Path.GetDirectoryName(configFilePath), HostingEnvironment.ApplicationVirtualPath.Trim('/') + "Config.xml");
                throw new Exception("Multiple middle tier servers are potentially trying to connect to the same database.\r\n"
                    + "This middle tier server cannot connect to the database within the config file found.\r\n"
                    + "This middle tier server should be using the following config file:\r\n\t" + filePath + "\r\n"
                    + "The config file is expecting an ApplicationName of:\r\n\t" + HostingEnvironment.ApplicationVirtualPath);
            }
            #endregion Verify ApplicationName Config File Value
            string connString = "", server = "", database = "", mysqlUser = "", mysqlPassword = "", mysqlUserLow = "", mysqlPasswordLow = "";
            XPathNavigator navConString = navConn.SelectSingleNode("ConnectionString");
            if (navConString != null)
            {//If there is a connection string then use it.
                connString = navConString.Value;
            }
            else
            {
                //return navOne.SelectSingleNode("summary").Value;
                //now, get the values for this connection
                server = navConn.SelectSingleNode("ComputerName").Value;
                database = navConn.SelectSingleNode("Database").Value;
                mysqlUser = navConn.SelectSingleNode("User").Value;
                mysqlPassword = navConn.SelectSingleNode("Password").Value;
                XPathNavigator encryptedPwdNode = navConn.SelectSingleNode("MySQLPassHash");
                string decryptedPwd;
                if (mysqlPassword == "" && encryptedPwdNode != null && encryptedPwdNode.Value != "" && CodeBase.ODDesProcess.Decrypt(encryptedPwdNode.Value, out decryptedPwd))
                {
                    mysqlPassword = decryptedPwd;
                }
                mysqlUserLow = navConn.SelectSingleNode("UserLow").Value;
                mysqlPasswordLow = navConn.SelectSingleNode("PasswordLow").Value;
            }
            XPathNavigator dbTypeNav = navConn.SelectSingleNode("DatabaseType");
            DatabaseType dbtype = DatabaseType.MySql;
            if (dbTypeNav != null)
            {
                if (dbTypeNav.Value == "Oracle")
                {
                    dbtype = DatabaseType.Oracle;
                }
            }
            DataConnection dcon = new DataConnection();
            if (connString != "")
            {
                try
                {
                    dcon.SetDb(connString, "", dbtype);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message + "\r\n" + "Connection to database failed.  Check the values in the config file on the web server " + configFilePath);
                }
            }
            else
            {
                try
                {
                    dcon.SetDb(server, database, mysqlUser, mysqlPassword, mysqlUserLow, mysqlPasswordLow, dbtype);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message + "\r\n" + "Connection to database failed.  Check the values in the config file on the web server " + configFilePath);
                }
            }
        }

        private static DtoException GetDtoException(Exception e)
        {
            DtoException dtoException = new DtoException();
            //The typical outter exception will be a TargetInvocationException due to how we process known DTO payloads.
            //Therefore, we need to get the InnerException which will be the actual exception that the method that was invoked threw.
            //E.g. we need to preserve the fact that an S class method threw an ODException and pass that along to the client workstation.
            dtoException.ExceptionType = e.GetType().Name;
            if (e.GetType() == typeof(ODException))
            {
                dtoException.ErrorCode = ((ODException)e).ErrorCode;
            }
            dtoException.Message = e.Message;
            return dtoException;
        }

        ///<summary>Contains all information about a recieved DTO.</summary>
        private class DtoInformation
        {
            ///<summary>The full name of the method include namespace, class, and method.</summary>
            public string[] FullNameComponents;
            ///<summary>The name of the assembly/namespace. Usually RunQinBusiness, but may be a plugin.</summary>
            public string AssemblyName;
            ///<summary>The name of the class that stores the method that will be invoked. If the namespace has a sub-namespace, such as 
            ///RunQinBusiness.Eclaims, the ClassName will include both the sub-namespace and the class such as Eclaims.Eclaims.</summary>
            public string ClassName;
            ///<summary>The name of the method that will be invoked.</summary>
            public string MethodName;
            ///<summary>The return type of the class that will be invoked.</summary>
            public Type ClassType;
            ///<summary>The parameters that are passed into the method.</summary>
            public DtoObject[] Parameters;
            ///<summary>The types for the given parameters.</summary>
            public Type[] ParamTypes;
            ///<summary>The information about the given method. Includes fields such as attributes and return types.</summary>
            public MethodInfo MethodInfo;
            ///<summary>The objects for the passed in paramaters.</summary>
            public object[] ParamObjs;

            public DtoInformation(DataTransferObject dto)
            {
                FullNameComponents = GetComponentsFromDtoMeth(dto.MethodName);
                AssemblyName = FullNameComponents[0];
                ClassName = FullNameComponents[1];
                MethodName = FullNameComponents[2];
                ClassType = null;
                ClassType = Type.GetType(AssemblyName + "." + ClassName + "," + AssemblyName);
                Parameters = dto.Params;
                ParamTypes = DtoObject.GenerateTypes(Parameters, AssemblyName);
                MethodInfo = ClassType.GetMethod(MethodName, ParamTypes);
                if (MethodInfo == null)
                {
                    throw new ApplicationException("Method not found with " + Parameters.Length.ToString() + " parameters: " + dto.MethodName);
                }
                ParamObjs = DtoObject.GenerateObjects(Parameters);
            }

            ///<summary>Only used if the dto is trying to call "Userods.HashPassword".
            ///This is so that passwords will be hashed on the server to utilize the server's MD5 hash algorithm instead of the workstation's algorithm.  
            ///This is due to the group policy security option "System cryptography: Use FIPS compliant algorithms for encryption,
            ///hashing and signing" that is enabled on workstations for some users but not on the server.  This allows those users to utilize the server's
            ///algorithm without requiring the workstations to have the algorithm at all.</summary>
            public string GetHashPassword()
            {
                string strResult = (string)MethodInfo.Invoke(null, ParamObjs);
                strResult = XmlConverter.XmlEscape(strResult);
                return strResult;
            }

            ///<summary>Helper function to handle full method name and turn it into 3 components.  The 3 components returned are:
            ///1. Assembly name
            ///2. Class name (however, this may contain the portion of the namespace after the assembly, 
            ///e.g. if "RunQinBusiness.Eclaims.Eclaims.GetMissingData" is passed in, this component will contain "Eclaims.Eclaims".)
            ///3. Method name</summary>
            private static string[] GetComponentsFromDtoMeth(string methodName)
            {
                if (methodName.Split('.').Length == 2)
                {
                    //Versions prior to 14.3 will send 2 components. 14.3 and above will send the assembly name RunQinBusiness or plugin assembly name.  
                    //If only 2 components are received, we will prepend RunQinBusiness so this will be backward compatible with versions prior to 14.3.
                    methodName = "RunQinBusiness." + methodName;
                }
                if (methodName.Split('.').Length <= 3)
                {
                    return methodName.Split('.');
                }
                //The method is in a namespace that contains multiple parts.
                int firstIdx = methodName.IndexOf('.');
                int lastIdx = methodName.LastIndexOf('.');
                return new string[] {
					//First part of namespace which should also be the assembly name
					methodName.Substring(0,firstIdx),
					//The rest of the namespace plus the class name
					methodName.Substring(firstIdx+1,lastIdx-firstIdx-1),
					//The method name
					methodName.Substring(lastIdx+1)
                };
            }
        }
    }
}
