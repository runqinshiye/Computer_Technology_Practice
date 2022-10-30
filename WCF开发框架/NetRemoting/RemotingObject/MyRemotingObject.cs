using System;

namespace RemotingObject
{
    // 第一步：创建远程对象
    // 创建远程对象——必须继承MarshalByRefObject,该类支持对象的跨域边界访问
    public class MyRemotingObject :MarshalByRefObject
    {
        // 用来测试Tcp通道 
        public int AddForTcpTest(int a, int b)
        {
            return a + b;
        }

        // 用来测试Http通道
        public int MinusForHttpTest(int a, int b)
        {
            return a - b;
        }

        // 用来测试IPC通道
        public int MultipleForIPCTest(int a, int b)
        {
            return a * b;
        }
    }
}
