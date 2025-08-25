using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_5
{
    abstract class Question
    {
        public string QuestionTitle { get; set; }

        public decimal Marks { get; set; }

        protected Question(string title, decimal marks)
        {
            QuestionTitle = title;
            Marks = marks;
        }

        public abstract bool CheckAnswer(string answer);
    }
}
