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

namespace CURSACH.View
{
    /// <summary>
    /// Логика взаимодействия для ProfileMain.xaml
    /// </summary>
    public partial class ProfileMain : Window
    {
        public ProfileMain()
        {
            InitializeComponent();
            LoadNotifications();
        }

        private void LoadNotifications()
        {
            
            var notifications = DatabaseManager.GetUserNotifications();
            NotificationsDataGrid.ItemsSource = notifications;
        }

         
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUser.type == "Клиент")
            {
                ClientHome home = new ClientHome();
                home.Show();
                this.Hide();
            }
            else if (CurrentUser.type == "Мастер") 
            {
                MasterHome home = new MasterHome();
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
