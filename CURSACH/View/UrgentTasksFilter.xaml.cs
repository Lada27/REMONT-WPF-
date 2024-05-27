using CURSACH.View;
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

namespace CURSACH
{
    /// <summary>
    /// Логика взаимодействия для UrgentTasksFilter.xaml
    /// </summary>
    public partial class UrgentTasksFilter : Window
    {

        public UrgentTasksFilter()
        {
            InitializeComponent();
        }

        private void btnApplyDeadlineFilerData(object sender, RoutedEventArgs e)
        {
            // Сохраняем значение из TextBox в свойство DaysUntilDeadline
            Home.deadlineDisplaay = int.Parse(daysUntilDeadline.Text);

            // Устанавливаем результат диалога как "true" и закрываем окно
            DialogResult = true;
            Close();

        }
    }
}
