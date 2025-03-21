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
using CURSACH.Classes;
using CURSACH.View;



namespace CURSACH.View
{
    /// <summary>
    /// Логика взаимодействия для DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        public Request Request { get; set; }


        public DetailsWindow(Request request)
        {
            InitializeComponent();
            Request = request;
            Description.Text = request.problemDescryption;
            tbModel.Text = request.homeTechModel;

            tbSelectedStartDate.Text = request.startDate.ToString("dd.MM.yyyy");
            // Устанавливаем значение DatePicker 
            dpStartDate.SelectedDate = request.startDate;
            // Отображаем дату в текстовом формате
            dpStartDate.Text = request.startDate.ToString("dd.MM.yyyy");

            
            if  (request.requestStatus == "В процессе выполнения" || request.requestStatus == "Новая заявка")
            {
                tbSelectedEndDate.Text = "Не закончено";
            }
            else
            {
                tbSelectedEndDate.Text = request.completionDate?.ToString("dd.MM.yyyy");
                // Устанавливаем значение DatePicker 
                dpEndDate.SelectedDate = request.completionDate;
                // Отображаем дату в текстовом формате
                dpEndDate.Text = request.completionDate?.ToString("dd.MM.yyyy");
            }


            // Устанавливаем значение ComboBox cbStatus
            foreach (ComboBoxItem item in cbStatus.Items)
            {
                if (item.Content.ToString().Trim() == request.requestStatus.Trim())
                {
                    item.IsSelected = true;
                    break;
                }
            }
                        

            // Получение списка мастеров из базы данных
            List<User> masters = DatabaseManager.GetAllMasters();

            // Добавление проектов в ComboBox
            foreach (User master in masters)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = master.fio.Trim();
                item.Tag = master.userID; // Используем Tag для хранения Id проекта
                cbMaster.Items.Add(item);

                // Установка текущего выбранного элемента, если его Id совпадает с Id проекта задачи
                if (master.userID == request.masterID)
                {
                    item.IsSelected = true;
                }
            }

            RepairDetails.Text = DatabaseManager.GetRequestById(request.requestID).repairParts;
            LoadComments(request.requestID);

            Phone.Text = DatabaseManager.GetUserById(request.clientID).phone;

            if (CurrentUser.type == "Заказчик")
            {
                btnSave.Visibility = Visibility.Collapsed;
                Description.IsReadOnly = true;
                tbModel.IsReadOnly = true;
                RepairDetails.IsReadOnly = true;
                dpEndDate.IsEnabled = false;
                dpStartDate.IsEnabled = false;
                cbMaster.IsEnabled = false;
                cbStatus.IsEnabled = false;
                btnAddComment.Visibility = Visibility.Collapsed;
                CommentText.Visibility = Visibility.Collapsed;
            }
            else if (CurrentUser.type == "Мастер")
            {
                btnSave.Visibility = Visibility.Visible;
                Description.IsReadOnly = true;
                tbModel.IsReadOnly = false;
                RepairDetails.IsReadOnly = false;
                dpEndDate.IsEnabled = true;
                dpStartDate.IsEnabled = false;
                cbMaster.IsEnabled = false;

            }
            else if (CurrentUser.type == "Оператор")
            {
                btnSave.Visibility = Visibility.Visible;
                Description.IsReadOnly = true;
                tbModel.IsReadOnly = true;
                RepairDetails.IsReadOnly = true;
                dpEndDate.IsEnabled = false;
                dpStartDate.IsEnabled = false;
                cbMaster.IsEnabled = true;
                cbStatus.IsEnabled = false;
                btnAddComment.Visibility = Visibility.Collapsed;
                CommentText.Visibility = Visibility.Collapsed;

            }
        }
        public void LoadComments(int requestId)
        {
            var comments = DatabaseManager.GetCommentsByRequestId(requestId);

            CommentsPanel.Children.Clear();

            foreach (var comment in comments)
            {
                var commentTextBlock = new TextBlock
                {
                    Text = comment.message,
                    FontSize = 16,
                    Margin = new Thickness(5),
                    TextWrapping = TextWrapping.Wrap
                };

                var commentBorder = new Border
                {
                    BorderBrush = Brushes.LightGray,
                    BorderThickness = new Thickness(1),
                    Padding = new Thickness(5),
                    Margin = new Thickness(5)
                };

                commentBorder.Child = commentTextBlock;

                CommentsPanel.Children.Add(commentBorder);
            }
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

        private void btnCloseDetails_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }




        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string description = Description.Text;
            DateTime endDate = dpEndDate.SelectedDate ?? DateTime.Now;
            string status = ((ComboBoxItem)cbStatus.SelectedItem)?.Content.ToString();
            int masterId = ((ComboBoxItem)cbMaster.SelectedItem)?.Tag as int? ?? -1;
            string model = tbModel.Text;
            string repairDetails = RepairDetails.Text;

            bool statusChanged = Request.requestStatus != status;

            Request.problemDescryption = description;
            Request.completionDate = endDate;
            Request.requestStatus = status;
            Request.masterID = masterId;
            Request.homeTechModel = model;
            Request.repairParts = repairDetails;

            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите изменить заявку?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
               

                if (statusChanged)
                {
                    string notificationMessage = $"Статус вашей заявки №{Request.requestID} был изменен на {status}";
                    DatabaseManager.AddNotification(Request.clientID, Request.requestID, notificationMessage);
                    if (status == "Готова к выдаче")
                    {
                        Request.completionDate = DateTime.Now;
                    }
                    else
                    {
                        Request.completionDate = null;
                    }
                }
                DatabaseManager.UpdateRequest(Request);
                MessageBox.Show("Заявка успешно изменена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();

            }
        }


        private void DatePicker_SelectedEndDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DatePicker datePicker && datePicker.SelectedDate.HasValue)
            {
                DateTime selectedDate = datePicker.SelectedDate.Value;
                tbSelectedEndDate.Text = selectedDate.ToString("dd.MM.yyyy");

            }
        }
        private void btnAddComment_Click(object sender, RoutedEventArgs e)
        {
            string commentText = CommentText.Text;

            // Проверка на пустое поле
            if (string.IsNullOrWhiteSpace(commentText))
            {
                MessageBox.Show("Пожалуйста, введите комментарий.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            
            // Добавление комментария в базу данных
            bool success = DatabaseManager.AddComment(Request.requestID, commentText);

            if (success)
            {
                MessageBox.Show("Комментарий успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                CommentText.Text = string.Empty; // Очистка текстового поля после добавления комментария
                LoadComments(Request.requestID); // Перезагрузка комментариев, если у вас есть метод для этого
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении комментария. Попробуйте снова.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
