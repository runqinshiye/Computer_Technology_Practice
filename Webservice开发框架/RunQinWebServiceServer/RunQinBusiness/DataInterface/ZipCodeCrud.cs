using DataConnectionBase;
using RunQinBusiness.Remoting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RunQinBusiness.DataInterface
{
    public class ZipCodeCrud
    {
        ///<summary>Gets one ZipCode object from the database using the primary key.  Returns null if not found.</summary>
        public static ZipCode SelectOne(long zipCodeNum)
        {
            string command = "SELECT * FROM zipcode "
                + "WHERE ZipCodeNum = " + SOut.Long(zipCodeNum);
            List<ZipCode> list = TableToList(Db.GetTable(command));
            if (list.Count == 0)
            {
                return null;
            }
            return list[0];
        }

        ///<summary>Gets one ZipCode object from the database using a query.</summary>
        public static ZipCode SelectOne(string command)
        {
            if (RemotingClient.RemotingRole == RemotingRole.ClientWeb)
            {
                throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n" + command);
            }
            List<ZipCode> list = TableToList(Db.GetTable(command));
            if (list.Count == 0)
            {
                return null;
            }
            return list[0];
        }

        ///<summary>Gets a list of ZipCode objects from the database using a query.</summary>
        public static List<ZipCode> SelectMany(string command)
        {
            if (RemotingClient.RemotingRole == RemotingRole.ClientWeb)
            {
                throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n" + command);
            }
            List<ZipCode> list = TableToList(Db.GetTable(command));
            return list;
        }

        ///<summary>Converts a DataTable to a list of objects.</summary>
        public static List<ZipCode> TableToList(DataTable table)
        {
            List<ZipCode> retVal = new List<ZipCode>();
            ZipCode zipCode;
            foreach (DataRow row in table.Rows)
            {
                zipCode = new ZipCode();
                zipCode.ZipCodeNum = SIn.Long(row["ZipCodeNum"].ToString());
                zipCode.ZipCodeDigits = SIn.String(row["ZipCodeDigits"].ToString());
                zipCode.City = SIn.String(row["City"].ToString());
                zipCode.State = SIn.String(row["State"].ToString());
                zipCode.IsFrequent = SIn.Bool(row["IsFrequent"].ToString());
                retVal.Add(zipCode);
            }
            return retVal;
        }

        ///<summary>Converts a list of ZipCode into a DataTable.</summary>
        public static DataTable ListToTable(List<ZipCode> listZipCodes, string tableName = "")
        {
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = "ZipCode";
            }
            DataTable table = new DataTable(tableName);
            table.Columns.Add("ZipCodeNum");
            table.Columns.Add("ZipCodeDigits");
            table.Columns.Add("City");
            table.Columns.Add("State");
            table.Columns.Add("IsFrequent");
            foreach (ZipCode zipCode in listZipCodes)
            {
                table.Rows.Add(new object[] {
                    SOut.Long  (zipCode.ZipCodeNum),
                                zipCode.ZipCodeDigits,
                                zipCode.City,
                                zipCode.State,
                    SOut.Bool  (zipCode.IsFrequent),
                });
            }
            return table;
        }
        ///<summary>Inserts one ZipCode into the database.  Provides option to use the existing priKey.</summary>
        public static long Insert(ZipCode zipCode)
        {
            string command = "INSERT INTO zipcode (";

            command += "ZipCodeDigits,City,State,IsFrequent) VALUES(";

            command +=
                 "'" + SOut.String(zipCode.ZipCodeDigits) + "',"
                + "'" + SOut.String(zipCode.City) + "',"
                + "'" + SOut.String(zipCode.State) + "',"
                + SOut.Bool(zipCode.IsFrequent) + ")";

            zipCode.ZipCodeNum = Db.NonQ(command, true, "ZipCodeNum", "zipCode");

            return zipCode.ZipCodeNum;
        }
        ///<summary>Updates one ZipCode in the database.</summary>
        public static void Update(ZipCode zipCode)
        {
            string command = "UPDATE zipcode SET "
                + "ZipCodeDigits= '" + SOut.String(zipCode.ZipCodeDigits) + "', "
                + "City         = '" + SOut.String(zipCode.City) + "', "
                + "State        = '" + SOut.String(zipCode.State) + "', "
                + "IsFrequent   =  " + SOut.Bool(zipCode.IsFrequent) + " "
                + "WHERE ZipCodeNum = " + SOut.Long(zipCode.ZipCodeNum);
            Db.NonQ(command);
        }

        ///<summary>Updates one ZipCode in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
        public static bool Update(ZipCode zipCode, ZipCode oldZipCode)
        {
            string command = "";
            if (zipCode.ZipCodeDigits != oldZipCode.ZipCodeDigits)
            {
                if (command != "") { command += ","; }
                command += "ZipCodeDigits = '" + SOut.String(zipCode.ZipCodeDigits) + "'";
            }
            if (zipCode.City != oldZipCode.City)
            {
                if (command != "") { command += ","; }
                command += "City = '" + SOut.String(zipCode.City) + "'";
            }
            if (zipCode.State != oldZipCode.State)
            {
                if (command != "") { command += ","; }
                command += "State = '" + SOut.String(zipCode.State) + "'";
            }
            if (zipCode.IsFrequent != oldZipCode.IsFrequent)
            {
                if (command != "") { command += ","; }
                command += "IsFrequent = " + SOut.Bool(zipCode.IsFrequent) + "";
            }
            if (command == "")
            {
                return false;
            }
            command = "UPDATE zipcode SET " + command
                + " WHERE ZipCodeNum = " + SOut.Long(zipCode.ZipCodeNum);
            Db.NonQ(command);
            return true;
        }

        ///<summary>Returns true if Update(ZipCode,ZipCode) would make changes to the database.
        ///Does not make any changes to the database and can be called before remoting role is checked.</summary>
        public static bool UpdateComparison(ZipCode zipCode, ZipCode oldZipCode)
        {
            if (zipCode.ZipCodeDigits != oldZipCode.ZipCodeDigits)
            {
                return true;
            }
            if (zipCode.City != oldZipCode.City)
            {
                return true;
            }
            if (zipCode.State != oldZipCode.State)
            {
                return true;
            }
            if (zipCode.IsFrequent != oldZipCode.IsFrequent)
            {
                return true;
            }
            return false;
        }

        ///<summary>Deletes one ZipCode from the database.</summary>
        public static void Delete(long zipCodeNum)
        {
            string command = "DELETE FROM zipcode "
                + "WHERE ZipCodeNum = " + SOut.Long(zipCodeNum);
            Db.NonQ(command);
        }

        ///<summary>Deletes many ZipCodes from the database.</summary>
        public static void DeleteMany(List<long> listZipCodeNums)
        {
            if (listZipCodeNums == null || listZipCodeNums.Count == 0)
            {
                return;
            }
            string command = "DELETE FROM zipcode "
                + "WHERE ZipCodeNum IN(" + string.Join(",", listZipCodeNums.Select(x => SOut.Long(x))) + ")";
            Db.NonQ(command);
        }

    }
}