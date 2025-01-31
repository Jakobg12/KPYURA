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
    /// Логика взаимодействия для TypeOfTroops_items.xaml
    /// </summary>
    public partial class TypeOfTroops_items : UserControl
    {
        ClassModules.Zapchast type_of_troops;
        public TypeOfTroops_items(ClassModules.Zapchast _type_of_troops)
        {
            InitializeComponent();
            if (Pages.Login_Regin.Login.UserInfo[1] != "admin") Buttons.Visibility = Visibility.Hidden;
            type_of_troops = _type_of_troops;
            if(_type_of_troops.Name_zapchast != null)
            {
                Name_zapchast.Content = "Название запчасти: " + _type_of_troops.Name_zapchast;
                Description.Content = "Описание: " + _type_of_troops.Description;
                Date_foundation.Content = "Дата создания: " + _type_of_troops.Date_foundation.ToString("dd.MM.yyyy");
            }
        }

        private void Click_redact(object sender, RoutedEventArgs e) => MainWindow.main.Animation_move(MainWindow.main.scroll_main, MainWindow.main.frame_main, MainWindow.main.frame_main, new Pages.PagesInTable.Type_of_troops(type_of_troops));

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить информацию о виде запчасти?", "Удаление информации", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Pages.Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.zapchast);
                    string query = $"Delete From Type_of_troops Where Id_zapchast = " + type_of_troops.Id_zapchast.ToString() + "";
                    var query_apply = Pages.Login_Regin.Login.connection.Query(query);
                    if (query_apply != null)
                    {
                        Pages.Login_Regin.Login.connection.LoadData(ClassConnection.Connection.Tables.zapchast);
                        MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Pages.Main.page_main.type_of_troops);
                    }
                    else MessageBox.Show("Запрос на удаление вида запчасти не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
