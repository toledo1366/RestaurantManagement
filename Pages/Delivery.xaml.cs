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
using System.IO;

namespace DesktopApp.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy Delivery.xaml
    /// </summary>
    public partial class Delivery : Page
    {
        private List<List<string>> query;
        public Delivery()
        {
            InitializeComponent();
            DatabaseReader databaseReader = new DatabaseReader();
            databaseReader.Connect();
            query = databaseReader.Read("SELECT o.id, o.Status, u.address, f.name, oi.quantity, o.total FROM orders o join users u on o.user_id=u.id join order_items oi on oi.order_id=o.id join foods f on f.id = oi.food_id");
            databaseReader.Close();
            SetOrdersId();
        }
        private void SetOrdersId()
        {
            for (int i = 0; i < query.Count; i++)
            {
                if (query[i][1].Contains("W realizacji"))
                {
                    comboBoxDelivery.Items.Add(query[i][0]);
                }
            }
        }

        
        void PrintText(object sender, SelectionChangedEventArgs args)
        {
            addressContent.Text = query[comboBoxDelivery.SelectedIndex][2];
        }

        private void button_minimize_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(Application.Current.MainWindow);
        }

        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(Application.Current.MainWindow);
        }

        private void button_goto_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new Pages.Cook();
        }

        private void button_refresh_Click(object sender, RoutedEventArgs e)
        {
            DatabaseReader databaseReader = new DatabaseReader();
            databaseReader.Connect();
            query = databaseReader.Read("SELECT o.id, o.Status, u.address, f.name, oi.quantity, o.total FROM orders o join users u on o.user_id=u.id join order_items oi on oi.order_id=o.id join foods f on f.id = oi.food_id");
            databaseReader.Close();
        }

        void Paragon()
        {
            string path = "order" + query[comboBoxDelivery.SelectedIndex][0] + ".txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("Ślązaczka sp. z o.o.\nul. Bogucicka 5\n40-001 Katowice\nNIP:0123456789\n\n");
                sw.WriteLine("Zawartość zamówienia:\n\n" + query[comboBoxDelivery.SelectedIndex][3] + "      x" + query[comboBoxDelivery.SelectedIndex][4]);
                sw.WriteLine("Cena: " + query[comboBoxDelivery.SelectedIndex][5]);
                sw.WriteLine("\n\nWszystkie ceny posiadają wliczony podatek VAT 8%\n\n");
                sw.WriteLine("---------------------------------------------------------\n\n");
                sw.WriteLine("Dane do dostawy:\n");
                sw.WriteLine(query[comboBoxDelivery.SelectedIndex][2]);
                sw.Close();
            }
        }

        private void addressContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            addressContent.Text = query[comboBoxDelivery.SelectedIndex][0];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Paragon();
            DatabaseReader databaseReader = new DatabaseReader();
            databaseReader.Connect();
            query = databaseReader.Read("update orders set Status='W dostawie' where id= " + query[comboBoxDelivery.SelectedIndex][0]);
            databaseReader.Close();
        }
    }
}
