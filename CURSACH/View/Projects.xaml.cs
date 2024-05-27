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
    /// Логика взаимодействия для Projects.xaml
    /// </summary>
    public partial class Projects : Window
    {
        int selectedProjectId = 1;
        public Projects()
        {
            InitializeComponent();
            WindowState = CurrentWindow.State;
            loadAllProjects();
            loadTasksByProjectId_Click(selectedProjectId);
        }


       // Вывод данных
       private void loadAllProjects()
        {
            List<ProjectsClass> projects = DatabaseManager.GetAllProjects();
            spAllProjects.Children.Clear();

            foreach (ProjectsClass project in projects)
            {
                Style buttonStyle = new Style(typeof(Button));
                if (project.ProjectId == selectedProjectId)
                {
                    buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC97F"))));
                    CurrentProjectName.Text = project.ProjectName.Trim();
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
                buttonStyle.Setters.Add(new Setter(FrameworkElement.MinHeightProperty, 60.0)); // Устанавливаем минимальную высоту в 50


                // Добавление Setter в стиль кнопок
                buttonStyle.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));


                // Создание объекта Border для задания радиуса скругления углов
                CornerRadius cornerRadius = new CornerRadius(30); // Устанавливаем радиус скругления углов

                // Создание объекта Setter для установки свойства CornerRadius
                Setter cornerRadiusSetter = new Setter(Border.CornerRadiusProperty, cornerRadius);

                // Добавление Setter в стиль кнопок
                buttonStyle.Setters.Add(cornerRadiusSetter);

                Button btnProject = new Button();
                btnProject.Content = project.ProjectName;
                btnProject.Style = buttonStyle;
                btnProject.Click += (sender, e) => loadTasksByProjectId_Click(project.ProjectId);
                spAllProjects.Children.Add(btnProject);
            }
        }

        private void loadTasksByProjectId_Click(int Id)
        {
            selectedProjectId = Id;
            loadAllProjects();
            List<Tasks> tasks = DatabaseManager.GetTasksByProjectId(Id);
            spTasksByProject.Children.Clear();

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
                spTasksByProject.Children.Add(btnTask);
            }
        }


        private void OpenTaskDetails(Tasks task)
        {
            // Создаем новое окно с подробной информацией о задаче
            TaskDetailsWindow taskDetailsWindow = new TaskDetailsWindow(task);
            taskDetailsWindow.ShowDialog(); // Отображаем окно как модальное

            loadTasksByProjectId_Click(selectedProjectId);
            // логика после закрытия окна

        }

        // добавления задач и проектоы
        private void btPlusTask_Click(object sender, RoutedEventArgs e)
        {
            TaskDetailsWindow taskDetailsWindow = new TaskDetailsWindow(selectedProjectId);
            taskDetailsWindow.ShowDialog(); // Отображаем окно как модальное
            loadTasksByProjectId_Click(selectedProjectId);


        }

        private void btnAddProject_Click(object sender, RoutedEventArgs e)
        {
            AddProjectWindow addProjectWindow = new AddProjectWindow();
            addProjectWindow.ShowDialog();
            loadAllProjects();

        }

        // Кнопки упраавления всего окна
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

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            ClientHome home = new ClientHome(CurrentUser.UserId);
            home.Show();
            this.Hide();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterWindow filterWindow = new FilterWindow(CurrentProjectName.Text);
            filterWindow.ShowDialog();

            int selectedProjectId = DatabaseManager.GtProjectIdByName(filterWindow.SelectedProject);
            int selectedUser = DatabaseManager.GetUserIdByName(filterWindow.SelectedUser);
            string selectedStatus = filterWindow.SelectedStatus;
            string selectedPriority = filterWindow.SelectedPriority;
            int daysUntilDeadline = filterWindow.DaysUntilDeadline;

            List<Tasks> tasks = DatabaseManager.GetFilteredTasks(selectedProjectId, selectedUser, selectedStatus, selectedPriority, daysUntilDeadline);
            spTasksByProject.Children.Clear();

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
                spTasksByProject.Children.Add(btnTask);
            }
        }
    }
}
