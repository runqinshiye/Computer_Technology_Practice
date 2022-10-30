using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;

namespace CRMServer
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“FileService”。
    public class FileService : IFileService
    {
        //保存很多个传输的文件，因为有的文件可能要多次才能传完
        public Dictionary<string ,FileStream> dic=new Dictionary<string,FileStream>();
        #region 实现IFileService接口

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="fileModel"></param>
        public void FileUpLoad(FileModel fileModel)
        {
            try
            {
                //写到e盘的files文件夹下
                FileStream filestream = new FileStream("e:/files/" + fileModel.FileName, FileMode.Create);
                dic.Add(fileModel.FileId, filestream);

            }
            catch (Exception)
            {
                throw;
            }
          
        }

        /// <summary>
        /// 文件传输
        /// </summary>
        /// <param name="fileModel"></param>
        public void FileTransfer(FileModel fileModel)
        {
            try
            {
                //添加到字典
                FileStream filestream = dic[fileModel.FileId];
                //写入到磁盘中
                filestream.Write(fileModel.FileBytes, 0, fileModel.FileCount);
                //写入成功，告诉客户端写入好了，回调
                //OperationContext:操作上下文，全局文件对象,获取当前客户端和服务器的交流通道
                ICallBack callback = OperationContext.Current.GetCallbackChannel<ICallBack>();
                callback.ToFileSize(filestream.Length);
                //假设10000kB,传了1000次，如果上传完了
                if (filestream.Length >= fileModel.FileSize)
                {
                    filestream.Close();
                    dic.Remove(fileModel.FileId);
                }
            }
            catch (Exception)
            {
               
            }
          
        }

        /// <summary>
        /// 文件关闭，取消传输
        /// </summary>
        /// <param name="fileModel"></param>
        public void FileStop(FileModel fileModel)
        {
            try
            {
                FileStream filestream = dic[fileModel.FileId];
                filestream.Close(); 
                dic.Remove(fileModel.FileId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
