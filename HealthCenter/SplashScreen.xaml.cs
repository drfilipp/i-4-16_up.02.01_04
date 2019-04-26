using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using Microsoft.Win32;
using Microsoft.VisualBasic;

namespace HealthCenter
{
    /// <summary>
    /// Логика взаимодействия для SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RegistryKey RK = Registry.LocalMachine.OpenSubKey("SOFTWARE\\MICROSOFT\\Microsoft SQL Server");
            if (RK == null)
            {
                MessageBox.Show("Error", "Don't install sql server", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                for (int i = 0; i <= 100; i += 5)
                {
                    Thread.Sleep(1000);
                    bar.Value = i;
                }
                startMenu();
        }

        private void startMenu()
        {
            Data dt = new Data();
            this.Hide();
            dt.Show();
            //MainWindow mn = new MainWindow();
            //this.Hide();
            //mn.Show();
        }
    }
}
