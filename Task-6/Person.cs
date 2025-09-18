using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_6
{
    internal class Person
    {
        public int Id { get; set; }
        public string fName { get; set; }
        public string? Mname { get; set; }
        public string LName { get; set; }
        public DateTime? DateofBirth { get; set; }

        public override string ToString()
        {
            string info = $"id: {Id}\n" +
                $"First name: {fName}";

            if (Mname != null) info += $"\nMiddle Name: {Mname}";

            info += $"\nLast name: {LName}";

            if (DateofBirth.HasValue) info += $"\nDate of Birth: {DateofBirth}";

            return info ;
        }
    }
}
