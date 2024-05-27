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
using CURSACH.Classes;
using CURSACH.View;



namespace CURSACH.View
{
    /// <summary>
    /// Логика взаимодействия для TaskDetailsWindow.xaml
    /// </summary>
    public partial class TaskDetailsWindow : Window
    {
        public Tasks Task { get; set; }

        string action = "изменить";
        public TaskDetailsWindow(int selectedProjectId = -1)
        {
            InitializeComponent();
            action = "добавить";

            btnDeleteTask.Visibility = Visibility.Collapsed;
            // Устанавливаем значение DatePicker dpDeadline
            dpDeadline.SelectedDate = DateTime.Now;


            // Получение списка проектов из базы данных
            List<ProjectsClass> projects = DatabaseManager.GetProjectsFromDatabase();

            // Добавление проектов в ComboBox
            foreach (ProjectsClass project in projects)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = project.ProjectName.Trim();
                item.Tag = project.ProjectId; // Используем Tag для хранения Id проекта
                if (project.ProjectId == selectedProjectId)
                {
                    item.IsSelected = true;
                }
                cbProjectName.Items.Add(item);

                
            }


            // Получение списка пользователей из базы данных
            List<Users> users = DatabaseManager.LoadAllUsers();

            // Добавление проектов в ComboBox
            foreach (Users user in users)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = user.Name.Trim();
                item.Tag = user.Id; // Используем Tag для хранения Id проекта
                cbUsers.Items.Add(item);


            }
        }

        public TaskDetailsWindow(Tasks task)
        {
            InitializeComponent();
            Task = task;
            TaskDescription.Text = task.Description;

            // Устанавливаем значение ComboBox cbPriority
            foreach (ComboBoxItem item in cbPriority.Items)
            {
                if (item.Content.ToString().Trim() == task.Priority.Trim())
                {
                    item.IsSelected = true;
                    break;
                }
            }

            // Устанавливаем значение ComboBox cbStatus
            foreach (ComboBoxItem item in cbStatus.Items)
            {
                if (item.Content.ToString().Trim() == task.Status.Trim())
                {
                    item.IsSelected = true;
                    break;
                }
            }

            tbSelectedDate.Text = task.Deadline.ToString("dd.MM.yyyy");

            // Устанавливаем значение DatePicker dpDeadline
            dpDeadline.SelectedDate = task.Deadline;
            // Отображаем дату в текстовом формате
            dpDeadline.Text = task.Deadline.ToString("dd.MM.yyyy");

            // Получение списка проектов из базы данных
            List<ProjectsClass> projects = DatabaseManager.GetProjectsFromDatabase();

            // Добавление проектов в ComboBox
            foreach (ProjectsClass project in projects)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = project.ProjectName.Trim();
                item.Tag = project.ProjectId; // Используем Tag для хранения Id проекта
                cbProjectName.Items.Add(item);

                // Установка текущего выбранного элемента, если его Id совпадает с Id проекта задачи
                if (project.ProjectId == task.ProjectId)
                {
                    item.IsSelected = true;
                }
            }

            // Получение списка проектов из базы данных
            List<Users> users = DatabaseManager.LoadAllUsers();

            // Добавление проектов в ComboBox
            foreach (Users user in users)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = user.Name.Trim();
                item.Tag = user.Id; // Используем Tag для хранения Id проекта
                cbUsers.Items.Add(item);

                // Установка текущего выбранного элемента, если его Id совпадает с Id проекта задачи
                if (user.Id == task.UserId)
                {
                    item.IsSelected = true;
                }
            }

            action = "изменить";
            DataContext = this; // Установите DataContext на текущий объект TaskDetailsWindow, чтобы обеспечить привязку данных
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

        

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            int taskId = Task.Id;
            // Выводим сообщение пользователю с запросом подтверждения удаления задачи
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить эту задачу?", "Удаление задачи", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Если пользователь подтвердил удаление, удаляем задачу из базы данных
            if (result == MessageBoxResult.Yes)
            {
                try
                {                   
                    DatabaseManager.DeleteTask(taskId);
                    MessageBox.Show("Задача удалена");
                    Close();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении задачи: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnCloseDetails_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (action == "изменить")
            {
                // Получение данных о задаче из пользовательского ввода
                string description = TaskDescription.Text;
                DateTime deadline = dpDeadline.SelectedDate ?? DateTime.Now; // Получаем выбранную дату или текущую дату
                string status = ((ComboBoxItem)cbStatus.SelectedItem)?.Content.ToString(); // Получаем выбранный статус из ComboBox
                string projectName = ((ComboBoxItem)cbProjectName.SelectedItem)?.Content.ToString(); // Получаем выбранный проект из ComboBox
                string priority = ((ComboBoxItem)cbPriority.SelectedItem)?.Content.ToString(); // Получаем выбранный приоритет из ComboBox
                string userName = ((ComboBoxItem)cbUsers.SelectedItem)?.Content.ToString();


                // Обновление свойств объекта задачи
                Task.Description = description;
                Task.Description = description;
                Task.Deadline = deadline;
                Task.Status = status;
                Task.Priority = priority;
                Task.ProjectId = DatabaseManager.GtProjectIdByName(projectName.Trim());
                Task.UserId =Convert.ToInt32(DatabaseManager.GetUserIdByName(userName.Trim()));

                // Проверка, действительно ли пользователь хочет изменить задачу
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите изменить задачу?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Выполнение SQL-запроса для обновления данных в базе данных
                    DatabaseManager.UpdateTask(Task); // Передаем обновленный объект задачи
                    MessageBox.Show("Задача успешно изменена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            if (action == "добавить")
            {
                Tasks taskToSave = new Tasks();
                // Получение данных о задаче из пользовательского ввода
                string description = TaskDescription.Text;
                DateTime deadline = dpDeadline.SelectedDate ?? DateTime.Now; // Получаем выбранную дату или текущую дату
                string status = ((ComboBoxItem)cbStatus.SelectedItem)?.Content.ToString(); // Получаем выбранный статус из ComboBox
                string projectName = ((ComboBoxItem)cbProjectName.SelectedItem)?.Content.ToString(); // Получаем выбранный проект из ComboBox
                string priority = ((ComboBoxItem)cbPriority.SelectedItem)?.Content.ToString(); // Получаем выбранный приоритет из ComboBox
                string userId = ((ComboBoxItem)cbUsers.SelectedItem)?.Content.ToString(); // Получаем выбранный приоритет из ComboBox


                // Обновление свойств объекта задачи
                taskToSave.Description = description;
                taskToSave.Deadline = deadline;
                taskToSave.Status = status;
                taskToSave.Priority = priority;
                taskToSave.ProjectId = DatabaseManager.GtProjectIdByName(projectName.Trim());
                taskToSave.UserId = DatabaseManager.GetUserIdByName(userId.Trim());

                // Проверка, действительно ли пользователь хочет изменить задачу
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите добавить задачу?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Выполнение SQL-запроса для обновления данных в базе данных
                    DatabaseManager.AddTask(taskToSave); // Передаем обновленный объект задачи
                    MessageBox.Show("Задача успешно добвлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DatePicker datePicker && datePicker.SelectedDate.HasValue)
            {
                DateTime selectedDate = datePicker.SelectedDate.Value;
                tbSelectedDate.Text = selectedDate.ToString("dd.MM.yyyy");
            }
        }

    }
}
