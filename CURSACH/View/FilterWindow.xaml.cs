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
    /// Логика взаимодействия для FilterWindow.xaml
    /// </summary>
    public partial class FilterWindow : Window
    {

        public string SelectedProject { get; private set; }
        public string SelectedUser { get; private set; }
        public string SelectedStatus { get; private set; }
        public string SelectedPriority { get; private set; }
        public int DaysUntilDeadline { get; private set; }



        public FilterWindow(int userId)
        {
            InitializeComponent();

            // Получение списка проектов из базы данных
            List<ProjectsClass> projects = DatabaseManager.GetProjectsFromDatabase();

            // Добавление проектов в ComboBox
            foreach (ProjectsClass project in projects)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = project.ProjectName.Trim();
                item.Tag = project.ProjectId; // Используем Tag для хранения Id проекта
                cbProjectName.Items.Add(item);


            }
            ComboBoxItem itemAnyProject = new ComboBoxItem();
            itemAnyProject.Content = "Любой";
            cbProjectName.Items.Add(itemAnyProject);

            // Получение списка пользователей из базы данных
            List<Users> users = DatabaseManager.LoadAllUsers();

            // Добавление проектов в ComboBox
            foreach (Users user in users)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = user.Name.Trim();
                item.Tag = user.Id; // Используем Tag для хранения Id проекта
                if (user.Id == userId)
                {
                    item.IsSelected = true;
                }
                cbUsers.Items.Add(item);

            }
            ComboBoxItem itemAnyUser = new ComboBoxItem();
            itemAnyUser.Content = "Любой";
            cbUsers.Items.Add(itemAnyUser);



        }

        public FilterWindow(String projectName)
        {
            InitializeComponent();

            // Получение списка проектов из базы данных
            List<ProjectsClass> projects = DatabaseManager.GetProjectsFromDatabase();
            int selectedProjectId = DatabaseManager.GtProjectIdByName(projectName);

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

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {


            SelectedProject = (cbProjectName.SelectedItem as ComboBoxItem)?.Content?.ToString();
            SelectedUser = (cbUsers.SelectedItem as ComboBoxItem)?.Content?.ToString();
            SelectedStatus = (cbStatus.SelectedItem as ComboBoxItem)?.Content?.ToString();
            SelectedPriority = (cbPriority.SelectedItem as ComboBoxItem)?.Content?.ToString();

            string daysText = daysUntilDeadline.Text;
            if (string.IsNullOrWhiteSpace(daysText))
            {
                // Если строка пустая или содержит только пробелы, считаем, что пользователь не ввел данные
                DaysUntilDeadline = -1; // соответствует "нет ввода"
                Close(); // Закрыть окно
            }
            else
            {
                // Пытаемся преобразовать введенный текст в число
                if (int.TryParse(daysText, out int days))
                {
                    // Если удалось успешно преобразовать текст в число
                    DaysUntilDeadline = days;
                    Close(); // Закрыть окно или выполнить другие действия
                }
                else
                {
                    // Если введенный текст не удалось преобразовать в число
                    MessageBox.Show("Введите корректное количество дней до истечения срока выполнения задачи");
                }
            }
        }
    }
}
