﻿using System;
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
        BD bd;

        public MainWindow(string[] str = null)
        {
            InitializeComponent();
            bd = new BD(str);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int temp;
            if ((temp = bd.Authorization(LoginAuth.Text, PasswordAuth.Password)) != -1)
                showTables(temp);
            else MessageBox.Show("Аккаунт не существует!");
        }

        private void showTables(int temp)
        {
            Tables tables = new Tables((byte)temp, bd, this);
            this.Hide();
            tables.Show();
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
