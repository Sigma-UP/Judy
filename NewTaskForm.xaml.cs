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
using System.Windows.Shapes;

using static System.Convert;
using static System.DateTime;

namespace TaskManager
{
    public partial class NewTaskForm : Window
    {
        public Task new_task = null;

        public NewTaskForm() 
        {
            InitializeComponent();
        }

        #region textbox_input_handlers
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            int maxChar = 2;

            if ((sender as TextBox).Name == tbox_dateYear.Name)
                maxChar = 4;
            if (!Int32.TryParse(e.Text, out val) || (sender as TextBox).Text.Length >= maxChar)
                e.Handled = true;
        }
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void tbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == null || (sender as TextBox).Text == "")
                if ((sender as TextBox).Name == tbox_dateDay.Name)
                    (sender as TextBox).Text = "DD";
                else if ((sender as TextBox).Name == tbox_dateMonth.Name)
                    (sender as TextBox).Text = "MM";
                else if ((sender as TextBox).Name == tbox_dateYear.Name)
                    (sender as TextBox).Text = "YY";
                else if ((sender as TextBox).Name == tbox_timeHour.Name)
                    (sender as TextBox).Text = "HH";
                else if ((sender as TextBox).Name == tbox_timeMinute.Name)
                    (sender as TextBox).Text = "MM";
            
        }
        private void tbox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Text = "";
        }
        #endregion
        #region button_handlers
        private void btn_done_Click(object sender, RoutedEventArgs e)
        {

            DateNormalize();

            //if (date_isOK && desc_isOK && time_isOK && head_isOK)
            //{
            //    new_task_isOK = true;
            //    Close();
            //}
        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            new_task = null;
            Close();
        }
        #endregion


        //нормализируй дату
        //настрой корректное сохранение

        private void DateNormalize()
        {
            int currDay   = ToInt32(Now.Day), 
                currMonth = ToInt32(Now.Month), 
                currYear  = ToInt32(Now.Year),
                currMin   = ToInt32(Now.Minute),
                currHour  = ToInt32(Now.Hour);

            NormalizeYear();
            NormalizeMonth();
            NormalizeDay();
           

            if (tbox_dateYear.Text.Length == 3)
                tbox_dateYear.Text = "2" + tbox_dateYear.Text;
            else if (tbox_dateYear.Text.Length == 2)
                tbox_dateYear.Text = "20" + tbox_dateYear.Text;
            else if (tbox_dateYear.Text.Length == 1)
                tbox_dateYear.Text = "202" + tbox_dateYear.Text;
        }

           // // для даты, большей за возможную
           // if (Convert.ToInt32(tbox_dateDay.Text) > DateTime.DaysInMonth(Convert.ToInt32(tbox_dateYear.Text), Convert.ToInt32(tbox_dateMonth.Text)))
           //     tbox_dateDay.Text = $"{DateTime.DaysInMonth(Convert.ToInt32(tbox_dateYear.Text), Convert.ToInt32(tbox_dateMonth.Text))}";
           // if (Convert.ToInt32(tbox_dateMonth.Text) > 12)
           //     tbox_dateYear.Text = "12";
        
        private void NormalizeYear()
        {
            if (ToInt32(tbox_dateYear.Text) < Now.Year)
                tbox_dateYear.Text = $"{Now.Year}";
        }
        
        private void NormalizeMonth()
        {
            if (ToInt32(tbox_dateMonth.Text) >= 1 && ToInt32(tbox_dateMonth.Text) <= 12)
            {
                if (ToInt32(tbox_dateMonth.Text) < Now.Month)
                    if (ToInt32(tbox_dateYear.Text) == Now.Year)
                        tbox_dateMonth.Text = $"{Now.Month}";

            }
            else
            {
                if (ToInt32(tbox_dateMonth.Text) >= 12)
                    tbox_dateMonth.Text = "12";
                else
                {
                    if (ToInt32(tbox_dateYear.Text) == Now.Year)
                        tbox_dateMonth.Text = $"{Now.Month}";
                    else
                        tbox_dateMonth.Text = "01";
                }
            }


        }

        private void NormalizeDay()
        {
            if (ToInt32(tbox_dateDay.Text) >= 1 &&
                ToInt32(tbox_dateDay.Text) <= DaysInMonth(ToInt32(tbox_dateYear.Text), ToInt32(tbox_dateMonth.Text)))
            {
                if (tbox_dateMonth.Text == $"{Now.Month}" &&
                    tbox_dateYear.Text == $"{Now.Year}" &&
                    ToInt32(tbox_dateDay.Text) < Now.Day)
                    tbox_dateDay.Text = $"{Now.Day}";
            }
            else
            {
                if (ToInt32(tbox_dateDay.Text) < 1)
                {
                    if (ToInt32(tbox_dateMonth.Text) == Now.Month)
                        tbox_dateDay.Text = $"{Now.Day}";
                    else
                        tbox_dateDay.Text = "01";
                }
                else
                    tbox_dateDay.Text = $"{DaysInMonth(ToInt32(tbox_dateYear.Text), ToInt32(tbox_dateMonth.Text))}";
            }
        }
    }
}