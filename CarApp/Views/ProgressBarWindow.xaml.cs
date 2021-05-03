using CarApp.Data;
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

namespace CarApp.Views
{
    public partial class ProgressBarWindow : Window
    {
        public bool CancelTask { get; set; }
        public ProgressBarWindow()
        {
            InitializeComponent();
            CancelTask = false;
        }

        private async void ProgressRenderedAsync(object sender, EventArgs e)
        {
            await ProgressAsync();
        }
        async Task ProgressAsync()
        {
            for (int i = 0; i <= 100; i++)
            {
                await Task.Delay(10);
                ProgressBarElement.Value = i;
                if (CancelTask == true)
                    Close();
            }
            await Task.Delay(1000);
            BackToMenu();
            this.Close();
        }
        private void BackToMenu()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).Visibility = Visibility.Visible;
                }
            }
        }
        private void CancelTaskButton(object sender, RoutedEventArgs e)
        {
            CancelTask = true;
            if (ProgressBarElement.Value == 100)
            {
                BackToMenu();
                this.Close();
            }
        }
    }
}
