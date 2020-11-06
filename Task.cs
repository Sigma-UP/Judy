using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    class Task
    {
        static private string path = "Tasks.txt";
        
        private struct TaskDescription { 
            private string header;
            private string description;

            public void setDescription(string header, string description)
            {
                this.header = header;
                this.description = description;
            }
        };
        
        private struct TaskDate {
            private int day;
            private int month;
            private int year;

            public void setDate(int day, int month, int year)
            {
                this.day = day;
                this.month = month;
                this.year = year;
            }
        };

        public void save()
        {

        }
    }
}
