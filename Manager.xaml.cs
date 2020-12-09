using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class Manager : Page
    {
        public List<Task> tasks = new List<Task>();
    
        public Manager()
        {
            InitializeComponent();

            Task.load(tasks);
            task_list.ItemsSource = tasks;

            if (tasks.Count > 0)
                btn_removeTask.IsEnabled = true;
        }

        private void task_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
            tbox_description.Text = 
                $"  Task: {tasks[task_list.SelectedIndex].Header}\r\n" +
                $"  Date: {tasks[task_list.SelectedIndex].Date}\r\n" +
                $"  Description: {tasks[task_list.SelectedIndex].Description}";
            }
            catch (Exception)
            {
                task_list.SelectedIndex = 0;
            }
        }

        private void btn_addTask_Click(object sender, RoutedEventArgs e)
        {
            NewTaskForm newTask = new NewTaskForm();
            newTask.ShowDialog();
            if (newTask.new_task != null)
            {
                tasks.Add(newTask.new_task);
                task_list.Items.Refresh();
                btn_removeTask.IsEnabled = true;
            }
        }

        private void btn_removeTask_Click(object sender, RoutedEventArgs e)
        {
            Task.delete(tasks, task_list.SelectedIndex);

            task_list.Items.Refresh();

            if (tasks.Count == 0)
                btn_removeTask.IsEnabled = false;
        }
    }
}