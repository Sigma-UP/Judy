using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System;

namespace TaskManager
{
    public partial class Manager : Page
    {
        public List<CustomTask> tasks = new List<CustomTask>();


        public Manager()
        {
            InitializeComponent();

            CustomTask.load(tasks);
            task_list.ItemsSource = tasks;

            tbox_upcoming.Text = CustomTask.upcoming(tasks);

            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => {
                tbox_upcoming.Text = CustomTask.upcoming(tasks);
            };
            timer.Start();
        }

        private void task_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (task_list.SelectedIndex >= 0 && task_list.SelectedIndex < tasks.Count)
            {
                tbox_description.Text =
                    $"  Task: {tasks[task_list.SelectedIndex].Header}\r\n" +
                    $"  Date: {tasks[task_list.SelectedIndex].Date}\r\n" +
                    $"  Time: {tasks[task_list.SelectedIndex].Time}\r\n" +
                    $"  Description: {tasks[task_list.SelectedIndex].Description}";
                btn_removeTask.IsEnabled = true;
                btn_taskDone.IsEnabled = true;
            }
            else
            {
                tbox_description.Text = "";
                btn_removeTask.IsEnabled = false;
                btn_taskDone.IsEnabled = false;

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
            }
        }

        private void btn_removeTask_Click(object sender, RoutedEventArgs e)
        {
            CustomTask.delete(tasks, task_list.SelectedIndex);

            task_list.Items.Refresh();
        }

        private void btn_taskDone_Click(object sender, RoutedEventArgs e)
        {
            tasks[task_list.SelectedIndex].save(path:"TasksDone.txt");
            CustomTask.delete(tasks, task_list.SelectedIndex);
            task_list.Items.Refresh();

        }
    }
}