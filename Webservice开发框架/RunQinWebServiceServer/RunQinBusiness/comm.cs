using DataConnectionBase;
namespace RunQinBusiness
{
    public class comm
    {
        public static void SetDataBaseInfo(string server, string db, string user, string password)
        {
            DataConnection dcon = new DataConnection();
            dcon.SetDb(server, db, user, password, "", "", DataConnection.DBtype);
        }
    }
}
