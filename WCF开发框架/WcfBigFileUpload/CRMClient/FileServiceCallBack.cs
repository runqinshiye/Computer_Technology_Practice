using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMClient.Service;

namespace CRMClient
{
    public class FileServiceCallBack:IFileServiceCallback
    {
        //定义一个委托事件（+=注册）
        public event Action<long> ToFileSizeCallBack;

        #region 实现服务端的契约回调接口
        /// <summary>
        /// 回传传输的文件大小
        /// 实际的实现方法将会和委托关联（注册）
        /// </summary>
        /// <param name="length">文件传输了多少</param>
        public void ToFileSize(long length)
        {
            ToFileSizeCallBack(length);
        }
        #endregion

    }
}
