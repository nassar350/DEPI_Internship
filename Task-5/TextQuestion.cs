using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_5
{
    internal class TextQuestion : Question
    {
        public string Answer { get; set; }

        public TextQuestion(string title, decimal marks, string answer) : base(title, marks)
        {
            Answer = answer;
        }

        public override bool CheckAnswer(string answer)
        {
            if (Answer != answer) return false;
            else return true;
        }
    }
}
