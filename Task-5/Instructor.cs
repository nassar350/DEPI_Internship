using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_5
{
    internal class Instructor
    {
        public int ID { get; }
        public string Name { get; }
        public string Specialization { get; }
        public string PhoneNumber { get; }

        static int id = 100;

        // course list
        List<Course> Courses = new List<Course>();

        public Instructor(string name, string specialization, string phonenumber)
        {
            Name = name;
            Specialization = specialization;
            PhoneNumber = phonenumber;
            ID = id;
            id++;
        }

        public void AddCourse(Course course)
        {
            Courses.Add(course);
        }
    }
}
