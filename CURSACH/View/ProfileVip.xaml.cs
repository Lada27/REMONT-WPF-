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
using System.Text.RegularExpressions;
using CURSACH.Classes;


namespace CURSACH.View
{
    /// <summary>
    /// Логика взаимодействия для ProfileVip.xaml
    /// </summary>
    public partial class ProfileVip : Window
    {
        public ProfileVip()
        {

            InitializeComponent();
            WindowState = CurrentWindow.State;
            UserName.Text = CurrentUser.fio.Trim();
            UserEmail.Text = CurrentUser.login.Trim();
            UserPassword.Text = CurrentUser.password.Trim();
            UserPhone.Text = CurrentUser.phone.Trim();

            if (CurrentUser.type == "Оператор")
            {
                btnTeam.Visibility = Visibility.Collapsed;
                imgEmployees.Visibility = Visibility.Collapsed;
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMaximaze_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                CurrentWindow.State = WindowState.Normal;

            }
            else
            {
                WindowState = WindowState.Maximized;
                CurrentWindow.State = WindowState.Maximized;

            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void btnTeam_Click(object sender, RoutedEventArgs e)
        {
            Team team = new Team();
            team.Show();
            this.Hide();
        }


        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUser.type == "Оператор")
            {
                OperHome home = new OperHome();
                home.Show();
                this.Hide();
            }
            else if (CurrentUser.type == "Менеджер")
            {
                ManagerHome home = new ManagerHome();
                home.Show();
                this.Hide();
            }
            
        }

    private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            string confirmationMessage = "Вы уверены, что хотите выйти из профиля?";

            MessageBoxResult result = MessageBox.Show(confirmationMessage, "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                CurrentUser.userId = 0;
                CurrentUser.fio = null;
                CurrentUser.password = null;
                CurrentUser.login = null;
                CurrentUser.phone = null;

                LoginView loginView = new LoginView();
                loginView.Show();
                this.Hide();
            }
        }
    }
}
