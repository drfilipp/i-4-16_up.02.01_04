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
using System.Data;
using System.Data.SqlClient;

namespace HealthCenter
{
    /// <summary>
    /// Логика взаимодействия для Tables.xaml
    /// </summary>
    public partial class Tables : Window
    {
        byte idRole;
        BD bd;
        Window AuthWin;
        public Tables(byte Role, BD bdt, Window window)
        {
            InitializeComponent();
            idRole = Role;
            AuthWin = window;
            bd = bdt;
            ShowTables();
            IdLabel.Content = String.Format("Уровень доступа: {0}", idRole == (byte)BD.RoleID.Admin ? "Администратор" :
                idRole == (byte)BD.RoleID.Specialist ? "Специалист" : idRole == (byte)BD.RoleID.Client ? "Клиент" : "Неизвестно");
        }

        private void ShowTables()
        {
            DGSpec.ItemsSource = bd.GetFilledTable((byte)BD.Tables.Specialist).DefaultView;
            DGPreparation.ItemsSource = bd.GetFilledTable((byte)BD.Tables.Preparation).DefaultView;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            AuthWin.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Exit_Click(null, null);
        }
    }
}
