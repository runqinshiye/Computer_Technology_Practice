﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.6421
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCF.Demo.Service
{

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IRemove")]
    public interface IRemove
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IRemove/DoWork", ReplyAction = "http://tempuri.org/IRemove/DoWorkResponse")]
        bool DoWork(int UserID);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IRemoveChannel : IRemove, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class RemoveClient : System.ServiceModel.ClientBase<IRemove>, IRemove
    {

        public RemoveClient()
        {
        }

        public RemoveClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public RemoveClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public RemoveClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public RemoveClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public bool DoWork(int UserID)
        {
            return base.Channel.DoWork(UserID);
        }
    }
}