using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Linq;

namespace bower
{
    public partial class MainWindow : Window
    {
        private List<string> history = new List<string>();
        private string historyFilePath = "browser_history.txt";
        private int historyIndex = -1; // Индекс текущей страницы в истории
        private bool isInHistoryMode = true; // Флаг режима просмотра истории

        public MainWindow()
        {
            InitializeComponent();
            LoadLastVisitedPage();
            Closing += MainWindow_Closing;
        }

        private void LoadLastVisitedPage()
        {
            if (File.Exists(historyFilePath))
            {
                string[] savedHistory = File.ReadAllLines(historyFilePath);
                history.AddRange(savedHistory);
                if (history.Count > 0)
                {
                    historyIndex = history.Count - 1;
                    webView.Source = new Uri(history[historyIndex]);
                }
                else
                {
                    webView.Source = new Uri("https://kaspi.kz/");
                }
            }
            else
            {
                webView.Source = new Uri("https://kaspi.kz/");
            }
        }

        private void MainWindow_Closing(object sender, EventArgs e)
        {
            if (historyIndex < history.Count - 1)
            {
                history.RemoveRange(historyIndex + 1, history.Count - historyIndex - 1);
            }
            SaveHistoryToFile();
        }

        private void SaveHistoryToFile()
        {
            try
            {
                File.WriteAllLines(historyFilePath, history);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении истории: {ex.Message}");
            }
        }

        private void Nazad(object sender, RoutedEventArgs e)
        {
            if (historyIndex > 0)
            {
                isInHistoryMode = true; // Входим в режим просмотра истории
                --historyIndex;
                webView.Source = new Uri(history[historyIndex]);
            }
            else
            {
                MessageBox.Show("Назад перематывать некуда.");
            }
        }

        private void Vpered(object sender, RoutedEventArgs e)
        {
            if (historyIndex < history.Count - 1)
            {
                ++historyIndex;
                webView.Source = new Uri(history[historyIndex]);
                isInHistoryMode = historyIndex != history.Count - 1;
            }
            else if(historyIndex >= history.Count - 1)
            {
                isInHistoryMode = false;
                MessageBox.Show("Вперед перематывать некуда.");
            }
        }

        private void webView_NavigationStarting(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e)
        {
            string targetUrl = e.Uri;
            if (!targetUrl.StartsWith("https://kaspi.kz/"))
            {
                e.Cancel = true; // Отменяем навигацию, если URL не соответствует условиям
            }
            else
            {
                // Проверяем, является ли URL новым для истории
                if (!history.Contains(targetUrl))
                {
                    // Добавляем новый URL в историю и обновляем индекс
                    history.Add(targetUrl);
                    historyIndex = history.Count - 1;

                    // Удаляем все элементы после текущего индекса, если мы не в конце истории
                    if (historyIndex < history.Count - 1)
                    {
                        history = history.Take(historyIndex + 1).ToList();
                    }
                }

                // Обновляем флаг режима просмотра истории
                isInHistoryMode = false;
            }
        }
    }
}
