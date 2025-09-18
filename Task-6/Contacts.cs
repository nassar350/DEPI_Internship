using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_6
{
    internal class Contacts
    {
        Dictionary<string, string> _phones;

        public Contacts()
        {
            _phones = new Dictionary<string, string>();
        }

        public void add (string name, string number)
        {
            _phones[name] = number;
        }

        public void remove(string name) 
        {
            _phones.Remove(name);
        }

        public string search(string name)
        {
            if (_phones.ContainsKey(name))
            {
                return _phones[name];
            }
            else
            {
                return "Not Found";
            }
        }
    }
}
