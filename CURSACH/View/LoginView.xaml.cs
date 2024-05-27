using CURSACH.Classes;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CURSACH.View
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            WindowState = CurrentWindow.State;
            DatabaseHelper db = new DatabaseHelper();

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


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            String email = txtUser.Text;
            String password = txtPass.Password;
            Int32 UserId = DatabaseManager.AuthenticateUser(email, password);
            CurrentUser.UserId = UserId;
            if (UserId >= 0) 
            {

                Home home = new Home(CurrentUser.UserId);
                home.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Пользователь не найден");
            }
            
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUpView signUpVew = new SignUpView();
            signUpVew.Show();
            this.Hide();
        }
    }
}
