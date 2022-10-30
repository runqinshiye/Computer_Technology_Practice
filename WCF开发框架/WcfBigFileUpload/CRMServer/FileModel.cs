using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CRMServer
{
    [Serializable]//允许以流的文件进行传输
    [DataContract]//数据契约
    public  class FileModel
    {
        [DataMember]
        /// <summary>
        /// 文件标识
        /// </summary>
        public string FileId { get; set; }

        [DataMember]
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        [DataMember]
        /// <summary>
        /// 文件路径全名
        /// </summary>
        public string FullName { get; set; }

        [DataMember]
        /// <summary>
        /// 总文件大小，以字节为单位
        /// </summary>
        public long FileSize { get; set; }

        [DataMember]
        /// <summary>
        /// 每次传输的文件byte[]
        /// </summary>
        public byte[] FileBytes { get; set; }

        [DataMember]
        /// <summary>
        /// 每次传输的文件长度
        /// </summary>
        public int FileCount { get; set; }

        [DataMember]
        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExtName { get; set; }
    }
}
