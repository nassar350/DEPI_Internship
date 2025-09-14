using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_6
{
    internal class Pair<T, U>
    {
        public T first { get; set; }
        public U second { get; set; }

        public Pair(T fvalue, U svalue)
        {
            first = fvalue;
            second = svalue;
        }

        public override string ToString() 
        {
            return ($"first: {first}, second: {second}");
        }
    }
}
