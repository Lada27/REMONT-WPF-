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
        int SelectedUserId = CurrentUser.UserId;
        public Team()
        {
            InitializeComponent();
            WindowState = CurrentWindow.State;
            loadAllUsers();
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

       

        private void loadAllUsers()
        {

            List<Users> users = DatabaseManager.LoadAllUsers();
            spAllUsers.Children.Clear();

            foreach (Users user in users)
            {
                Style buttonStyle = new Style(typeof(Button));
                if (user.Id == SelectedUserId)
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
                btnUser.Content = user.Name;
                btnUser.Style = buttonStyle;
                btnUser.Click += (sender, e) => LoadUserInfoById(user.Id);
                spAllUsers.Children.Add(btnUser);
            }
        }

        private void LoadUserInfoById(int UserId)
        {
            SelectedUserId = UserId;
            loadAllUsers();
            Users user = DatabaseManager.GetUserById(UserId);
            FirstLetterOfTheName.Text = Convert.ToString(user.Name[0]);

            UserName.Text = user.Name;
            UserEmail.Text = user.Email;
            UserPhone.Text = user.Phone;

            List<Tasks> tasks = DatabaseManager.GetAllTasksByUser(UserId);
            spUserTasks.Children.Clear();

            foreach (Tasks task in tasks)
            {
                Style buttonStyle = new Style(typeof(Button));
                buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2CAE5"))));
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

                Button btnTask = new Button();
                btnTask.Content = task.Description;
                btnTask.Style = buttonStyle;
                btnTask.Click += (sender, e) => OpenTaskDetails(task);
                spUserTasks.Children.Add(btnTask);
            }
        }


        private void OpenTaskDetails(Tasks task)
        {
            // Создаем новое окно с подробной информацией о задаче
            DetailsWindow taskDetailsWindow = new DetailsWindow(task);
            taskDetailsWindow.ShowDialog(); // Отображаем окно как модальное

            loadAllUsers();
            LoadUserInfoById(SelectedUserId);
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

        private void btnTeam_Click(object sender, RoutedEventArgs e)
        {
            Team team = new Team();
            team.Show();
            this.Hide();
        }

        private void btnProjects_Click(object sender, RoutedEventArgs e)
        {
            Projects projects = new Projects();
            projects.Show();
            this.Hide();
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileVip profile = new ProfileVip();
            profile.Show();
            this.Hide();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            ClientHome home = new ClientHome(CurrentUser.UserId);
            home.Show();
            this.Hide();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterWindow filterWindow = new FilterWindow(SelectedUserId);
            filterWindow.ShowDialog();

            int selectedProjectId = DatabaseManager.GtProjectIdByName(filterWindow.SelectedProject);
            int selectedUser = DatabaseManager.GetUserIdByName(filterWindow.SelectedUser);
            string selectedStatus = filterWindow.SelectedStatus;
            string selectedPriority = filterWindow.SelectedPriority;
            int daysUntilDeadline = filterWindow.DaysUntilDeadline;

            List<Tasks> tasks = DatabaseManager.GetFilteredTasks(selectedProjectId, selectedUser, selectedStatus, selectedPriority, daysUntilDeadline);
            spUserTasks.Children.Clear();

            foreach (Tasks task in tasks)
            {
                Style buttonStyle = new Style(typeof(Button));

                buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2CAE5"))));


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


                Button btnTask = new Button();
                btnTask.Content = task.Description;
                btnTask.Style = buttonStyle;
                btnTask.Click += (se, ee) => OpenTaskDetails(task);
                spUserTasks.Children.Add(btnTask);
            }
        }


            private void btnAddTaskByUser_Click(object sender, RoutedEventArgs e)
            {
                DetailsWindow taskDetailsWindow = new DetailsWindow();
                taskDetailsWindow.ShowDialog(); // Отображаем окно как модальное
                
            }

       
    }
    
    
}
