using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    public class Task
    {
        private List<Task> tasks = new List<Task>();

        static string path = "Tasks.txt";
        private string header;
        private string description;
        private string date;

        public string Header
        {
            get
            {
                return header;
            }
            set
            {
                header = value;
            }
        }
        public string Desc
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
        public string Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }

        public void setDescription(string header, string description)
        {
            this.header = header;
            this.description = description;
        }
        public void setDate(string day = "NN", string month = "NN", string year = "NN", string hour = "NN", string min = "NN")
        {
            date = $"{day}/{month}/{year}|{hour}:{min}";
        }

        public void save(int i)
        {
            StreamWriter sw = new StreamWriter(path, append:true);

            sw.WriteLine($"[K]HEAD:\n{header}");
            sw.WriteLine($"[K]DATE:\n{date}");
            sw.WriteLine($"[K]DESC:\n{description}");

            sw.Close();
        }
        public static void load(List<Task> tasks)
        {
            StreamReader sr = new StreamReader(path);

            string line;

            int a = 0;

            Task task = null;
            while ((line = sr.ReadLine()) != null)
            {
                if (!(line[0] == '[' && line[1] == 'K' && line[2] == ']'))
                    if (a == 0)
                    {
                        task = new Task();
                        task.Header = line;
                        a++;
                    }
                    else if (a == 1)
                    {
                        task.date = line;
                        a++;
                    }
                    else if (a == 2)
                    {
                        //you need to upgrade this shit
                        while (line != null)
                        {
                           if (line.Length >= 3)
                                if(line[0] == '[' && line[1] == 'K' && line[2] == ']')
                                    break;

                            if (task.description != "" && task.description!=null)
                                task.description += "\r\n  ";

                            task.description += line;

                            if ((line = sr.ReadLine()) == null)
                                break;
                        }
                        tasks.Add(task);
                        a = 0;
                    }
            }
        }
    }
}
