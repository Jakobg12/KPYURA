using ClassModules;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для Locations.xaml
    /// </summary>
    public partial class Ceh : Page
    {
        ClassModules.Ceh locations;
        public Ceh(ClassModules.Ceh _locations)
        {
            InitializeComponent();
            locations = _locations;
            if (_locations.Address != null)
            {
                oborud.Text = _locations.oborud;
                Address.Text = _locations.Address;
                remuslug.Text = _locations.remuslug.ToString();
            }
        }

        private void Click_Locations_Redact(object sender, RoutedEventArgs e)
        {
            //Country id_country_temp;
            
            //int id = Login_Regin.Login.connection.SetLastId(ClassConnection.Connection.Tables.locations);
            //if (locations.oborud == null)
            //{
            //    string query = $"Insert Into locations ([Id_сeh], [Country], [oborud], [Address], [remuslug], [remuslug]) Values ({id.ToString()}, {id_country_temp.Id.ToString()}, N'{oborud.Text}', N'{Address.Text}', N'{remuslug.Text}', N'{remuslug.Text}')";
            //    var query_apply = Login_Regin.Login.connection.Query(query);
            //    if (query_apply != null)
            //    {
            //        Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.locations);
            //        MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.locations);
            //    }
            //    else MessageBox.Show("Запрос на добавление места дислокации не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
            //else
            //{
            //    string query = $"Update locations Set [Country] = '{id_country_temp.Id.ToString()}', [oborud] = N'{oborud.Text}', [Address] = N'{Address.Text}', [remuslug] = N'{remuslug.Text}', [remuslug] = N'{remuslug.Text}' Where [Id_сeh] = {locations.Id_сeh}";
            //    var query_apply = Login_Regin.Login.connection.Query(query);
            //    if (query_apply != null)
            //    {
            //        Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.locations);
            //        MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.locations);
            //    }
            //    else MessageBox.Show("Запрос на изменение места дислокации не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
        }

        private void Click_Cancel_Locations_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main);
        }

        private void Click_Remove_Locations_Redact(object sender, RoutedEventArgs e)
        {
            try
            {
                Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.locations);
                string query = "Delete From locations Where [Id_сeh] = " + locations.Id_сeh.ToString() + "";
                var query_apply = Login_Regin.Login.connection.Query(query);
                if (query_apply != null)
                {
                    Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.locations);
                    MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.locations);
                }
                else MessageBox.Show("Запрос на удаление места дислокации не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBox_PreviewTextInput_2(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[А-Яа-яA-Za-z\s]*$");
            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true;
            }
        }

        private void TextBox_LostFocus_2(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string[] words = textBox.Text.Split(' ');
            if (words.Any(word => word.Length == 0))
            {
                textBox.Text = "Ошибка: введите значение";
                oborud.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FB3F51"));
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
                oborud.BorderBrush = brush;
            }
        }

        private void TextBox_LostFocus_3(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string[] words = textBox.Text.Split(' ');
            if (words.Any(word => word.Length == 0))
            {
                textBox.Text = "Ошибка: введите значение";
                Address.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FB3F51"));
            }
        }

        private void TextBox_GotFocus_3(object sender, RoutedEventArgs e)
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
                oborud.BorderBrush = brush;
            }
        }

        private void TextBox_PreviewTextInput_4(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9\s]*$");
            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true;
            }
        }

        private void TextBox_LostFocus_4(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string[] words = textBox.Text.Split(' ');
            if (words.Any(word => word.Length == 0))
            {
                textBox.Text = "Ошибка: введите значение";
                remuslug.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FB3F51"));
            }
        }

        private void TextBox_GotFocus_4(object sender, RoutedEventArgs e)
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
                remuslug.BorderBrush = brush;
            }
        }

        private void TextBox_PreviewTextInput_5(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9\s]*$");
            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true;
            }
        }

        private void TextBox_LostFocus_5(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string[] words = textBox.Text.Split(' ');
            if (words.Any(word => word.Length == 0))
            {
                textBox.Text = "Ошибка: введите значение";
                remuslug.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FB3F51"));
            }
        }

        private void TextBox_GotFocus_5(object sender, RoutedEventArgs e)
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
                remuslug.BorderBrush = brush;
            }
        }
    }
}
