using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Restful.IService.DTOs.Outputs;

namespace Restful.IService.Interfaces
{
    /// <summary>
    /// 学生服务契约接口
    /// </summary>
    [ServiceContract(Namespace = "http://Restful.IService.Interfaces")]
    public interface IStudentContract
    {
        //命令

        #region # 创建学生 —— StudentInfo CreateStudent(string name, IEnumerable<string> addresses)
        /// <summary>
        /// 创建学生
        /// </summary>
        /// <param name="name">学生名称</param>
        /// <param name="addresses">地址集</param>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StudentInfo CreateStudent(string name, IEnumerable<string> addresses);
        #endregion

        #region # 修改学生 —— StudentInfo UpdateStudent(Student student)
        /// <summary>
        /// 修改学生
        /// </summary>
        /// <param name="student"></param>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        StudentInfo UpdateStudent(StudentInfo student);
        #endregion

        #region # 批量修改学生 —— IEnumerable<StudentInfo> UpdateStudents(IEnumerable<Student> students)
        /// <summary>
        /// 批量修改学生
        /// </summary>
        /// <param name="students"></param>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        IEnumerable<StudentInfo> UpdateStudents(IEnumerable<StudentInfo> students);
        #endregion


        //查询

        #region # 获取学生 —— StudentInfo GetStudent(int id)
        /// <summary>
        /// 获取学生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StudentInfo GetStudent(int id);
        #endregion

        #region # 获取学生列表 —— IEnumerable<StudentInfo> GetStudents(string keywords)
        /// <summary>
        /// 获取学生列表
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        IEnumerable<StudentInfo> GetStudents(string keywords);
        #endregion

        #region # 获取地址列表 —— IEnumerable<string> GetAddresses(IEnumerable<string> addresses)
        /// <summary>
        /// 获取地址列表
        /// </summary>
        /// <param name="addresses">地址列表</param>
        /// <returns>地址列表</returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        IEnumerable<string> GetAddresses(IEnumerable<string> addresses);
        #endregion

        #region # 测试异常 —— string GetException(string name)
        /// <summary>
        /// 测试异常
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string GetException(string name);
        #endregion

        #region # 获取字典 —— IDictionary<string, string> GetDictionary(...
        /// <summary>
        /// 获取字典
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        IDictionary<string, string> GetDictionary(IDictionary<string, string> dictionary);
        #endregion
    }
}
