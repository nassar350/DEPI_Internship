using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_6
{
    internal class WeeklySchedule
    {
        Dictionary<string, string> schedule = new Dictionary<string, string>();
        
        public string this[string day]
        {
            get
            {
                if (schedule.ContainsKey(day))
                {
                    return schedule[day];
                }
                else
                {
                    return "no schedule for today";
                }
            }
            set
            {
                schedule[day] = value;
            }
        }
    }
}
