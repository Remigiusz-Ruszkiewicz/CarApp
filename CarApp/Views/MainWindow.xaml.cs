using System.IO;
using System.Windows;
using CarApp.Data;
using CarApp.Views;
using CarApp.Models;
using Microsoft.Data.Sqlite;
using System.Linq;


namespace CarApp
{
    public partial class MainWindow : Window
    {
        private readonly DataContext dataContext;
        Car ActiveCar = new Car();
        public MainWindow(DataContext dataContext)
        {
            this.dataContext = dataContext;
            InitializeComponent();
            GetCars();
            UpdateCarGrid.IsEnabled = false;
        }

        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            AddCarWindow addcarwindow = new AddCarWindow(dataContext);
            addcarwindow.Show();
        }
        public void GetCars()
        {
            CarDataGrid.ItemsSource = dataContext.Car.ToList();
        }

        private void UpdateCar(object s, RoutedEventArgs e)
        {
            dataContext.Update(ActiveCar);
            dataContext.SaveChanges();
            GetCars();
            ClearUpdateCarGrid();
            EditGrid.Visibility = Visibility.Hidden;
        }
        private void ClearUpdateCarGrid()
        {
            UpdateCarGrid.DataContext = null;
            UpdateCarGrid.IsEnabled = false;
        }
        private void SelectCarToEdit(object s, RoutedEventArgs e)
        {
            EditGrid.Visibility = Visibility.Visible;
            ActiveCar = (s as FrameworkElement).DataContext as Car;
            UpdateCarGrid.DataContext = ActiveCar;
            UpdateCarGrid.IsEnabled = true;
        }

        private void DeleteCar(object s, RoutedEventArgs e)
        {
            var productToDelete = (s as FrameworkElement).DataContext as Car;
            dataContext.Car.Remove(productToDelete);
            dataContext.SaveChanges();
            GetCars();
        }

        private void AddCarWindow(object sender, RoutedEventArgs e)
        {
            AddCarWindow addCarWindow = new AddCarWindow(dataContext);
            addCarWindow.Show();
            Close();
        }

        private void OpenProgressBarButton(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            ProgressBarWindow progresBarWindow = new ProgressBarWindow();
            progresBarWindow.Show();
        }

        private void RefreshGridButton(object sender, RoutedEventArgs e)
        {
            GetCars();
            EditGrid.Visibility = Visibility.Hidden;
        }
        private void PrintGridButton(object sender, RoutedEventArgs e)
        {
            new Print(dataContext).StartPrint();
        }

    }
}
