using DataConnectionBase;
using RunQinBusiness.Remoting;
using System.Reflection;

namespace RunQinBusiness.DataInterface
{
    ///<summary></summary>
    public class ZipCodes
    {

        ///<summary></summary>
        public static long Insert(ZipCode Cur)
        {
            if (RemotingClient.RemotingRole == RemotingRole.ClientWeb)
            {
                Cur.ZipCodeNum = Meth.GetLong(MethodBase.GetCurrentMethod(), Cur);
                return Cur.ZipCodeNum;
            }
            return ZipCodeCrud.Insert(Cur);
        }

        ///<summary></summary>
        public static void Update(ZipCode Cur)
        {
            if (RemotingClient.RemotingRole == RemotingRole.ClientWeb)
            {
                Meth.GetVoid(MethodBase.GetCurrentMethod(), Cur);
                return;
            }
            ZipCodeCrud.Update(Cur);
        }

        ///<summary></summary>
        public static void Delete(ZipCode Cur)
        {
            if (RemotingClient.RemotingRole == RemotingRole.ClientWeb)
            {
                Meth.GetVoid(MethodBase.GetCurrentMethod(), Cur);
                return;
            }
            string command = "DELETE from zipcode WHERE zipcodenum = '" + SOut.Long(Cur.ZipCodeNum) + "'";
            Db.NonQ(command);
        }


    }



}













