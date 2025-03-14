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
    /// Логика взаимодействия для ClientHome.xaml
    /// </summary>
    public partial class ClientHome : Window
    {

        
        public ClientHome()
        {
            InitializeComponent();
            WindowState = CurrentWindow.State;

            LoadWaitingRequests();
            LoadInProcessRequests();
            LoadDoneRequests();

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
            ProfileMain profile = new ProfileMain();
            profile.Show();
            this.Hide();
        }

        // Загрузка данных из бд по столбцам
        private void LoadInProcessRequests()
        {
            List<Request> requests = DatabaseManager.GetInProcessRequestsByClient();
            InPocessRequests.Children.Clear();

            foreach (Request request in requests)
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

                Button btnRequest = new Button();
                btnRequest.Content = request.homeTechType + " " + request.problemDescryption;
                btnRequest.Style = buttonStyle;
                btnRequest.Click += (sender, e) => OpenDetails(request);
                InPocessRequests.Children.Add(btnRequest);
            }
        }

        private void LoadWaitingRequests()
        {
            List<Request> requests = DatabaseManager.GetWaitingRequestsByClient();
            WaitingRequests.Children.Clear();


            foreach (Request request in requests)
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




                Button btnRequest = new Button();
                btnRequest.Content = request.homeTechType + " " + request.problemDescryption;
                btnRequest.Style = buttonStyle;
                btnRequest.Click += (sender, e) => OpenDetails(request);
                WaitingRequests.Children.Add(btnRequest);
            }
        }

        private void LoadDoneRequests()
        {
            List<Request> requests = DatabaseManager.GetDoneRequestsByClient();
            DoneRequests.Children.Clear();


            foreach (Request request in requests)
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

                Button btnRequest = new Button();
                btnRequest.Content = request.homeTechType + " " + request.problemDescryption;
                btnRequest.Style = buttonStyle;
                btnRequest.Click += (sender, e) => OpenDetails(request);
                DoneRequests.Children.Add(btnRequest);
            }
        }

        private void OpenDetails(Request request)
        {
            // Создаем новое окно с подробной информацией о задаче
            DetailsWindow DetailsWindow = new DetailsWindow(request);
            DetailsWindow.ShowDialog(); // Отображаем окно как модальное
            LoadWaitingRequests();
            LoadInProcessRequests();
            LoadDoneRequests();
        }

        private void btnPlusRequest_Click(object sender, RoutedEventArgs e)
        {
            AddRequestCart addRequest = new AddRequestCart();
            addRequest.ShowDialog(); // Отображаем окно как модальное
            LoadWaitingRequests();
            LoadInProcessRequests();
            LoadDoneRequests();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string requestNumber = tbNumber.Text;
            if (int.TryParse(requestNumber, out int requestId))
            {
                Request request = DatabaseManager.GetRequestById(requestId);
                if (request != null)
                {
                    DetailsWindow DetailsWindow = new DetailsWindow(request);
                    DetailsWindow.ShowDialog(); // Отображаем окно как модальное
                    LoadWaitingRequests();
                    LoadInProcessRequests();
                    LoadDoneRequests();
                }
                else
                {
                    MessageBox.Show("Заявка не найдена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Введите корректный номер заявки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
