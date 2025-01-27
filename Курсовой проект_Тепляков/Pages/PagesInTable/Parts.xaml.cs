using ClassConnection;
using ClassModules;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для Parts.xaml
    /// </summary>
    public partial class Parts : Page
    {
        ClassModules.Garage parts;
        public Parts(ClassModules.Garage _parts)
        {
            InitializeComponent();
            parts = _parts;
            foreach (var item in Connection.ceh)
            {
                ComboBoxItem cb_locations = new ComboBoxItem();
                cb_locations.Tag = item.Id_сeh;
                cb_locations.Content = "Город: " + item.oborud;
                if (_parts.Locations == item.Id_сeh) cb_locations.IsSelected = true;
                Locations.Items.Add(cb_locations);
            }
            foreach (var item in Connection.voditel)
            {
                ComboBoxItem cb_Vmestim = new ComboBoxItem();
                cb_Vmestim.Tag = item.Id_voditel;
                cb_Vmestim.Content = item.Name_voditel;
                if (_parts.Vmestim == item.Id_voditel) cb_Vmestim.IsSelected = true;
                Vmestim.Items.Add(cb_Vmestim);
            }
        }

        private void Click_Parts_Redact(object sender, RoutedEventArgs e)
        {
            if (Locations.SelectedItem != null)
            {
                if (Vmestim.SelectedItem != null)
                {
                    ClassModules.Ceh Id_сeh_temp;
                    ClassModules.Voditel Id_voditel_temp;
                    Id_сeh_temp = ClassConnection.Connection.ceh.Find(x => x.Id_сeh == Convert.ToInt32(((ComboBoxItem)Locations.SelectedItem).Tag));
                    Id_voditel_temp = ClassConnection.Connection.voditel.Find(x => x.Id_voditel == Convert.ToInt32(((ComboBoxItem)Vmestim.SelectedItem).Tag));
                    int id = Login_Regin.Login.connection.SetLastId(ClassConnection.Connection.Tables.garage);
                    if (parts.Vmestim == 0)
                    {
                        string query = $"Insert Into parts ([Id_garage], [Locations], [Vmestim], [Date_of_foundation])" +
                            $"Values ({id.ToString()}, {Id_сeh_temp.Id_сeh.ToString()}, {Id_voditel_temp.Id_voditel.ToString()}, '{DateTime.Now.ToString("yyyy-MM-dd")}')";
                        var query_apply = Login_Regin.Login.connection.Query(query);
                        if (query_apply != null)
                        {
                            Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.garage);
                            MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.parts);
                        }
                        else MessageBox.Show("Запрос на добавление части не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        string query = $"Update parts Set Locations = '{Id_сeh_temp.Id_сeh.ToString()}', Vmestim = '{Id_voditel_temp.Id_voditel.ToString()}' Where Id_garage = {parts.Id_garage}";
                        var query_apply = Login_Regin.Login.connection.Query(query);
                        if (query_apply != null)
                        {
                            Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.garage);
                            MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.parts);
                        }
                        else MessageBox.Show("Запрос на изменение части не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        private void Click_Cancel_Parts_Redact(object sender, RoutedEventArgs e) => MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main);

        private void Click_Remove_Parts_Redact(object sender, RoutedEventArgs e)
        {
            try
            {
                Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.garage);
                string query = "Delete parts Where [Id_garage] = " + parts.Id_garage.ToString() + "";
                var query_apply = Login_Regin.Login.connection.Query(query);
                if (query_apply != null)
                {
                    Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.garage);
                    Main.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.parts);
                }
                else MessageBox.Show("Запрос на удаление части не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
