using System.Collections.Generic;
using System.IO;
using System;

namespace TaskManager
{
    public class CustomTask
    {

        public string Header { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

        public void setDescription(string header, string description)
        {
            this.Header = header;
            this.Description = description;
        }
        public void setDate(string day = "NN", string month = "NN",
            string year = "NN", string hour = "NN", string min = "NN")
        {
            Date = $"{day}/{month}/{year}";
            Time = $"{hour}:{min}";
        }

        public void save(string path = "Tasks.txt", bool append = true)
        {
            StreamWriter sw = new StreamWriter(path, append: append);

            sw.WriteLine($"[K]HEAD:\n{Header}");
            sw.WriteLine($"[K]DATE:\n{Date}");
            sw.WriteLine($"[K]TIME:\n{Time}");
            sw.WriteLine($"[K]DESC:\n{Description}");

            sw.Close();
        }
        public static void delete(List<CustomTask> tasks, int i, string path = "Tasks.txt")
        {
            while (i < tasks.Count - 1)
                tasks[i] = tasks[++i];

            tasks.RemoveAt(tasks.Count - 1);

            FileStream fs = new FileStream(path, FileMode.Create);
            fs.Close();
            foreach (CustomTask t in tasks)
                t.save(path);
        }
        public static void load(List<CustomTask> tasks, string path = "Tasks.txt")
        {
            StreamReader sr = new StreamReader(path);

            string line;

            int a = 0;

            CustomTask task = null;
            while ((line = sr.ReadLine()) != null)
            {
                if (!(line[0] == '[' && line[1] == 'K' && line[2] == ']'))
                    if (a == 0)
                    {
                        task = new CustomTask();
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
                        task.Time = line;
                        a++;
                    }
                    else if (a == 3)
                    {
                        //you need to upgrade this shit

                        //upd: get out 
                        while (line != null)
                        {
                            if (line.Length >= 3)
                                if (line[0] == '[' && line[1] == 'K' && line[2] == ']')
                                    break;

                            if (task.Description != "" && task.Description != null)
                                task.Description += "\r\n  ";

                            task.Description += line;

                            if ((line = sr.ReadLine()) == null)
                                break;
                        }
                        tasks.Add(task);
                        a = 0;
                    }
            }
            sr.Close();
        }

        //eсли текущая дата - время, иначе дата
        public override string ToString() {
            string td;

            if (Date == $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}")
                td = Time;
            else
                td = Date;

            return $"{td} | {Header}";
        }
        
        public static string upcoming(List<CustomTask> tasks)
        {
            string result = "";

            foreach(CustomTask task in tasks)
            {
                if (task.Date == $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}" ||
                     task.Date == $"{DateTime.Now.Day + 1}/{DateTime.Now.Month}/{DateTime.Now.Year}")
                    result += task.Header + " | ";
            }
            
            return result;
        }
    }
}
