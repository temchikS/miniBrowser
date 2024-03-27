"" Шаг 1 ""
Установка библиотеки WebView2
Прежде всего, вам необходимо установить библиотеку Microsoft.Web.WebView2. Эта библиотека позволяет интегрировать браузер Chromium в ваши приложения WPF.

Откройте менеджер пакетов NuGet в вашем проекте.
Установите пакет Microsoft.Web.WebView2.
h2 Шаг 2: Создание интерфейса пользователя
Теперь давайте создадим интерфейс для вашего мини-браузера. Вам понадобится файл XAML (например, MainWindow.xaml), в котором вы определите интерфейс пользователя.

xaml
```html
<Window x:Class="bower.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:wv2="clr-namespace:Microsoft.Web.WebView2.Wpf;assembly=Microsoft.Web.WebView2.Wpf"
        Title="AlishBrowser1.0 Beta" Height="800" Width="1300">
    <Grid>
        <wv2:WebView2 x:Name="webView"
                  Source="https://kaspi.kz/"
                  NavigationStarting="webView_NavigationStarting"
                  Grid.Row="0" Margin="0,46,0,0"/>
        <Button Content="назад" Click="Nazad" HorizontalAlignment="Left" Margin="17,12,0,0" VerticalAlignment="Top" Height="28" Width="60"/>
        <Button Content="вперед" Click="Vpered" HorizontalAlignment="Left" Margin="106,12,0,0" VerticalAlignment="Top" Height="29" Width="60"/>
    </Grid>
</Window>
```
Этот код создает окно с веб-виджетом (WebView2) и двумя кнопками для навигации назад и вперед.

h2 Шаг 3: Обработка навигации
Теперь давайте добавим код обработки навигации и управления историей просмотра в вашем коде C#.

csharp
```html
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Linq;

namespace bower
{
    public partial class MainWindow : Window
    {
        // Ваш код здесь
    }
}
```
h2 Шаг 4: Навигация назад и вперед
Реализуйте методы Nazad и Vpered, которые будут обрабатывать нажатия на кнопки "назад" и "вперед". Они будут осуществлять навигацию по истории просмотра.

h2 Шаг 5: Обновление истории просмотра
Вам также потребуется обновить историю просмотра при загрузке страницы и при переходе по ссылкам.

h2 Шаг 6: Закрытие приложения
Не забудьте сохранить историю просмотра при закрытии приложения.

h2 Шаг 7: Отмена навигации
В методе webView_NavigationStarting добавьте код для отмены навигации, если пользователь пытается перейти на недопустимый URL.

После выполнения всех этих шагов, вы получите мини-браузер с функцией перехода назад и вперед по истории просмотра. Убедитесь, что вы включили обработку исключений и проверку ошибок для обеспечения стабильной работы вашего приложения.
