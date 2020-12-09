using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        static string path = "Tasks.txt";
        private string header;
        private string description;
        private string date;

        public string Header { get => header; set => header = value;  }
        public string Description { get => description; set => description = value; }
        public string Date { get => date; set => date = value; }

        public void setDescription(string header, string description)
        {
            this.Header = header;
            this.Description = description;
        }
        public void setDate(string day = "NN", string month = "NN", string year = "NN", string hour = "NN", string min = "NN")
        {
            Date = $"{day}/{month}/{year}|{hour}:{min}";
        }

        public void save(bool append = true)
        {
            StreamWriter sw = new StreamWriter(path, append:append);

            sw.WriteLine($"[K]HEAD:\n{Header}");
            sw.WriteLine($"[K]DATE:\n{Date}");
            sw.WriteLine($"[K]DESC:\n{Description}");

            sw.Close();
        }
        public static void delete(List<Task> tasks, int i)
        {
            while (i < tasks.Count - 1)
                tasks[i] = tasks[++i];

            tasks.RemoveAt(tasks.Count - 1);

            FileStream fs = new FileStream(path, FileMode.Create);
            fs.Close();
            foreach (Task t in tasks)
                t.save();
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
                        task.Date = line;
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

                            if (task.Description != "" && task.Description!=null)
                                task.Description += "\r\n  ";

                            task.Description += line;

                            if ((line = sr.ReadLine()) == null)
                                break;
                        }
                        tasks.Add(task);
                        a = 0;
                    }
            }
        }
        public override string ToString() => Header;
    }
}
