using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace BusinessEntity
{
    [DataContract]// 数据契约属性声明
    public class User
    {
        [DataMember(Name = "UserName")]//定义别名
        public string Name
        { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Email { get; set; }

        // 没有[DataMember]声明将不会序列化传送
        public string Mobile { get; set; } 

        public string Test { get; set; }
    }
}
