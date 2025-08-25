using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_5
{
    internal class MultipleChoice : Question
    {
        public string Answer { get; set; }

        public List<string> Options = new List<string>();

        public MultipleChoice(string title, decimal marks, string answer) : base (title, marks)
        {
            Answer = answer;
        }

        public override bool CheckAnswer(string answer)
        {
            return Answer.Equals(answer);
        }
    }
}
