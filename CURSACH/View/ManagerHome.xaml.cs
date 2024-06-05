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
    /// Логика взаимодействия для ManagerHome.xaml
    /// </summary>
    public partial class ManagerHome : Window
    {
        public ManagerHome()
        {
            InitializeComponent();
            NumberOfRequests.Text = DatabaseManager.GetNumberOfDoneRequests().ToString();
           // MidTime.Text = DatabaseManager.GetMidTime().ToString();

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
        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileVip profile = new ProfileVip();
            profile.Show();
            this.Hide();
        }
        private void btnTeam_Click(object sender, RoutedEventArgs e)
        {
            Team team = new Team();
            team.Show();
            this.Hide();
        }
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            ManagerHome home = new ManagerHome();
            home.Show();
            this.Hide();
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string type = TypeInput.Text;
            NumberOfFilteredRequests.Text = DatabaseManager.GetNumberOfDoneRequestsByType(type).ToString();
            MidTimeOfFiltered.Text = DatabaseManager.GetMidTimeOfDoneRequestsByType(type).ToString();
        }
    }
}
