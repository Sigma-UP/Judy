using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Notifications.Wpf;
using StringExtension;

namespace TaskManager{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<CustomTask> current_tasks = new List<CustomTask>();
        public List<CustomTask> finished_tasks = new List<CustomTask>();
        public MainWindow()
        {
            InitializeComponent();

            CustomTask.load(current_tasks);
            CustomTask.load(finished_tasks, path: "TasksDone.txt");

            task_list_current.ItemsSource = current_tasks;
            task_list_done.ItemsSource = finished_tasks;
            
            tbox_upcoming.Text = CustomTask.upcoming(current_tasks);

            var notificationManager = new NotificationManager();
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            int current_sec = DateTime.Now.Second;
            timer.Tick += (o, t) => {
                tbox_upcoming.Text = CustomTask.upcoming(current_tasks); // to show upcoming tasks
                foreach (CustomTask task in current_tasks)
                    {
                        if (task.Date == $"{DateTime.Now.Day.ToString().ToNN_Format()}/{DateTime.Now.Month.ToString().ToNN_Format()}/{DateTime.Now.Year}" &&
                             task.Time == $"{DateTime.Now.Hour.ToString().ToNN_Format()}:{DateTime.Now.Minute.ToString().ToNN_Format()}" && current_sec % 59 == 0)
                
                        notificationManager.Show(new NotificationContent
                        {
                            Title = task.Header,
                            Message = task.Description,
                            Type = NotificationType.Information
                        });
                
                    }
                tbox_currTime.Text = $"{DateTime.Now.Hour:00}:{DateTime.Now.Minute:00}:{DateTime.Now.Second:00}";
                tbox_currDate.Text = $"{DateTime.Now.DayOfWeek} | {DateTime.Now.Day:00}/{DateTime.Now.Month:00}/{DateTime.Now.Year}";
                current_sec++;
            };
            timer.Start();
        }


        #region Module - Current Tasks
        private void current_tasks_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (task_list_current.SelectedIndex >= 0 && task_list_current.SelectedIndex < current_tasks.Count)
            {
                tbox_description.Text =
                    $"  Task: {current_tasks[task_list_current.SelectedIndex].Header}\r\n" +
                    $"  Date: {current_tasks[task_list_current.SelectedIndex].Date}\r\n" +
                    $"  Time: {current_tasks[task_list_current.SelectedIndex].Time}\r\n" +
                    $"  Description: {current_tasks[task_list_current.SelectedIndex].Description}";
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
                current_tasks.Add(newTask.new_task);
                task_list_current.Items.Refresh();
            }
        }

        private void btn_removeTask_Click(object sender, RoutedEventArgs e)
        {
            CustomTask.delete(current_tasks, task_list_current.SelectedIndex);

            task_list_current.Items.Refresh();
        }

        private void btn_taskDone_Click(object sender, RoutedEventArgs e)
        {
            current_tasks[task_list_current.SelectedIndex].save(path: "TasksDone.txt");
            CustomTask.delete(current_tasks, task_list_current.SelectedIndex);
            task_list_current.Items.Refresh();

        }
        #endregion
        #region Module - Finished Tasks
        private void task_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (task_list_done.SelectedIndex >= 0 && task_list_done.SelectedIndex < finished_tasks.Count)
            {
                tbox_done_description.Text =
                    $"  Task: {finished_tasks[task_list_done.SelectedIndex].Header}\r\n" +
                    $"  Date: {finished_tasks[task_list_done.SelectedIndex].Date}\r\n" +
                    $"  Description: {finished_tasks[task_list_done.SelectedIndex].Description}";
                btn_done_removeTask.IsEnabled = true;
            }
            else
            {
                tbox_done_description.Text = "";
                btn_done_removeTask.IsEnabled = false;

            }
        }
        private void btn_done_removeTask_Click(object sender, RoutedEventArgs e)
        {
            CustomTask.delete(finished_tasks, task_list_done.SelectedIndex, "TasksDone.txt");

            task_list_done.Items.Refresh();
        }
        #endregion

        #region Menu Handlers
        private void btn_currTasks_Click(object sender, RoutedEventArgs e)
        {
            finished_tasks_module.Visibility = Visibility.Collapsed;
            current_tasks_module.Visibility = Visibility.Visible;
        }

        private void btn_finTasks_Click(object sender, RoutedEventArgs e)
        {
            current_tasks_module.Visibility = Visibility.Collapsed;
            finished_tasks_module.Visibility = Visibility.Visible;

        }
        #endregion
        #region Basic Window Handlers
        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void manipulation_bar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        #endregion
    }
}
