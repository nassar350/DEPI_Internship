using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_5
{
    internal class TrueOrFalse : Question
    {
        public string Answer { get; set; }

        public TrueOrFalse(string title, decimal marks, string answer) : base(title, marks)
        {
            Answer = answer;
        }

        public override bool CheckAnswer(string answer)
        {
            return Answer.Equals(answer);
        }
    }
}
