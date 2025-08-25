using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_5
{
    internal class Course
    {
        public string Title { get; }
        public string Code { get; }
        public string Description { get; set; }
        public int MaxDegree { get; }
        public int Year { get; }
        public string InstructorName { get; private set; }

        // student list
        List<Student> Students = new List<Student>();

        public List<Exam> exams = new List<Exam>();

        public Course(string title, string code, string description, int maxdegree)
        {
            Title = title;
            Code = code;
            Description = description;
            MaxDegree = maxdegree;
            Year = DateTime.Now.Year;
        }

        public void setInstructorName(string instructorName)
        {
            InstructorName = instructorName;
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

        public void AddExam(Exam exam)
        {
            exams.Add(exam);
        }
    }
}
