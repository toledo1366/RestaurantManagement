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

namespace DesktopApp.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy Cook.xaml
    /// </summary>
    public partial class Cook : Page
    {
        private List<List<string>> query;
        public Cook()
        {
            InitializeComponent();
            DatabaseReader databaseReader = new DatabaseReader();
            databaseReader.Connect();
            query = databaseReader.Read("select o.id, f.name, oi.quantity, o.Status from orders o join order_items oi on o.id=oi.id join foods f on f.id=oi.food_id");
            databaseReader.Close();
            SetOrdersId();
        }

        private void SetOrdersId()
        {
            for (int i = 0; i < query.Count; i++)
            {
                if (query[i][3].Contains("Oczekuje"))
                {
                    comboBoxOrders.Items.Add(query[i][0]);
                }
                
            }
        }

        void PrintText(object sender, SelectionChangedEventArgs args)
        {
            zamowienieContent.Text = "Nazwa: " + query[comboBoxOrders.SelectedIndex][1] + "\nIlość: " + query[comboBoxOrders.SelectedIndex][2];
            preferences.Text = "-";
        }


        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(Application.Current.MainWindow);
        }

        private void button_minimize_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(Application.Current.MainWindow);
        }

        private void button_refresh_Click(object sender, RoutedEventArgs e)
        {
            DatabaseReader databaseReader = new DatabaseReader();
            databaseReader.Connect();
            query = databaseReader.Read("select o.id, f.name, oi.quantity, o.Status from orders o join order_items oi on o.id=oi.id join foods f on f.id=oi.food_id");
            databaseReader.Close();
        }

        private void button_goto_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new Pages.Delivery();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DatabaseReader databaseReader = new DatabaseReader();
            databaseReader.Connect();
            query = databaseReader.Read("update orders set Status='W realizacji' where id= " + query[comboBoxOrders.SelectedIndex][0]);
            databaseReader.Close();
        }
    }
}
