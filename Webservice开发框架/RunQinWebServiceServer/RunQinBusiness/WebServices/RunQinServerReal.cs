namespace RunQinBusiness.WebServices
{
    ///<summary>This is a helper class that allows the real RunQinServer.ServiceMain class implement IRunQinServer.
    ///This also gives us a place to add code in the future if we ever need to add anything to RunQinServer.ServiceMain.</summary>
    public class RunQinServerReal : RunQinBusiness.RunQinWebServiceServer.ServiceMain, IRunQinServer
    {
    }
}
