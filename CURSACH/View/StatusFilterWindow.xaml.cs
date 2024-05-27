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
    /// Логика взаимодействия для StatusFilterWindow.xaml
    /// </summary>
    public partial class StatusFilterWindow : Window
    {

        public StatusFilterWindow()
        {
            InitializeComponent();
        }

        private void btnApplyStatusFilerData(object sender, RoutedEventArgs e)
        {
            // Проверяем, что в ComboBox есть выбранный элемент
            if (SelectedStatus.SelectedItem != null)
            {
                // Получаем выбранный элемент и преобразуем его к типу ComboBoxItem
                ComboBoxItem selectedItem = SelectedStatus.SelectedItem as ComboBoxItem;

                // Получаем контент выбранного элемента (текст)
                string selectedStatus = selectedItem.Content.ToString();

                // Теперь вы можете использовать выбранный статус в вашем коде
                Home.statusDisplay = selectedStatus;

                // Устанавливаем результат диалога как "true" и закрываем окно
                DialogResult = true;
                Close();
            }
            else
            {
                // Если ничего не выбрано, вы можете обработать эту ситуацию, например, вывести сообщение пользователю
                MessageBox.Show("Выберите статус");
            }

        }
    }
}
