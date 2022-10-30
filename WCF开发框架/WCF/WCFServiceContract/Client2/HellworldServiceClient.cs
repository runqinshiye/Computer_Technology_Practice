using Contract;
using System.ServiceModel;
namespace Client2
{
    class HellworldServiceClient : ClientBase<IHelloWorld>, IHelloWorld
    {
        #region IHelloWorld Members
        public string GetHelloWorld()
        {
            return this.Channel.GetHelloWorld();
        }

        public string GetHelloWorld(string name)
        {
            return this.Channel.GetHelloWorld(name);
        }
        #endregion 
    }
}
