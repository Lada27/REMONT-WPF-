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

namespace CURSACH.View
{
    /// <summary>
    /// Логика взаимодействия для AddRequestCart.xaml
    /// </summary>
    public partial class AddRequestCart : Window
    {
        public AddRequestCart()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Собираем данные, введенные пользователем
                string description = Description.Text.Trim();
                string model = tbModel.Text.Trim();
                string type = tbType.Text.Trim();

                // Создаем новый объект Request
                Request newRequest = new Request
                {
                    problemDescryption = description,
                    homeTechModel = model,
                    homeTechType = type,
                    requestStatus = "Новая заявка", // Устанавливаем статус по умолчанию
                    startDate = DateTime.Now, // Устанавливаем текущее время как время добавления
                };

                // Вызываем метод для добавления заявки в базу данных
                DatabaseManager.AddRequest(newRequest);

                // Сообщаем пользователю об успешном добавлении заявки
                MessageBox.Show("Заявка успешно добавлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Закрываем окно после сохранения
                this.Close();
            }
            catch (Exception ex)
            {
                // Сообщаем пользователю об ошибке
                MessageBox.Show($"Ошибка при добавлении заявки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCloseDetails_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
