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
    /// Логика взаимодействия для Team.xaml
    /// </summary>
    public partial class Team : Window
    {
        int SelectedUserId = 2;
        public Team()
        {
            InitializeComponent();
            WindowState = CurrentWindow.State;
            loadAllMasters();
            LoadUserInfoById(SelectedUserId);

            if (CurrentWindow.State == WindowState.Maximized)
            {
                Icon.Width = 200;
                Icon.Height = 200;
                CornerRadius cornerRadius = new CornerRadius(80);
                Icon.CornerRadius = cornerRadius;

                UserName.FontSize = 30;
                UserEmail.FontSize = 30;
                UserPhone.FontSize = 30;

                
            }

        }

        private void loadAllMasters()
        {
            List<User> masters = DatabaseManager.GetAllMasters();
            spAllMaster.Children.Clear();

            foreach (User master in masters)
            {
                Style buttonStyle = new Style(typeof(Button));
                if (master.userID == SelectedUserId)
                {
                    buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC97F"))));
                }
                else
                {
                    buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2CAE5"))));
                }

                buttonStyle.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.Black));
                buttonStyle.Setters.Add(new Setter(Button.FontSizeProperty, 14.0));
                buttonStyle.Setters.Add(new Setter(Button.MarginProperty, new Thickness(5, 10, 5, 0)));
                buttonStyle.Setters.Add(new Setter(Border.BorderThicknessProperty, new Thickness(0)));
                buttonStyle.Setters.Add(new Setter(FrameworkElement.MinWidthProperty, 90.0)); // Устанавливаем минимальную ширину в 100
                buttonStyle.Setters.Add(new Setter(FrameworkElement.MinHeightProperty, 40.0)); // Устанавливаем минимальную высоту в 50


                // Добавление Setter в стиль кнопок
                buttonStyle.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));


                // Создание объекта Border для задания радиуса скругления углов
                CornerRadius cornerRadius = new CornerRadius(30); // Устанавливаем радиус скругления углов

                // Создание объекта Setter для установки свойства CornerRadius
                Setter cornerRadiusSetter = new Setter(Border.CornerRadiusProperty, cornerRadius);

                // Добавление Setter в стиль кнопок
                buttonStyle.Setters.Add(cornerRadiusSetter);

                Button btnUser = new Button();
                btnUser.Content = master.fio;
                btnUser.Style = buttonStyle;
                btnUser.Click += (sender, e) => LoadUserInfoById(master.userID);
                spAllMaster.Children.Add(btnUser);
            }
        }

        private void LoadUserInfoById(int UserId)
        {
            SelectedUserId = UserId;
            loadAllMasters();
            User user = DatabaseManager.GetUserById(UserId);
            FirstLetterOfTheName.Text = Convert.ToString(user.fio[0]);

            UserName.Text = user.fio;
            UserEmail.Text = user.login;
            UserPhone.Text = user.phone;

            CurrentRequests.Text = DatabaseManager.GetCurrentRequestsByMasterId(user.userID).ToString();
            DoneRequests.Text = DatabaseManager.GetDoneRequestsByMasterId(user.userID).ToString();
            MidTme.Text = DatabaseManager.GetMidTimeByMasterId(user.userID).ToString();
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
                Icon.Width = 70;
                Icon.Height = 70;
                CornerRadius cornerRadius = new CornerRadius(35);
                Icon.CornerRadius = cornerRadius;

                UserName.FontSize = 18;
                UserEmail.FontSize = 18;
                UserPhone.FontSize = 18;

                CurrentWindow.State = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
                Icon.Width = 200;
                Icon.Height = 200;
                CornerRadius cornerRadius = new CornerRadius(80);
                Icon.CornerRadius = cornerRadius;

                UserName.FontSize = 30;
                UserEmail.FontSize = 30;
                UserPhone.FontSize = 30;

                CurrentWindow.State = WindowState.Maximized;

            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileVip profile = new ProfileVip();
            profile.Show();
            this.Hide();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            ManagerHome home = new ManagerHome();
            home.Show();
            this.Hide();
        }
    }
    
    
}
