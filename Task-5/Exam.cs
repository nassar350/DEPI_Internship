using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_5
{
    internal class Exam
    {
        public string CourseName { get; set; }
        public string ExamType { get; set; }
        public int Year { get; set; }
        public bool Started { get; set; }
        public decimal CurrentMark { get; set; }
        public int ID { get; }

        static int id = 1000;

        public List<Question> questions= new List<Question>();

        public Exam(string coursename, string examtype)
        {
            CourseName = coursename;
            ExamType = examtype;
            Year = DateTime.Now.Year;
            Started = false;
            CurrentMark = 0;
            ID = id;
            id++;
        }


        public void AddQuestion(Question question)
        {
            questions.Add(question);
        }
    }
}
