﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18408
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.HelloWorldServices {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.Learninghard.com", ConfigurationName="HelloWorldServices.HellworldService")]
    public interface HellworldService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Learninghard.com/HellworldService/GetHelloWorld", ReplyAction="http://www.Learninghard.com/HellworldService/GetHelloWorldResponse")]
        string GetHelloWorld();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Learninghard.com/HellworldService/GetHelloWorld", ReplyAction="http://www.Learninghard.com/HellworldService/GetHelloWorldResponse")]
        System.Threading.Tasks.Task<string> GetHelloWorldAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface HellworldServiceChannel : Client.HelloWorldServices.HellworldService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class HellworldServiceClient : System.ServiceModel.ClientBase<Client.HelloWorldServices.HellworldService>, Client.HelloWorldServices.HellworldService {
        
        public HellworldServiceClient() {
        }
        
        public HellworldServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public HellworldServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HellworldServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HellworldServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetHelloWorld() {
            return base.Channel.GetHelloWorld();
        }
        
        public System.Threading.Tasks.Task<string> GetHelloWorldAsync() {
            return base.Channel.GetHelloWorldAsync();
        }
    }
}
