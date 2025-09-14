using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Task_6
{
    internal class PhoneBook
    {
        Dictionary<string, string> _phones = new Dictionary<string, string>();

        public string this[string name]
        {
            get
            {
                if (_phones.ContainsKey(name))
                {
                 return _phones[name];
                }
                else
                {
                return "Number not found.";
                }
            }
            set
            {
               _phones[name] = value;
            }

        }
}
}
