using System;
using System.IO;
using System.Windows;

namespace bower
{
    public partial class MainWindow : Window
    {
        private string saveFilePath = "last_visited_page.txt";

        public MainWindow()
        {
            InitializeComponent();
            LoadLastVisitedPage();
            Closing += MainWindow_Closing;
        }

        private void LoadLastVisitedPage()
        {
            if (File.Exists(saveFilePath))
            {
                string lastVisitedPage = File.ReadAllText(saveFilePath);
                webView.Source = new Uri(lastVisitedPage);
            }
            else
            {
                webView.Source = new Uri("https://www.google.com/");
            }
        }

        private void MainWindow_Closing(object sender, EventArgs e)
        {
            SaveLastVisitedPage();
        }

        private void SaveLastVisitedPage()
        {
            string lastVisitedPage = webView.Source?.AbsoluteUri;
            if (!string.IsNullOrEmpty(lastVisitedPage))
            {
                try
                {
                    File.WriteAllText(saveFilePath, lastVisitedPage);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении URL-адреса: {ex.Message}");
                }
            }
        }
        private void Nazad(object sender, RoutedEventArgs e)
        {
            webView.GoBack();
        }

        private void Vpered(object sender, RoutedEventArgs e)
        {
            webView.GoForward();
        }
        private void webView_NavigationStarting(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e)
        {
            // Получаем URL-адрес, на который пытается перейти браузер
            string targetUrl = e.Uri;

            // Проверяем, является ли URL-адрес целевой страницей (например, "https://example.com")
            if (!targetUrl.StartsWith("https://www.google.com/"))
            {
                // Блокируем навигацию на другие страницы
                e.Cancel = true;
            }
        }

    }
}
