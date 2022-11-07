using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using Restful.IService.DTOs.Outputs;
using Restful.IService.Interfaces;

namespace Restful.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            ChannelFactory<IStudentContract> studentContractFactory = new ChannelFactory<IStudentContract>(typeof(IStudentContract).FullName);

            IStudentContract studentContract = studentContractFactory.CreateChannel();

            IEnumerable<StudentInfo> students = studentContract.GetStudents(null);

            foreach (StudentInfo student in students)
            {
                Console.WriteLine(student.Name);
            }

            watch.Stop();

            Console.WriteLine(watch.Elapsed);
            Console.ReadKey();
        }
    }
}
