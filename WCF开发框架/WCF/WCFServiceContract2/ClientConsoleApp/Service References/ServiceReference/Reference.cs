﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18408
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClientConsoleApp.ServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.ICompleteInstrumentation")]
    public interface ICompleteInstrumentation {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISimpleInstrumentation/WriteEventLog", ReplyAction="http://tempuri.org/ISimpleInstrumentation/WriteEventLogResponse")]
        string WriteEventLog();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISimpleInstrumentation/WriteEventLog", ReplyAction="http://tempuri.org/ISimpleInstrumentation/WriteEventLogResponse")]
        System.Threading.Tasks.Task<string> WriteEventLogAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICompleteInstrumentation/IncreatePerformanceCounter", ReplyAction="http://tempuri.org/ICompleteInstrumentation/IncreatePerformanceCounterResponse")]
        string IncreatePerformanceCounter();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICompleteInstrumentation/IncreatePerformanceCounter", ReplyAction="http://tempuri.org/ICompleteInstrumentation/IncreatePerformanceCounterResponse")]
        System.Threading.Tasks.Task<string> IncreatePerformanceCounterAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICompleteInstrumentationChannel : ClientConsoleApp.ServiceReference.ICompleteInstrumentation, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CompleteInstrumentationClient : System.ServiceModel.ClientBase<ClientConsoleApp.ServiceReference.ICompleteInstrumentation>, ClientConsoleApp.ServiceReference.ICompleteInstrumentation {
        
        public CompleteInstrumentationClient() {
        }
        
        public CompleteInstrumentationClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CompleteInstrumentationClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CompleteInstrumentationClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CompleteInstrumentationClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string WriteEventLog() {
            return base.Channel.WriteEventLog();
        }
        
        public System.Threading.Tasks.Task<string> WriteEventLogAsync() {
            return base.Channel.WriteEventLogAsync();
        }
        
        public string IncreatePerformanceCounter() {
            return base.Channel.IncreatePerformanceCounter();
        }
        
        public System.Threading.Tasks.Task<string> IncreatePerformanceCounterAsync() {
            return base.Channel.IncreatePerformanceCounterAsync();
        }
    }
}
