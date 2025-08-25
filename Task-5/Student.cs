using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_5
{
    internal class Student
    {
        public int ID { get; }
        public string Name { get; }
        public string Email { get; }
        public string PhoneNumber { get; }

        static int id = 100;

        // course list
        public List<Course> courses = new List<Course>();
        Dictionary<Exam, decimal> ExamScores = new Dictionary<Exam, decimal>();

        public Student(string name, string email, string phonenumber)
        {
            Name = name;
            Email = email;
            PhoneNumber = phonenumber;
            ID = id;
            id++;
        }

        public void AddCourse(Course course)
        {
            courses.Add(course);
        }

        public decimal GetExamScores(Exam exam)
        {
            return ExamScores.GetValueOrDefault(exam);
        }

        public void AddExam(Exam exam) 
        {
            ExamScores.Add(exam, 0);
        }

        public void editExamScore(Exam exam, decimal result)
        {
            ExamScores[exam] = result;
        }

        public void StudentReport()
        {
            if (ExamScores.Count == 0)
            {
                Console.WriteLine($"Student Name: {Name}");
                foreach (var course in courses) 
                {
                    Console.WriteLine($"Course Name: {course.Title}");
                }
                Console.WriteLine("Student Has Not Done Any Exams Yet!");
            }
            else
            {
                foreach (var ex in ExamScores)
                {
                    Console.WriteLine($"Student Name: {Name}\n" +
                        $"Course Name: {ex.Key.CourseName}\n" +
                        $"Exam Type: {ex.Key.ExamType}\n" +
                        $"Exam Score: {ex.Value}\n" +
                        $"Status: {(ex.Value >= (ex.Key.CurrentMark/2) ? "Pass" : "Fail")}");
                }
            }
        }
    }
}
