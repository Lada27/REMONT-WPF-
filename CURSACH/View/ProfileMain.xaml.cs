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
            WindowState = CurrentWindow.State;
            UserName.Text = CurrentUser.fio.Trim();
            UserEmail.Text = CurrentUser.login.Trim();
            UserPassword.Text = CurrentUser.password.Trim();
            UserPhone.Text = CurrentUser.phone.Trim();
            UserType.Text = CurrentUser.type.Trim();
        }

        // Rнопки упраавления всего окна
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

        private void LoadNotifications()
        {
            // Очистка панели перед добавлением новых уведомлений
            NotificationsPanel.Children.Clear();

            // Получение уведомлений
            var notifications = DatabaseManager.GetUserNotifications();

            // Добавление каждого уведомления в панель
            foreach (var notification in notifications)
            {
                var border = new Border
                {
                    BorderBrush = Brushes.LightGray,
                    BorderThickness = new Thickness(1),
                    Padding = new Thickness(5),
                    Margin = new Thickness(5)
                };

                var textBlock = new TextBlock
                {
                    Text = notification.message,
                    FontSize = 16,
                    TextWrapping = TextWrapping.Wrap
                };

                border.Child = textBlock;
                NotificationsPanel.Children.Add(border);
            }
        }




        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUser.type == "Заказчик")
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
