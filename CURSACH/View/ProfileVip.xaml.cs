﻿using System;
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
using System.Text.RegularExpressions;
using CURSACH.Classes;


namespace CURSACH.View
{
    /// <summary>
    /// Логика взаимодействия для ProfileVip.xaml
    /// </summary>
    public partial class ProfileVip : Window
    {
        Users user;
        public ProfileVip()
        {

            InitializeComponent();
            WindowState = CurrentWindow.State;

            user = DatabaseManager.GetUserById(CurrentUser.UserId);

            UserName.Text = user.Name.Trim();
            UserEmail.Text = user.Email.Trim();
            UserPassword.Text = user.Password.Trim();
            UserPhone.Text = user.Phone.Trim();
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
            Home home = new Home(CurrentUser.UserId);
            home.Show();
            this.Hide();
        }

        private void btnApplyUserDataChane_Click(object sender, RoutedEventArgs e)
        {
            // Получаем новые значения из текстовых полей
            string newName = UserName.Text.Trim();
            string newEmail = UserEmail.Text.Trim();
            string newPassword = UserPassword.Text.Trim();
            string newPhone = UserPhone.Text.Trim();

            // Проверяем корректность введенных данных
            if (!IsValidEmail(newEmail))
            {
                MessageBox.Show("Введите корректный адрес электронной почты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsValidPassword(newPassword))
            {
                MessageBox.Show("Пароль должен содержать не менее 8 символов и включать как минимум одну букву и одну цифру.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsValidPhoneNumber(newPhone))
            {
                MessageBox.Show("Введите корректный номер телефона.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Формируем сообщение для подтверждения изменений
            string confirmationMessage = "Вы уверены, что хотите внести изменения в свой профиль?";
            MessageBoxResult result = MessageBox.Show(confirmationMessage, "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Если пользователь подтвердил изменения
            if (result == MessageBoxResult.Yes)
            {
                // Обновляем данные в базе данных
                if (DatabaseManager.UpdateUserProfile(CurrentUser.UserId, newName, newEmail, newPassword, newPhone))
                {
                    MessageBox.Show("Данные профиля успешно изменены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка при обновлении данных профиля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }



        // Метод для проверки корректности адреса электронной почты
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Проверка с использованием регулярного выражения
            var emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return emailRegex.IsMatch(email);
        }


        // Метод для проверки корректности пароля
        private bool IsValidPassword(string password)
    {
        // Пароль должен содержать не менее 8 символов и включать как минимум одну букву и одну цифру
        return !string.IsNullOrEmpty(password) && password.Length >= 8 && Regex.IsMatch(password, @"\d") && Regex.IsMatch(password, "[a-zA-Z]");
    }


    // Метод для проверки корректности номера телефона
    private bool IsValidPhoneNumber(string phoneNumber)
    {
        // Регулярное выражение для проверки номера телефона (допустимы форматы: +1234567890, 123-456-7890, 123.456.7890, 1234567890)
        string phonePattern = @"^\+?\d{0,2}?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$";

        // Проверяем соответствие введенного номера телефона регулярному выражению
        return Regex.IsMatch(phoneNumber, phonePattern);
    }



    private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser.UserId = 0;
            string confirmationMessage = "Вы уверены, что хотите выйти из профиля?";

            MessageBoxResult result = MessageBox.Show(confirmationMessage, "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                LoginView loginView = new LoginView();
                loginView.Show();
                this.Hide();
            }
        }
    }
}
