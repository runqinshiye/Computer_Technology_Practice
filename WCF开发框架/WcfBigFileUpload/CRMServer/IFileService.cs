using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CRMServer
{
    [ServiceContract(CallbackContract=typeof(ICallBack))]
    public interface IFileService
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void FileUpLoad(FileModel fileModel);

        /// <summary>
        /// 文件传输
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void FileTransfer(FileModel fileModel);

        /// <summary>
        /// 取消文件传输
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void FileStop(FileModel fileModel);
    }

    /// <summary>
    /// 回调契约接口，在客户端实现接口
    /// </summary>
    public interface ICallBack
    {
        /// <summary>
        /// 回调已经传输的文件的大小
        /// </summary>
        /// <param name="length"></param>
         [OperationContract(IsOneWay = true)]
        void ToFileSize(long length);
    }

}
