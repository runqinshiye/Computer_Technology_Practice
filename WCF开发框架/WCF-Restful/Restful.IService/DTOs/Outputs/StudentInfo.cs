using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Restful.IService.DTOs.Outputs
{
    /// <summary>
    /// 学生数据传输对象
    /// </summary>
    [DataContract(Namespace = "http://Restful.IService.DTOs.Outputs")]
    public class StudentInfo
    {
        public StudentInfo()
        {
            this.Addresses = new List<string>();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public IEnumerable<string> Addresses { get; set; }
    }
}