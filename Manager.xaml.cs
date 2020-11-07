using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskManager
{
    /// <summary>
    /// Логика взаимодействия для Manager.xaml
    /// </summary>
    public partial class Manager : Page
    {
        List<Task> tasks = new List<Task>();
        
        public Manager()
        {
            InitializeComponent();

            Task.load(tasks);

            foreach(Task task in tasks)
                task_list.Items.Add(task.Header);
        }

        private void task_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbox_description.Text = 
                $"  Task: {tasks[task_list.SelectedIndex].Header}\r\n" +
                $"  Date: {tasks[task_list.SelectedIndex].Date}\r\n" +
                $"  Description: {tasks[task_list.SelectedIndex].Desc}";
        }


    }
}
