﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18408
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client3.ServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.Learninghard.com", ConfigurationName="ServiceReference.HellworldService")]
    public interface HellworldService {

        // 把自动生成的方法名GetHelloWorldWithoutParam修改成GetHelloWorld
        [System.ServiceModel.OperationContractAttribute(Name = "GetHelloWorldWithoutParam", Action = "http://www.Learninghard.com/HellworldService/GetHelloWorldWithoutParam", ReplyAction = "http://www.Learninghard.com/HellworldService/GetHelloWorldWithoutParamResponse")]
        string GetHelloWorld();

        // // 把自动生成的方法名GetHelloWorldWithoutParamAsync修改成GetHelloWorldAsync
        [System.ServiceModel.OperationContractAttribute(Name = "GetHelloWorldWithoutParam", Action="http://www.Learninghard.com/HellworldService/GetHelloWorldWithoutParam", ReplyAction="http://www.Learninghard.com/HellworldService/GetHelloWorldWithoutParamResponse")]
        System.Threading.Tasks.Task<string> GetHelloWorldAsync();
        
        [System.ServiceModel.OperationContractAttribute(Name = "GetHelloWorldWithParam", Action="http://www.Learninghard.com/HellworldService/GetHelloWorldWithParam", ReplyAction="http://www.Learninghard.com/HellworldService/GetHelloWorldWithParamResponse")]
        string GetHelloWorld(string name);

        [System.ServiceModel.OperationContractAttribute(Name = "GetHelloWorldWithParam", Action = "http://www.Learninghard.com/HellworldService/GetHelloWorldWithParam", ReplyAction = "http://www.Learninghard.com/HellworldService/GetHelloWorldWithParamResponse")]
        System.Threading.Tasks.Task<string> GetHelloWorldAsync(string name);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface HellworldServiceChannel : Client3.ServiceReference.HellworldService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class HellworldServiceClient : System.ServiceModel.ClientBase<Client3.ServiceReference.HellworldService>, Client3.ServiceReference.HellworldService {
        
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
        
        public string GetHelloWorld(string name) {
            return base.Channel.GetHelloWorld(name);
        }
        
        public System.Threading.Tasks.Task<string> GetHelloWorldAsync(string name) {
            return base.Channel.GetHelloWorldAsync(name);
        }
    }
}
