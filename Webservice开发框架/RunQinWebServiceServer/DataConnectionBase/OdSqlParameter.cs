using MySql.Data.MySqlClient;
using System;

namespace DataConnectionBase
{
    ///<summary>Hold parameter info in a database independent manner.</summary>
    public class OdSqlParameter
    {
        private string parameterName;
        private OdDbType dbType;
        private Object value;

        public OdDbType DbType
        {
            get { return dbType; }
            set { dbType = value; }
        }

        ///<summary>parameterName should not include the leading character such as @ or : . And DbHelper.ParamChar() should be used to determine the char in the query itself.</summary>
        public string ParameterName
        {
            get { return parameterName; }
            set { parameterName = value; }
        }

        public Object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        ///<summary>parameterName should not include the leading character such as @ or : . And DbHelper.ParamChar() should be used to determine the char in the query itself.</summary>
        public OdSqlParameter(string parameterName, OdDbType dbType, Object value)
        {
            this.parameterName = parameterName;
            this.dbType = dbType;
            this.value = value;
        }

        public MySqlDbType GetMySqlDbType()
        {
            switch (this.dbType)
            {
                case OdDbType.Text:
                    return MySqlDbType.MediumText;
                default:
                    throw new ApplicationException("Type not found");
            }
        }

        public MySqlParameter GetMySqlParameter()
        {
            MySqlParameter param = new MySqlParameter();
            param.ParameterName = DbHelper.ParamChar + this.parameterName;
            param.Value = Value;
            param.MySqlDbType = GetMySqlDbType();
            return param;
        }
    }
}
