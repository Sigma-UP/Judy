using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    class Task
    {
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
        }
        public string Date
        {
            get
            {
                return date;
            }
        }
        public void setDescription(string header, string description)
        {
            this.header = header;
            this.description = description;
        }
        public void setDate(int day, int month, int year, int time)
        {
            date = $"{day}/{month}/{year}|{time}";
        }


        public void save(int i)
        {
            StreamWriter sw = new StreamWriter(path);

            sw.WriteLine($"{i}_TaskName:\n{header}");
            sw.WriteLine($"{i}_TaskDate:\n{date}");
            sw.WriteLine($"{i}_TaskDescription:\n{description}");

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
                        while (line[0] != '[' && line[1] != 'K' && line[2] != ']')
                        {
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

        public void load(int i)
        {
            StreamReader sr = new StreamReader(path);

            string line, currentNum;
            
            while((line=sr.ReadLine()) != null)
            {
                int k = 0;
                currentNum = "";

                while (line[k] != '_')
                    currentNum += line[k];

                if (currentNum == i.ToString())
                {
                    int a = 0;
                    
                    while (a < 6)
                    {
                        line = sr.ReadLine();
                        if (a == 1)
                            header = line;
                        else if (a == 3)
                            date = line;
                        else if (a == 5)
                            description = line;
                        a++;
                    }
                    break;
                }
            }
        }
    }
}
