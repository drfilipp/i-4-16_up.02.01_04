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

namespace HealthCenter
{
    /// <summary>
    /// Логика взаимодействия для Data.xaml
    /// </summary>
    public partial class Data : Window
    {
        public Data()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mn = new MainWindow(new string[] {TBLogin.Text, TBPassword.Text });
            Hide();
            mn.Show();
        }
    }
}
