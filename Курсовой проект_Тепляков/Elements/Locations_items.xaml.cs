using ClassConnection;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Курсовой_проект_Тепляков.Pages;
using Курсовой_проект_Тепляков.Pages.PagesInTable;

namespace Курсовой_проект_Тепляков.Elements
{
    /// <summary>
    /// Логика взаимодействия для Locations_items.xaml
    /// </summary>
    public partial class Locations_items : UserControl
    {
        ClassModules.Ceh locations;
        public Locations_items(ClassModules.Ceh _locations)
        {
            InitializeComponent();
            if (Pages.Login_Regin.Login.UserInfo[1] != "admin") Buttons.Visibility = Visibility.Hidden;
            locations = _locations;
            if(_locations.remuslug != null)
            {
                Address.Content = "Адрес: " + _locations.Address;
                remuslug.Content = "Ремонтная услуга : " + _locations.remuslug;
            }
        }
        private void Click_redact(object sender, RoutedEventArgs e) => MainWindow.main.Animation_move(MainWindow.main.scroll_main, MainWindow.main.frame_main, MainWindow.main.frame_main, new Pages.PagesInTable.Locations(locations));

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить информацию о месте дислокации?", "Удаление информации", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Pages.Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.locations);
                    string query = $"Delete From Locations Where Id_сeh = " + locations.Id_сeh.ToString() + "";
                    var query_apply = Pages.Login_Regin.Login.connection.Query(query);
                    if (query_apply != null)
                    {
                        Pages.Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.locations);
                        MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Pages.Main.page_main.locations);
                    }
                    else MessageBox.Show("Запрос на удаление места дислокации не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
