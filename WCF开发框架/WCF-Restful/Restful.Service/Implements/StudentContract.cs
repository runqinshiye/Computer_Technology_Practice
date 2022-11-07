using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Restful.IService.DTOs.Outputs;
using Restful.IService.Interfaces;

namespace Restful.Service.Implements
{
    /// <summary>
    /// 学生服务契约实现
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class StudentContract : IStudentContract
    {
        //命令

        #region # 创建学生 —— StudentInfo CreateStudent(string name, IEnumerable<string> addresses)
        /// <summary>
        /// 创建学生
        /// </summary>
        /// <param name="name"></param>
        /// <param name="addresses"></param>
        public StudentInfo CreateStudent(string name, IEnumerable<string> addresses)
        {
            Console.WriteLine(name);
            Console.WriteLine(addresses.Count());

            StudentInfo student = new StudentInfo { Name = name, Addresses = addresses };

            return student;
        }
        #endregion

        #region # 修改学生 —— StudentInfo UpdateStudent(Student student)
        /// <summary>
        /// 修改学生
        /// </summary>
        /// <param name="student"></param>
        public StudentInfo UpdateStudent(StudentInfo student)
        {
            return student;
        }
        #endregion

        #region # 批量修改学生 —— IEnumerable<StudentInfo> UpdateStudents(IEnumerable<Student> students)
        /// <summary>
        /// 批量修改学生
        /// </summary>
        /// <param name="students"></param>
        public IEnumerable<StudentInfo> UpdateStudents(IEnumerable<StudentInfo> students)
        {
            return students;
        }
        #endregion


        //查询

        #region # 获取学生 —— StudentInfo GetStudent(int id)
        /// <summary>
        /// 获取学生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StudentInfo GetStudent(int id)
        {
            StudentInfo student = new StudentInfo { Id = 1, Name = "学生1", Addresses = new[] { "黑龙江", "吉林", "辽宁" } };

            return student;
        }
        #endregion

        #region # 获取学生列表 —— IEnumerable<StudentInfo> GetStudents(string keywords)
        /// <summary>
        /// 获取学生列表
        /// </summary>
        public IEnumerable<StudentInfo> GetStudents(string keywords)
        {
            IEnumerable<StudentInfo> students = new List<StudentInfo>
            {
                new StudentInfo{Id = 1,Name = "学生1",Addresses = new[]{"黑龙江","吉林","辽宁"}},
                new StudentInfo{Id = 2,Name = "学生2",Addresses = new[]{"黑龙江","吉林","辽宁"}},
                new StudentInfo{Id = 3,Name = "学生3",Addresses = new[]{"黑龙江","吉林","辽宁"}},
            };

            return students;
        }
        #endregion

        #region # 获取地址列表 —— IEnumerable<string> GetAddresses(IEnumerable<string> addresses)
        /// <summary>
        /// 获取地址列表
        /// </summary>
        /// <param name="addresses">地址列表</param>
        /// <returns>地址列表</returns>
        public IEnumerable<string> GetAddresses(IEnumerable<string> addresses)
        {
            return addresses;
        }
        #endregion

        #region # 测试异常 —— string GetException(string name)
        /// <summary>
        /// 测试异常
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetException(string name)
        {
            throw new ApplicationException("你看效果如何！");

            return "OK";
        }
        #endregion

        #region # 获取字典 —— IDictionary<string, string> GetDictionary(...
        /// <summary>
        /// 获取字典
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public IDictionary<string, string> GetDictionary(IDictionary<string, string> dictionary)
        {
            return dictionary;
        }
        #endregion
    }
}
