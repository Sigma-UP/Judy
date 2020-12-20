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
    /// Логика взаимодействия для Done.xaml
    /// </summary>
    public partial class Done : Page
    {
        public List<CustomTask> tasksDone = new List<CustomTask>();

        public Done()
        {
            InitializeComponent();
            CustomTask.load(tasksDone, path:"TasksDone.txt");

            task_list_done.ItemsSource = tasksDone;
        }
        private void task_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (task_list_done.SelectedIndex >= 0 && task_list_done.SelectedIndex < tasksDone.Count)
            {
                tbox_done_description.Text =
                    $"  Task: {tasksDone[task_list_done.SelectedIndex].Header}\r\n" +
                    $"  Date: {tasksDone[task_list_done.SelectedIndex].Date}\r\n" +
                    $"  Description: {tasksDone[task_list_done.SelectedIndex].Description}";
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
            CustomTask.delete(tasksDone, task_list_done.SelectedIndex, "TasksDone.txt");

            task_list_done.Items.Refresh();
        }
    }
}
