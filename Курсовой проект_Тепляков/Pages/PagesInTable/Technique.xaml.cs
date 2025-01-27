using ClassModules;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Курсовой_проект_Тепляков.Pages.PagesInTable
{
    /// <summary>
    /// Логика взаимодействия для Technique.xaml
    /// </summary>
    public partial class Technique : Page
    {
        ClassModules.Technique technique;
        public Technique(ClassModules.Technique _technique)
        {
            InitializeComponent();
            technique = _technique;
            if (_technique.Characteristics != null)
            {
                Name_technique.Text = _technique.Name_technique;
                Characteristics.Text = _technique.Characteristics;
            }
            foreach (var item in ClassConnection.Connection.voditel)
            {
                ComboBoxItem cb_Vmestim = new ComboBoxItem();
                cb_Vmestim.Tag = item.Id_voditel;
                cb_Vmestim.Content = item.Name_voditel;
                if (_technique.Vmestim == item.Id_voditel) cb_Vmestim.IsSelected = true;
                Vmestim.Items.Add(cb_Vmestim);
            }
        }

        private void Click_Technique_Redact(object sender, RoutedEventArgs e)
        {
            ClassModules.Voditel Id_voditel_temp;
            Id_voditel_temp = ClassConnection.Connection.voditel.Find(x => x.Id_voditel == Convert.ToInt32(((ComboBoxItem)Vmestim.SelectedItem).Tag));
            int id = Pages.Login_Regin.Login.connection.SetLastId(ClassConnection.Connection.Tables.technique);
            if (technique.Characteristics == null)
            {
                string query = $"Insert Into technique ([Id_technique], [Name_technique], [Vmestim], [Characteristics]) Values ({id.ToString()}, N'{Name_technique.Text}', '{Id_voditel_temp.Id_voditel.ToString()}', N'{Characteristics.Text}')";
                var query_apply = Pages.Login_Regin.Login.connection.Query(query);
                if (query_apply != null)
                {
                    Pages.Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.technique);
                    MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.technique);
                }
                else MessageBox.Show("Запрос на добавление техники не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                string query = $"Update technique Set Name_technique = N'{Name_technique.Text}', Vmestim = '{Id_voditel_temp.Id_voditel.ToString()}', Characteristics = N'{Characteristics.Text}' Where Id_technique = {technique.Id_technique}";
                var query_apply = Pages.Login_Regin.Login.connection.Query(query);
                if (query_apply != null)
                {
                    Pages.Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.technique);
                    MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.technique);
                }
                else MessageBox.Show("Запрос на изменение техники не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Click_Cancel_Technique_Redact(object sender, RoutedEventArgs e) => MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main);

        private void Click_Remove_Technique_Redact(object sender, RoutedEventArgs e)
        {
            try
            {
                Pages.Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.technique);
                string query = "Delete From technique Where [Id_technique] = " + technique.Id_technique.ToString() + "";
                var query_apply = Pages.Login_Regin.Login.connection.Query(query);
                if (query_apply != null)
                {
                    Pages.Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.technique);
                    MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.technique);
                }
                else MessageBox.Show("Запрос на удаление техники не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string[] words = textBox.Text.Split(' ');
            if (words.Any(word => word.Length == 0))
            {
                textBox.Text = "Ошибка: введите значение";
                Name_technique.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FB3F51"));
            }
        }

        private void TextBox_GotFocus_1(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.StartsWith("Ошибка:"))
            {
                textBox.Text = "";
                ColorAnimation animation = new ColorAnimation();
                animation.From = (Color)ColorConverter.ConvertFromString("#FB3F51");
                animation.To = Colors.Transparent;
                animation.Duration = new Duration(TimeSpan.FromSeconds(2));
                SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FB3F51"));
                brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
                Name_technique.BorderBrush = brush;
            }
        }

        private void TextBox_LostFocus_2(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string[] words = textBox.Text.Split(' ');
            if (words.Any(word => word.Length == 0))
            {
                textBox.Text = "Ошибка: введите значение";
                Characteristics.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FB3F51"));
            }
        }

        private void TextBox_GotFocus_2(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.StartsWith("Ошибка:"))
            {
                textBox.Text = "";
                ColorAnimation animation = new ColorAnimation();
                animation.From = (Color)ColorConverter.ConvertFromString("#FB3F51");
                animation.To = Colors.Transparent;
                animation.Duration = new Duration(TimeSpan.FromSeconds(2));
                SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FB3F51"));
                brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
                Characteristics.BorderBrush = brush;
            }
        }
    }
}
