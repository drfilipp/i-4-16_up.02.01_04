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

namespace HealthCenter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BD bd = new BD();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(bd.Authorization(LoginAuth.Text, PasswordAuth.Password).ToString());
        }

        private void LoginAuth_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LoginAuth.Text == "Логин") LoginAuth.Clear();
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(bd.Registration(LoginReg.Text, PasswordReg.Password, (byte)BD.RoleID.Client, NumAb.Text, IdSpec.Text).ToString());
        }
    }
}
