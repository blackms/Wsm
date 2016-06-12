using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace testdashboardf.Behaviors
{
    public class ClearTextboxOnFocus : DependencyObject
    {
        public static readonly DependencyProperty ClearTextOnEnter = DependencyProperty.RegisterAttached(
       "ClearTextOnEnter",
       typeof(bool),
       typeof(ClearTextboxOnFocus), new PropertyMetadata(default(bool), PropertyChangedCallback));


        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var textBox = dependencyObject as TextBox;
            if (textBox != null)
            {
                if ((bool)dependencyPropertyChangedEventArgs.NewValue == true)
                {
                    textBox.GotFocus += textBox_GotFocus;
                    textBox.LostFocus += textBox_LeaveFocus;
                }
            }
        }

        private static void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                if (textBox.Text == "Search...")
                {
                    textBox.Text = "";
                }
            }
        }

        private static void textBox_LeaveFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = "Search...";
                }
            }
        }


        public static void SetMyClearOnFocusedIfTextEqualsSearch(DependencyObject element, bool value)
        {
            element.SetValue(ClearTextOnEnter, value);
        }

        public static bool GetMyClearOnFocusedIfTextEqualsSearch(DependencyObject element)
        {
            return (bool)element.GetValue(ClearTextOnEnter);
        }
    }
}
