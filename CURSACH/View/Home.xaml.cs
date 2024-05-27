﻿using CURSACH.Classes;
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
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class Home : Window
    {

        public static String statusDisplay = "В работе";
        public static int deadlineDisplaay = 300;
        public Home(int Id)
        {
            InitializeComponent();
            WindowState = CurrentWindow.State;

            LoadUrgentTasks(Id, deadlineDisplaay);
            LoadTasksByStatus(Id, statusDisplay);
            LoadAllTasks(Id);

            StatusDisplay.Text = statusDisplay;
            daysDisplay.Text = $"Срочные \n {deadlineDisplaay}";



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


        // Кнопки управления бокового меню
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

        private void btnLHome_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home(CurrentUser.UserId);
            home.Show();
            this.Hide();
        }


        // Загрузка данных из бд по столбцам

        private void LoadAllTasks(int UserId)
        {
            List<Tasks> tasks = DatabaseManager.GetAllTasksByUser(UserId);
            AllTasks.Children.Clear();

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
                AllTasks.Children.Add(btnTask);
            }
        }

        private void LoadUrgentTasks(int userId, int deadLine)
        {
            List<Tasks> tasks = DatabaseManager.GetUrgentTasksByUser(userId, deadLine);
            UrgentTasks.Children.Clear();


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
                UrgentTasks.Children.Add(btnTask);
            }
        }

        
        private void LoadTasksByStatus(int userId, string status)
        {
            List<Tasks> tasks = DatabaseManager.GetTasksByStatusAndUserId(userId, status);
            statusFilterTasks.Children.Clear();


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
                statusFilterTasks.Children.Add(btnTask);
            }
        }


        // Открытие окна с подробной информацией о задаче

        private void OpenTaskDetails(Tasks task)
        {
            // Создаем новое окно с подробной информацией о задаче
            TaskDetailsWindow taskDetailsWindow = new TaskDetailsWindow(task);
            taskDetailsWindow.ShowDialog(); // Отображаем окно как модальное
            int Id = CurrentUser.UserId;
            LoadUrgentTasks(Id, deadlineDisplaay);
            LoadTasksByStatus(Id, statusDisplay);
            LoadAllTasks(Id);

            StatusDisplay.Text = statusDisplay;
            daysDisplay.Text = $"Срочные \n {deadlineDisplaay}";
        }


        // Фильтрация задач по столбцам (статус\дедлайн)
        private void btnDeadlineFilter_Click(object sender, RoutedEventArgs e)
        {
            // Создаем окно для фильтрации задач
            UrgentTasksFilter urgentTasksFilterWindow = new UrgentTasksFilter();

            // Показываем окно фильтрации задач как модальное
            bool? result = urgentTasksFilterWindow.ShowDialog();

            // Если пользователь подтвердил фильтр, обновляем список задач
            if (result == true)
            {
                // Получаем выбранные фильтры из окна фильтрации задач
                int daysUntilDeadline = deadlineDisplaay;

                // Применяем фильтр к списку задач
                List<Tasks> filteredTasks = DatabaseManager.GetUrgentTasksByUser(CurrentUser.UserId, daysUntilDeadline);
                
                // Очищаем старые задачи в StackPanel
                UrgentTasks.Children.Clear();

                // Загружаем отфильтрованные задачи в StackPanel
                foreach (Tasks task in filteredTasks)
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
                    btnTask.Click += (send, ee) => OpenTaskDetails(task);
                    UrgentTasks.Children.Add(btnTask);
                
                }
            }
            StatusDisplay.Text = statusDisplay;
            daysDisplay.Text = $"Срочные \n {deadlineDisplaay}";
        }

        private void btnStatusFilter_Click(object sender, RoutedEventArgs e)
        {
            // Создаем окно для фильтрации задач
            StatusFilterWindow StatusFilterWindow = new StatusFilterWindow();

            // Показываем окно фильтрации задач как модальное
            bool? result = StatusFilterWindow.ShowDialog();

            // Если пользователь подтвердил фильтр, обновляем список задач
            if (result == true)
            {
                // Получаем выбранные фильтры из окна фильтрации задач
                string status = statusDisplay;

                // Применяем фильтр к списку задач 
                List<Tasks> filteredTasks = DatabaseManager.GetTasksByStatusAndUserId(CurrentUser.UserId, status);

                // Очищаем старые задачи в StackPanel
                statusFilterTasks.Children.Clear();

                // Загружаем отфильтрованные задачи в StackPanel
                foreach (Tasks task in filteredTasks)
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
                    btnTask.Click += (send, ee) => OpenTaskDetails(task);
                    statusFilterTasks.Children.Add(btnTask);

                }
            }
            
        }

        private void btPlusTask_Click(object sender, RoutedEventArgs e)
        {
            TaskDetailsWindow taskDetailsWindow = new TaskDetailsWindow();
            taskDetailsWindow.ShowDialog(); // Отображаем окно как модальное
            int Id = CurrentUser.UserId;
            LoadUrgentTasks(Id, deadlineDisplaay);
            LoadTasksByStatus(Id, statusDisplay);
            LoadAllTasks(Id);

            StatusDisplay.Text = statusDisplay;
            daysDisplay.Text = $"Срочные \n {deadlineDisplaay}";
        }
    }
}
