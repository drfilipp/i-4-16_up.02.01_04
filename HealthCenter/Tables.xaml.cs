using System;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;

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
        DataGrid[] DGs;
        SqlCommand[] coms = new SqlCommand[11];
        public Tables(byte Role, BD bdt, Window window)
        {
            InitializeComponent();
            idRole = Role;
            AuthWin = window;
            bd = bdt;
            IdLabel.Content = String.Format("Уровень доступа: {0}", idRole == (byte)BD.RoleID.Admin ? "Администратор" :
                idRole == (byte)BD.RoleID.Specialist ? "Специалист" : idRole == (byte)BD.RoleID.Client ? "Клиент" : "Неизвестно");
            DGs = new DataGrid[]{ DGSpec, DGPreparation, DGProvider, DGVisit, DGNumAb, DGMedcard, DGTimeWorl, DGRole, DGAccounts, DGSpecial, DGZac };
            for (byte i = 0; i < DGs.Length; i++) coms[i] = new SqlCommand(BD.commandsView[i], bd.sql);
        }

        private void ShowTables()
        {
            for (byte i = 0; i < DGs.Length; i++)
            {
                DataTable tempdt = new DataTable();
                tempdt.Load((SqlDataReader)new SqlCommand(BD.commandsView[i], bd.sql).ExecuteReader());
                DGs[i].ItemsSource = tempdt.DefaultView;
            }
                    
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
        private void getData()
        {
            Action act = () =>
            {
                SqlDependency[] deps = new SqlDependency[11];
                SqlDependency.Start(bd.sql.ConnectionString);
                for (byte i = 0; i < deps.Length; i++)
                {
                    deps[i] = new SqlDependency(coms[i]);
                    deps[i].OnChange += new OnChangeEventHandler(onDataChanget);
                }
                 ShowTables();
            };
            Dispatcher.Invoke(act, System.Windows.Threading.DispatcherPriority.Background);
        }

        private void onDataChanget(object sender, SqlNotificationEventArgs e)
        {
            getData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getData();
        }
    }
}
